using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace AdventOfCode
{
    public class Day_7
    {
        private static Parser p = new Parser("7");
        public static long solution_A = solve_A();
        public static long solution_B = solve_B();
        
        private static long solve_A()
        {
            var lines = p.Lines;

            List<int> fuelList = Parser.StringArrToListInt(lines[0].Split(','));

            long min = -1;
            
            for (int fuel = 0; fuel <= fuelList.Max(); fuel++)
            {
                long total = 0;
                foreach (var otherFuel in fuelList)
                {
                    if (otherFuel == fuel)
                    {
                        continue;
                    }
                    int difference = Math.Abs(otherFuel - fuel);
                    
                    total += difference;
                    // Console.WriteLine(total);
                }

                if (min == -1 || total < min)
                {
                    min = total;
                }
                // Console.WriteLine("fuel: "+fuel);
                // Console.WriteLine(total);
                // Console.WriteLine();
            }

            return min;
        }
        
        private static long solve_B()
        {
            var lines = p.Lines;

            List<int> fuelList = Parser.StringArrToListInt(lines[0].Split(','));

            long min = -1;
            
            for (int fuel = 0; fuel <= fuelList.Max(); fuel++)
            {
                long total = 0;
                foreach (var otherFuel in fuelList)
                {
                    if (otherFuel == fuel)
                    {
                        continue;
                    }
                    int difference = Math.Abs(otherFuel - fuel);

                    total += CalculateSequence(difference);
                    // Console.WriteLine(total);
                }

                if (min == -1 || total < min)
                {
                    min = total;
                }
                // Console.WriteLine("fuel: "+fuel);
                // Console.WriteLine(total);
                // Console.WriteLine();
            }

            return min;
        }

        private static long CalculateSequence(int difference)
        {
            return (difference * (difference+1))/2;

        }

    }
}