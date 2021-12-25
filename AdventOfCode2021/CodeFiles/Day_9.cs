using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.Runtime.InteropServices;
using System.Xml.Schema;

namespace AdventOfCode
{
    public class Day_9
    {
        private static Parser p = new Parser("9");

        public static int solve_A()
        {
            int total = 0;
            var lines = p.Lines;

            var inputMap = GetMap(lines);
            var minimaDict = GetMinimaDict(inputMap);
            foreach (var minimum in minimaDict)
            {
                total += minimum.Value + 1;
            }
            
            return total;
        }
        
        public static int solve_B()
        {
            var lines = p.Lines;

            var inputMap = GetMap(lines);
            var minimaDict = GetMinimaDict(inputMap);

            List<int> basinSizes = new List<int>();
            
            foreach (var minimum in minimaDict)
            {
                HashSet<string> basin = new HashSet<string>();
                basin.Add(minimum.Key[0]+","+minimum.Key[1]);
                RecursiveBasin(minimum.Key[0], minimum.Key[1], minimum.Value-1, basin, inputMap);
                basinSizes.Add(basin.Count);
            }
            basinSizes.Sort();
            int count = basinSizes.Count-1;
            return basinSizes[count]*basinSizes[count-1]*basinSizes[count-2];
        }

        private static void RecursiveBasin(int y, int x, int previousValue, HashSet<string> basin, List<int[]> inputMap)
        {
            int value = inputMap[y][x];
            if (value == 9)
            {
                return;
            } else if (value > previousValue)
            {
                basin.Add(y+","+x);
                RecursiveBasin(y, x+1, value, basin, inputMap);
                RecursiveBasin(y, x-1, value, basin, inputMap);
                RecursiveBasin(y+1, x, value, basin, inputMap);
                RecursiveBasin(y-1, x, value, basin, inputMap);
            }
            return;
        }

        private static List<int[]> GetMap(string[] lines)
        {
            List<int[]> inputMap = new List<int[]>();
            
            int[] top = new int[lines[0].Length + 2];
            for (int i = 0; i < lines[0].Length+2; i++)
            {
                top[i] = 9;
            }
            
            inputMap.Add(top);
            foreach (var line in lines)
            {
                string boarderLine = "9" + line + "9";
                var lineArr = boarderLine.ToCharArray();
                int[] intArr = new int[lineArr.Length];
                intArr[0] = 9;
                for (int i = 0; i < lineArr.Length; i++)
                {
                    intArr[i] = Convert.ToInt32(char.GetNumericValue(lineArr[i]));
                }
                
                inputMap.Add(intArr);
                
            }
            inputMap.Add(top);
            
            return inputMap;
        }

        private static Dictionary<int[], int> GetMinimaDict(List<int[]> inputMap)
        {
            Dictionary<int[], int> minimaDict = new Dictionary<int[], int>();
            for (int y = 0; y < inputMap.Count; y++)
            {
                for (int x = 0; x < inputMap[y].Length; x++)
                {
                    var cur_value = inputMap[y][x];

                    if (cur_value == 9)
                    {
                        continue;
                    } else if (cur_value < inputMap[y+1][x] && cur_value < inputMap[y-1][x] && 
                               cur_value < inputMap[y][x+1] && cur_value < inputMap[y][x-1])
                    {
                        minimaDict.Add(new []{y, x}, cur_value);
                    }
                    
                }
            }

            return minimaDict;
        }
    }
}