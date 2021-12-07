using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Runtime.Serialization.Formatters;

namespace AdventOfCode
{
    public class Day_6
    {
        private static Parser p = new Parser("6");

        public static long solution_A = solve_A();
        public static long solution_B = solve_B();

        private static long solve_A()
        {
            string[] lines = p.Lines;
            return FishGoFuck(lines, 80);
        }

        private static long solve_B()
        {
            string[] lines = p.Lines;
            return FishGoFuck(lines, 265);
        }

        private static long FishGoFuck(string[] lines, int stop)
        {
            string[] timeArr = lines[0].Split(',');

            List<int> numTimeArr = Parser.StringArrToListInt(timeArr);

            Dictionary<int, long> fishBatchDict = new Dictionary<int, long>()
            {
                {-1, 0},
                {0, 0},
                {1, 0},
                {2, 0},
                {3, 0},
                {4, 0},
                {5, 0},
                {6, 0},
                {7, 0},
                {8, 0},
            };

            foreach (var time in timeArr)
            {
                int numbericTime = Int32.Parse(time);

                if (numbericTime == 1)
                {
                    fishBatchDict[1] += 1;
                }
                else if (numbericTime == 2)
                {
                    fishBatchDict[2] += 1;
                }
                else if (numbericTime == 3)
                {
                    fishBatchDict[3] += 1;
                }
                else if (numbericTime == 4)
                {
                    fishBatchDict[4] += 1;
                }
                else if (numbericTime == 5)
                {
                    fishBatchDict[5] += 1;
                }
                else if (numbericTime == 6)
                {
                    fishBatchDict[6] += 1;
                }

            }

            long fishAmount = 0;
            for (int i = 0; i < stop; i++)
            {
                fishBatchDict = DecreaseTimers(fishBatchDict);
            }


            foreach (var entry in fishBatchDict)
            {
                fishAmount += entry.Value;
            }

            return fishAmount;
        }

        private static Dictionary<int, long> DecreaseTimers(Dictionary<int, long> fishBatchDict)
        {
            fishBatchDict[-1] = fishBatchDict[0];
            fishBatchDict[0] = fishBatchDict[1];
            fishBatchDict[1] = fishBatchDict[2];
            fishBatchDict[2] = fishBatchDict[3];
            fishBatchDict[3] = fishBatchDict[4];
            fishBatchDict[4] = fishBatchDict[5];
            fishBatchDict[5] = fishBatchDict[6];
            fishBatchDict[6] = fishBatchDict[7];
            fishBatchDict[7] = fishBatchDict[8];
            fishBatchDict[8] = fishBatchDict[-1];
            fishBatchDict[6] += fishBatchDict[-1];
            fishBatchDict[-1] = 0;
            // foreach (var entry in fishBatchDict)
            // {
            // Console.WriteLine(entry.Key+", "+entry.Value);
            // }
            // Console.WriteLine();
            return fishBatchDict;
        }

    }
}