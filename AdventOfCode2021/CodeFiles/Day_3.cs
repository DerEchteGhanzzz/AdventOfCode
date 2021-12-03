using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;

namespace AdventOfCode
{
    public class Day_3
    {
        private static Parser p = new Parser("3");
        public static int solution_A = solve_A();
        public static int solution_B = solve_B();
        
        private static int solve_A()
        {
            var lines = p.Lines;
            List<string> lines_list = new List<string>();
            foreach (string line in lines)
            {
                lines_list.Add(line);
            }
            
            string[] gamma_and_epsilon = GammaEpsilonString(lines_list);
            string gamma_string = gamma_and_epsilon[0];
            string epsilon_string = gamma_and_epsilon[1];
            int gamma = Convert.ToInt32(gamma_string, 2);
            int epsilon = Convert.ToInt32(epsilon_string, 2);

            return gamma*epsilon;
        }
        
        private static int solve_B()
        {
            var lines = p.Lines;
            List<string> lines_list = new List<string>();
            foreach (string line in lines)
            {
                lines_list.Add(line);
            }
            
            string[] gamma_and_epsilon = GammaEpsilonString(lines_list);

            // Console.WriteLine("O2");
            int O2 = FindRatings(lines_list, 0, gamma_and_epsilon, 1);
            // Console.WriteLine("CO2");
            int CO2 = FindRatings(lines_list, 0, gamma_and_epsilon, -1);
            // Console.WriteLine("O2: "+O2+", CO2: "+CO2);
            return O2*CO2;
        }

        private static string[] GammaEpsilonString(List<string> lines)
        {
            List<int> total_list = new List<int>();
            foreach (char c in lines[0].ToCharArray())
            {
                total_list.Add(0);
            }
            foreach (string bite in lines)
            {
                var bites = bite.ToCharArray();
                
                for (int i = 0; i < total_list.Count; i++)
                {
                    if (bites[i] == '1')
                    {
                        total_list[i] += 1;
                    }
                }

            }
            
            string[] gamma_array = new string[lines[0].ToCharArray().Length];
            string[] epsilon_array = new string[lines[0].ToCharArray().Length];
            
            for (int i = 0; i < total_list.Count; i++)
            {
                if (total_list[i] > lines.Count / 2)
                {
                    gamma_array[i] = "1";
                    epsilon_array[i] = "0";
                }
                else
                {
                    gamma_array[i] = "0";
                    epsilon_array[i] = "1";
                }
            }
            
            string gamma_string = string.Join("", gamma_array);
            string epsilon_string = string.Join("", epsilon_array);
            
            return new string[2] {gamma_string, epsilon_string};
        }

        private static int FindRatings(List<string> list, int currentBit, string[] gammaAndEpsilon, int mult)
        {
            // Console.WriteLine(currentBit);
            if (list.Count == 1)
            {
                return Convert.ToInt32(list[0], 2);
            }
            string gamma_string = gammaAndEpsilon[0];
            string epsilon_string = gammaAndEpsilon[1];
            // Console.WriteLine(gamma_string);
            // Console.WriteLine("___________");
            int total = 0;
            char sweep;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ToCharArray()[currentBit] == gamma_string.ToCharArray()[currentBit])
                {
                    total += 1;
                    // Console.WriteLine(list[i].ToCharArray()[currentBit]+ ","+ gamma_string.ToCharArray()[currentBit]);
                    // Console.WriteLine(total);
                }
            }

            if (total*mult > list.Count/2*mult)
            {
                sweep = gamma_string.ToCharArray()[currentBit];
            } else if (total*mult == list.Count/2*mult)
            {
                // Console.WriteLine("here");
                if (mult == 1)
                {

                    sweep = '1';
                }
                else
                {
                    sweep = '0';
                }
            }
            else
            {
                sweep = epsilon_string.ToCharArray()[currentBit];
            }
            
            List<string> new_list = new List<string>();

            foreach (string line in list)
            {
                
                if (line.ToCharArray()[currentBit] == sweep)
                {
                    // Console.WriteLine(line);
                    new_list.Add(line);
                }
            }
            // Console.WriteLine();
            string[] new_GE = GammaEpsilonString(new_list);
            return FindRatings(new_list, currentBit + 1, new_GE, mult);
        }
    }
}