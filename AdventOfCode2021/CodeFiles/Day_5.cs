using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_5
    {
        private static Parser p = new Parser("5");

        public static int solve_A()
        {
            var lines = p.Lines;
            List<List<int[]>> coordList = LinesToVents(lines, true);
            foreach (List<int[]> coord in coordList)
            {
                // Console.WriteLine("begin: " + coord[0][0] + "," + coord[0][1] + " end: " + coord[1][0] + "," + coord[1][1]);
            }

            return CountOverlap(coordList);;
        }
        
        public static int solve_B()
        {
            var lines = p.Lines;
            List<List<int[]>> coordList = LinesToVents(lines, false);
            // foreach (List<int[]> coord in coordList)
            // {
            //     Console.WriteLine("begin: " + coord[0][0] + "," + coord[0][1] + " end: " + coord[1][0] + "," + coord[1][1]);
            // }

            return CountOverlap(coordList);;
        }

        private static List<List<int[]>> LinesToVents(String[] lines, bool onlyStraight)
        {
            List<List<int[]>> coordList = new List<List<int[]>>();
            
            foreach (string line in lines)
            {
                int[] begin = new int[2];
                int[] end = new int[2];

                string beginString = line.Split(new string[] {" -> "}, StringSplitOptions.None)[0];
                string endString = line.Split(new string[] {" -> "}, StringSplitOptions.None)[1];

                if ((beginString.Split(',')[0] != endString.Split(',')[0] &&
                     beginString.Split(',')[1] != endString.Split(',')[1]))
                {
                    if (onlyStraight)
                    {
                        continue;
                    } else if (Math.Abs(Int32.Parse(beginString.Split(',')[0]) - Int32.Parse(endString.Split(',')[0])) !=
                               Math.Abs(Int32.Parse(beginString.Split(',')[1]) - Int32.Parse(endString.Split(',')[1])))
                    {
                        continue;
                    }
                    
                }

                begin[0] = Int32.Parse(beginString.Split(',')[0]);
                begin[1] = Int32.Parse(beginString.Split(',')[1]);
                
                end[0] = Int32.Parse(endString.Split(',')[0]);
                end[1] = Int32.Parse(endString.Split(',')[1]);
                
                List<int[]> currentLine = new List<int[]>();
                currentLine.Add(begin);
                currentLine.Add(end);
                coordList.Add(currentLine);
            }
            
            return coordList;
        }

        private static int CountOverlap(List<List<int[]>> coordList)
        {
            int overlap = 0;
            List<int[]> CoveredPointsList = new List<int[]>();
            foreach (List<int[]> line in coordList)
            {
                if (line[0][0] == line[1][0])
                {
                    for (int i = Math.Min(line[0][1],line[1][1]); i <= Math.Max(line[0][1],line[1][1]); i++)
                    {
                        CoveredPointsList.Add(new []{line[0][0], i});
                    }
                } else if (line[0][1] == line[1][1])
                {
                    for (int i = Math.Min(line[0][0],line[1][0]); i <= Math.Max(line[0][0],line[1][0]); i++)
                    {
                        CoveredPointsList.Add(new []{i, line[0][1]});
                    }
                }
                else
                {
                    int j = 0;
                    bool up = false;
                    int had = 0;
                    // Console.WriteLine();
                    for (int i = Math.Min(line[0][0],line[1][0]); i <= Math.Max(line[0][0],line[1][0]); i++)
                    {
                        
                        if (((line[0][1] < line[1][1] && i == line[0][0]) || (line[0][1] > line[1][1] && i == line[1][0])) && had == 0)
                        {
                            if (i == line[0][0])
                            {
                                j = line[0][1];
                            }
                            else
                            {
                                j = line[1][1];
                            }
                            
                            up = true;
                            had = 1;
                            // Console.WriteLine(up);
                        }
                        else if (had == 0)
                        {
                            if (i == line[1][0])
                            {
                                j = line[1][1];
                            }
                            else
                            {
                                j = line[0][1];
                            }
                            up = false;
                            had = 1;
                            // Console.WriteLine(up);
                        }
                        
                        // Console.WriteLine(i+","+j);
                        CoveredPointsList.Add(new[] {i, j});
                        if (up)
                        {
                            j += 1;
                        }
                        else
                        {
                            j -= 1;
                        }
                    }
                }
            }

            Dictionary<string, int> OverlapPoints = new Dictionary<string, int>();
            foreach (int[] point in CoveredPointsList)
            {
                
                string coordString = point[0] + "," + point[1];
                if (OverlapPoints.ContainsKey(coordString))
                {
                    OverlapPoints[coordString] += 1;
                }
                else
                {
                    OverlapPoints.Add(coordString, 1);
                }
            }

            foreach (KeyValuePair<string, int> entry in OverlapPoints)
            {
                if (entry.Value > 1)
                {
                    overlap += 1;
                }
            }
            
            return overlap;
        }
    }
}