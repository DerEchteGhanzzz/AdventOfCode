using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Globalization;
using System.Net;

namespace AdventOfCode
{
    public class Day_24
    {
        private static Parser p = new Parser("24");
        private static Dictionary<string, long> memoization;
        public static string solve_A()
        {
            var lines = p.Lines;
            List<int> inputIndices = new List<int>();
            List<List<string[]>> program = ParseInput(lines);

            Dictionary<string, long> memory = new Dictionary<string, long>()
            {
                {"x", 0},
                {"y", 0},
                {"z", 0},
                {"w", 0},
            };
            memoization = new Dictionary<string, long>();
            return RecursiveMonad(memory, program, 0, "", true);
        }
        
        public static int solve_B()
        {
            var lines = p.Lines;

            return 0;
        }


        private static string RecursiveMonad(Dictionary<string, long> memory, List<List<string[]>> program, int depth, string MONAD, bool highest)
        {
            if (highest)
            {
                for (int i = 9; i > 0; i--)
                {

                    var currentMemory = PerformInput(new Dictionary<string, long>(memory), program[depth],
                        Convert.ToChar(i));
                    if (depth < program.Count-1)
                    {
                        var newMONAD = RecursiveMonad(currentMemory, program, depth + 1, MONAD + "" + i, highest);
                        if (newMONAD.Length > 0)
                        {
                            return newMONAD;
                        }
                    }
                    else
                    {
                        if (currentMemory["z"] == 0)
                        {
                            return MONAD;
                        }
                    }

                }

                return "";
            }

            return "-1";
        }


        private static Dictionary<string, long> PerformInput(Dictionary<string, long> memory, List<string[]> program, char MONADdigit)
        {
            bool keepGoing = true;
            bool inputHad = false;
            for (int i = 0; i < program.Count; i++)
            {
                var operation = program[i];
                keepGoing = PerformOperation(memory, operation, MONADdigit);
                if (!keepGoing)
                {
                    Console.WriteLine("ERROR");
                    break;
                }
            }
            
            return memory;
        }
        

        private static bool PerformOperation(Dictionary<string, long> memory, string[] operation, char MONAD)
        {
            // Console.WriteLine();
            // Console.Write(operation[0] + " " + operation[1]);
            // if (operation.Length == 3)
            // {
            //     Console.WriteLine(" "+operation[2]);
            // }
            // else
            // {
            //     Console.Write("\n");
            // }
            
            int numericValue;
            switch (operation[0])
            {
                case "inp":
                    memory[operation[1]] = Convert.ToInt32(char.GetNumericValue(MONAD));
                    
                    return true;
                case "add":
                    if (int.TryParse(operation[2], out numericValue))
                    {
                        memory[operation[1]] += numericValue;
                        return true;
                    }

                    memory[operation[1]] += memory[operation[2]];
                    return true;
                case "mul":
                    if (int.TryParse(operation[2], out numericValue))
                    {
                        memory[operation[1]] *= numericValue;
                        return true;
                    }

                    memory[operation[1]] *= memory[operation[2]];
                    return true;
                case "div":
                    if (int.TryParse(operation[2], out numericValue))
                    {
                        if (numericValue == 0)
                        {
                            return false;
                        }
                        memory[operation[1]] /= numericValue;
                        return true;
                    }

                    if (memory[operation[2]] == 0)
                    {
                        return false;
                    }
                    memory[operation[1]] /= memory[operation[2]];
                    return true;
                case "mod":

                    if (memory[operation[1]] < 0)
                    {
                        return false;
                    }
                    
                    if (int.TryParse(operation[2], out numericValue))
                    {
                        if (numericValue <= 0)
                        {
                            return false;
                        }
                        
                        memory[operation[1]] %= numericValue;
                        return true;
                    }

                    if (memory[operation[2]] <= 0)
                    {
                        return false;
                    }
                    
                    memory[operation[1]] %= memory[operation[2]];
                    return true;
                case "eql":
                    if (int.TryParse(operation[2], out numericValue))
                    {
                        memory[operation[1]] = memory[operation[1]] == numericValue ? 1 : 0;
                        return true;
                    }
                    memory[operation[1]] = memory[operation[1]] == memory[operation[2]] ? 1 : 0;
                    return true;
                default:
                    Console.WriteLine("error");
                    Environment.Exit(42);
                    return true;
            }
        }

        private static  List<List<string[]>> ParseInput(string[] lines)
        {
            List<List<string[]>> program = new List<List<string[]>>();
            List<string[]> input = new List<string[]>();
            foreach (var line in lines)
            {
                string[] operation = line.Split(' ');
                if (operation[0] == "inp")
                {
                    if (input.Count > 0)
                    {
                        program.Add(input);
                    }
                    input = new List<string[]>();
                    input.Add(operation);
                    continue;
                }
                input.Add(operation);
            }

            return program;
        }
    }
}