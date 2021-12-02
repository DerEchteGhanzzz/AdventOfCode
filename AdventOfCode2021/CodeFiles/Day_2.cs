using System;

namespace AdventOfCode
{
    public class Day_2
    {
        private static Parser p = new Parser("2");
        public static int solution_A = solve_A();
        public static int solution_B = solve_B();
        
        private static int solve_A()
        {
            var lines = p.Lines;
            int depth = 0;
            int hori = 0;
            foreach (string line in lines)
            {
                string[] current = line.Split(' ');
                if (current[0] == "forward")
                {
                    hori += Int32.Parse(current[1]);
                } else if (current[0] == "down")
                {
                    depth += Int32.Parse(current[1]);
                } else if (current[0] == "up")
                {
                    depth -= Int32.Parse(current[1]);
                }
            }
            return hori*depth;
        }
        
        private static int solve_B()
        {
            var lines = p.Lines;
            int aim = 0;
            int depth = 0;
            int hori = 0;
            foreach (string line in lines)
            {
                string[] current = line.Split(' ');
                if (current[0] == "forward")
                {
                    hori += Int32.Parse(current[1]);
                    depth += aim*Int32.Parse(current[1]);
                } else if (current[0] == "down")
                {
                    //depth += Int32.Parse(current[1]);
                    aim += Int32.Parse(current[1]);
                } else if (current[0] == "up")
                {
                    //depth -= Int32.Parse(current[1]);
                    aim -= Int32.Parse(current[1]);
                }
            }
            return hori*depth;
            return 0;
        }
        
    }
}