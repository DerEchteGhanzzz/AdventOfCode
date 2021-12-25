using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Globalization;
using System.Xml.Schema;

namespace AdventOfCode
{
    public class Day_8
    {
        private static Parser p = new Parser("8");
        
        public static int solve_A()
        {
            var lines = p.Lines;

            var inputList = new List<string>();
            var outputList = new List<string>();
            int count = 0;
            
            foreach (var line in lines)
            {
                string[] lineArr = line.Split(new string[] {" | "}, StringSplitOptions.None);
                foreach (var digit in lineArr[1].Split(' '))
                {
                    int length = digit.Length;
                    if (length == 2 | length == 3 | length == 4 | length == 7)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        
        public static long solve_B()
        {
            var lines = p.Lines;
            
            var inputList = new List<string[]>();
            var outputList = new List<string[]>();
            long total = 0;
            
            foreach (var line in lines)
            {
                string[] lineArr = line.Split(new string[] {" | "}, StringSplitOptions.None);
                inputList.Add(lineArr[0].Split(' '));
                outputList.Add(lineArr[1].Split(' '));
            }

            for (int i = 0; i < inputList.Count; i++)
            {

                Dictionary<char, HashSet<char>> connectionDict = new Dictionary<char, HashSet<char>>()
                {
                    {'a', new HashSet<char>()},
                    {'b', new HashSet<char>()},
                    {'c', new HashSet<char>()},
                    {'d', new HashSet<char>()},
                    {'e', new HashSet<char>()},
                    {'f', new HashSet<char>()},
                    {'g', new HashSet<char>()} 
                };
                
                foreach (var digit in inputList[i])
                {
                    foreach (var segment in digit)
                    {
                        var possibilitySet = SegmentPossibilities(digit, segment);

                        if (possibilitySet.Count < connectionDict[segment].Count | connectionDict[segment].Count == 0)
                        {
                            connectionDict[segment] = possibilitySet;
                        }
                    }
                    
                }
                // PrintDict(connectionDict);

                connectionDict = WorkOutPossibilities(connectionDict);
                // Console.WriteLine("Connect Dict:");
                // PrintDict(connectionDict);
                int current = SecondCycle(connectionDict, outputList[i]);
                // Console.WriteLine(current);
                total += current;
                
                
            }
            
            return total;
        }

        private static HashSet<char> SegmentPossibilities(string digit, char segment)
        {
            var possibilitySet = new HashSet<char>();

            switch (digit.Length)
            {
                case 2:
                    possibilitySet.Add('c');
                    possibilitySet.Add('f');
                    return possibilitySet;
                case 3:
                    possibilitySet.Add('a');
                    possibilitySet.Add('c');
                    possibilitySet.Add('f');
                    return possibilitySet;
                case 4:
                    possibilitySet.Add('b');
                    possibilitySet.Add('c');
                    possibilitySet.Add('d');
                    possibilitySet.Add('f');
                    return possibilitySet;
                default:
                    possibilitySet.Add('a');
                    possibilitySet.Add('b');
                    possibilitySet.Add('c');
                    possibilitySet.Add('d');
                    possibilitySet.Add('e');
                    possibilitySet.Add('f');
                    possibilitySet.Add('g');
                    return possibilitySet;
            }

            return possibilitySet;
        }

        private static Dictionary<char, HashSet<char>> WorkOutPossibilities(Dictionary<char, HashSet<char>> connectionDict)
        {
            var deletionSet = new HashSet<char>();
            var hasHad = new HashSet<char>();
            bool notDone = true;
            while (notDone)
            {
                char currentChar = ' ';
                int currentLength = int.MaxValue;
                foreach (var segment in connectionDict)
                {
                    if (segment.Value.Count < currentLength && !hasHad.Contains(segment.Key))
                    {
                        deletionSet = segment.Value;
                        currentLength = segment.Value.Count;
                        currentChar = segment.Key;
                    }
                }

                hasHad.Add(currentChar);
                // Console.WriteLine(currentChar);
                foreach (var segment in connectionDict)
                {
                    if (segment.Value.Count == deletionSet.Count)
                    {
                        continue;
                    }
                    else
                    {
                        segment.Value.ExceptWith(deletionSet);
                    }
                }
                
                if (hasHad.Count == 7)
                {
                    break;
                }
            }

            return connectionDict;
        }

        private static int SecondCycle(Dictionary<char, HashSet<char>> connectionDict,
            string[] OutputDigitArr)
        {
            string digitStream = "";
            
            foreach (string digit in OutputDigitArr)
            {

                Dictionary<char, HashSet<char>> digitSegmentMap = new Dictionary<char, HashSet<char>>();
                switch (digit.Length)
                {
                    case 2:
                        digitStream = digitStream + "1";
                        break;
                    case 3:
                        digitStream = digitStream + "7";
                        break;
                    case 4:
                        digitStream = digitStream + "4";
                        break;
                    case 7:
                        digitStream = digitStream + "8";
                        break;
                    case 5:
                        digitSegmentMap.Add('2', new HashSet<char>(){'a', 'c', 'd', 'e', 'g'});
                        digitSegmentMap.Add('3', new HashSet<char>(){'a', 'c', 'd', 'f', 'g'});
                        digitSegmentMap.Add('5', new HashSet<char>(){'a', 'b', 'd', 'f', 'g'});
                        digitStream = digitStream + MatchSegments(digitSegmentMap, connectionDict, digit);
                        break;
                    case 6:
                        digitSegmentMap.Add('0', new HashSet<char>(){'a', 'b', 'c', 'e', 'f', 'g'});
                        digitSegmentMap.Add('6', new HashSet<char>(){'a', 'b', 'd', 'e', 'f', 'g'});
                        digitSegmentMap.Add('9', new HashSet<char>(){'a', 'b', 'c', 'd', 'f', 'g'});
                        
                        digitStream = digitStream + MatchSegments(digitSegmentMap, connectionDict, digit);
                        break;
                }
            }
            return Int32.Parse(digitStream);
        }
        
        private static string MatchSegments(Dictionary<char, HashSet<char>> digitSegmentMap,
            Dictionary<char, HashSet<char>> connectionDict, string digit)
        {
            string actualDigit = "";
            // Console.WriteLine("digit: " + digit);
            // Console.WriteLine();
            // PrintDict(digitSegmentMap);
            Dictionary<char, HashSet<char>> connectionDictCopy = new Dictionary<char, HashSet<char>>();

            HashSet<char> hasToHave = new HashSet<char>();

            foreach (var segment in digit)
            {
                connectionDictCopy.Add(segment, connectionDict[segment]);
            }

            for (int i = 1; i <= 7; i++)
            {
                foreach (var segment in connectionDictCopy)
                {
                    if (segment.Value.Count == i)
                    {
                        int count = 0;
                        foreach (var otherSegment in connectionDictCopy)
                        {
                            if (otherSegment.Value.Count != i)
                            {
                                continue;
                            } else if (segment.Value.SetEquals(otherSegment.Value))
                            {
                                count++;
                            }
                        }

                        if (count == i)
                        {
                            hasToHave.UnionWith(segment.Value);
                        }
                    }
                }
            }
            
            // Console.WriteLine("Has to have: ");
            // foreach (var entry in hasToHave)
            // {
            //     Console.Write(entry + ", ");
            // }
            // Console.WriteLine();

            foreach (var digitSegment in digitSegmentMap)
            {
                if (digitSegment.Value.IsSupersetOf(hasToHave))
                {
                    // Console.WriteLine("Actual digit: " + digitSegment.Key);
                    return actualDigit + digitSegment.Key;
                }
            }
            
            return actualDigit;
        }

        private static void PrintDict(Dictionary<char, HashSet<char>> connectionDict)
        {
            foreach (var entry in connectionDict)
            {
                Console.Write(entry.Key+": ");
                foreach (var possibility in entry.Value)
                {
                    Console.Write(possibility+", ");
                }
                Console.WriteLine();
            }
            
        }
    }
}