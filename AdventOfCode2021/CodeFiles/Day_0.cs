﻿namespace AdventOfCode
{
    public class Day_0
    {
        private static Parser p = new Parser("0");
        public static string solution_A = solve_A();
        public static string solution_B = solve_B();
        private static string solve_A()
        {
            string solution = "";
            var lines = p.Lines;
            
            foreach (string line in lines)
            {
                solution = solution + line +  " ";
            }
            return solution.Remove(solution.Length - 1, 1);
        }
        
        private static string solve_B()
        {
            string solution = "Solutions:";
            var lines = p.Lines;

            return solution;
        }
    }
}