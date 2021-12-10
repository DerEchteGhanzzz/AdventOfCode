using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace AdventOfCode
{
    public class Day_10
    {
        private static Parser p = new Parser("10");
        public static int solution_A = solve_A();
        public static long solution_B = solve_B();
        
        private static int solve_A()
        {
            var lines = p.Lines;
            int total = 0;
            List<string> incompleteLines = new List<string>();
            Dictionary<char, int> pointsDict = new Dictionary<char, int>()
            {
                {')', 3},
                {']', 57},
                {'}', 1197},
                {'>', 25137}
            };
            
            foreach (var line in lines)
            {
                List<char> excpectedSyntax = new List<char>();
                foreach (var syntax in line.ToCharArray())
                {

                    if (syntax == '(')
                    {
                        excpectedSyntax.Add(')');
                    } else if (syntax == '<')
                    {
                        excpectedSyntax.Add('>');
                    }  else if (syntax == '{')
                    {
                        excpectedSyntax.Add('}');
                    }  else if (syntax == '[')
                    {
                        excpectedSyntax.Add(']');
                    } else if (syntax == ')' || syntax == ']' || syntax == '}' || syntax == '>')
                    {
                        if (excpectedSyntax[excpectedSyntax.Count-1] == syntax)
                        {
                            excpectedSyntax.RemoveAt(excpectedSyntax.Count-1);
                        }
                        else
                        {
                            // Console.WriteLine(excpectedSyntax[0] + " expected, but found " + syntax);
                            // Console.WriteLine(line);
                            total += pointsDict[syntax];
                            break;
                        }
                    }
                }
            }
            return total;
        }
        
        private static long solve_B()
        {
            var lines = p.Lines;
            var incompleteLines = RemoveCorruptedLines(lines);
            List<long> total = new List<long>();

            Dictionary<char, int> pointsDict = new Dictionary<char, int>()
            {
                {')', 1},
                {']', 2},
                {'}', 3},
                {'>', 4}
            };
            
            foreach (var line in incompleteLines)
            {
                List<char> excpectedSyntax = new List<char>();
                foreach (var syntax in line.ToCharArray())
                {

                    if (syntax == '(')
                    {
                        excpectedSyntax.Add(')');
                    } else if (syntax == '<')
                    {
                        excpectedSyntax.Add('>');
                    }  else if (syntax == '{')
                    {
                        excpectedSyntax.Add('}');
                    }  else if (syntax == '[')
                    {
                        excpectedSyntax.Add(']');
                    } else if (syntax == ')' || syntax == ']' || syntax == '}' || syntax == '>')
                    {
                        if (excpectedSyntax[excpectedSyntax.Count-1] == syntax)
                        {
                            excpectedSyntax.RemoveAt(excpectedSyntax.Count-1);
                        }
                        else
                        {
                            Console.WriteLine("SYNTAX ERROR");
                            break;
                        }
                    }
                }

                string autocomplete = "";
                long currentTotal = 0;
                for (int i = excpectedSyntax.Count-1; i >= 0; i--)
                {
                    // Console.WriteLine(currentTotal + ": " + currentTotal*5 + " + " + pointsDict[excpectedSyntax[i]]);
                    currentTotal = currentTotal * 5 + pointsDict[excpectedSyntax[i]];
                }
                // Console.WriteLine(currentTotal);
                total.Add(currentTotal);

            }

            long middle = 0;
            total.Sort();
            for (int i = 0; i < (total.Count+1)/2; i++)
            {
                middle = total[i];
            }
            return middle;
        }

        private static List<string> RemoveCorruptedLines(string[] lines)
        {
            List<string> incompleteLines = new List<string>();

            foreach (var line in lines)
            {
                incompleteLines.Add(line);
                List<char> excpectedSyntax = new List<char>();
                foreach (var syntax in line.ToCharArray())
                {
                    
                    if (syntax == '(')
                    {
                        excpectedSyntax.Add(')');
                    } else if (syntax == '<')
                    {
                        excpectedSyntax.Add('>');
                    }  else if (syntax == '{')
                    {
                        excpectedSyntax.Add('}');
                    }  else if (syntax == '[')
                    {
                        excpectedSyntax.Add(']');
                    } else if (syntax == ')' || syntax == ']' || syntax == '}' || syntax == '>')
                    {
                        if (excpectedSyntax[excpectedSyntax.Count-1] == syntax)
                        {
                            excpectedSyntax.RemoveAt(excpectedSyntax.Count-1);
                        }
                        else
                        {
                            // Console.WriteLine(excpectedSyntax[0] + " expected, but found " + syntax);
                            // Console.WriteLine(line);
                            incompleteLines.Remove(line);
                            break;
                        }
                    }
                }
            }
            return incompleteLines;
        }
    }
}