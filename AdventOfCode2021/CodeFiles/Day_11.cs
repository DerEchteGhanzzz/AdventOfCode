using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AdventOfCode
{
    public class Day_11
    {
        private static Parser p = new Parser("11");

        public static int solve_A()
        {
            var lines = p.Lines;

            var octoMap = GetOctoMap(lines);
            // PrintList(octoMap);
            int totalFlashes = 0;
            
            for (int i = 0; i < 100; i++)
            {
                // Console.WriteLine("i: "+i);
                IncreaseEnergy(octoMap);

                foreach (var line in octoMap)
                {
                    foreach (var octopus in line)
                    {
                        if (octopus == 0)
                        {
                            totalFlashes += 1;
                        }
                    }
                }
                // Console.WriteLine("After:");
                // PrintList(octoMap);
            }
            
            return totalFlashes;
        }
        
        public static int solve_B()
        {
            var lines = p.Lines;

            var octoMap = GetOctoMap(lines);
            // PrintList(octoMap);
            int totalFlashes = 0;
            int i = 1;
            while (true)
            {
                // Console.WriteLine("i: "+i);
                IncreaseEnergy(octoMap);

                bool inSync = true;
                foreach (var line in octoMap)
                {
                    foreach (var octopus in line)
                    {
                        if (octopus != 0)
                        {
                            inSync = false;
                        }
                    }
                }
                // Console.WriteLine("After:");
                // PrintList(octoMap);
                if (inSync)
                {
                    return i;
                }
                i++;
            }
        }

        private static List<int[]> GetOctoMap(string[] lines)
        {
            List<int[]> octoMap = new List<int[]>();
            
            foreach (var line in lines)
            {
                int[] currentArray = new int[line.Length];

                var lineArr = line.ToCharArray();
                for (int i = 0; i < lineArr.Length; i++)
                {
                    currentArray[i] = Convert.ToInt32(char.GetNumericValue(lineArr[i]));
                }
                octoMap.Add(currentArray);
            }

            return octoMap;
        }

        private static void IncreaseEnergy(List<int[]> octoMap)
        {
            bool pop = false;
            for (int y = 0; y < octoMap.Count; y++)
            {
                for (int x = 0; x < octoMap[y].Length; x++)
                {
                    octoMap[y][x] += 1;
                    if (octoMap[y][x] >= 10)
                    {
                        pop = true;
                    }
                }
            }
            // Console.WriteLine();
            // PrintList(octoMap);
            if (pop)
            {
                PopOctopi(octoMap);
            }
            return;
        }

        private static void PopOctopi(List<int[]> octoMap)
        {
            bool pop = false;
            for (int y = 0; y < octoMap.Count; y++)
            {
                for (int x = 0; x < octoMap[y].Length; x++)
                {
                    if (octoMap[y][x] >= 10)
                    {
                        octoMap[y][x] = 0;
                        // Console.WriteLine("y,x: " + y + "," + x);
                        for (int i = y - 1; i <= y + 1; i++)
                        {
                            if (i == -1 || i == octoMap.Count)
                            {
                                continue;
                            }

                            for (int j = x - 1; j <= x + 1; j++)
                            {
                                if (j == -1 || j == octoMap[i].Length || (j == x && i == y))
                                {
                                    continue;
                                }
                                // Console.WriteLine("i,j: " + i + "," + j);
                                if (octoMap[i][j] != 0)
                                {
                                    octoMap[i][j] += 1;
                                }
                                
                                if (octoMap[i][j] >= 10)
                                {
                                    pop = true;
                                }
                            }
                        }
                    }
                }
            }
            // Console.WriteLine();
            // PrintList(octoMap);

            if (pop)
            {
                PopOctopi(octoMap);
            }
        }

        private static void PrintList(List<int[]> list)
        {
            foreach (var line in list)
            {
                foreach (var number in line)
                {
                    Console.Write(number + ",");
                }
                Console.Write("\n");
            }
        }
    }
}