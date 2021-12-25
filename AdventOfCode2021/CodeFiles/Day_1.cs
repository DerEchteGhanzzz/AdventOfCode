using System;

namespace AdventOfCode
{
    public class Day_1
    {
        private static Parser p = new Parser("1");

        public static int solve_A()
        {
            var depthList = Parser.StringArrToListInt(p.Lines);
            int increase = 0;
            for (int i = 0; i < depthList.Count; i++)
            {
                if (i == 0)
                {
                    continue;
                }

                if (depthList[i] > depthList[i - 1])
                {
                    increase++;
                }
            }
            
            return increase;
        }
        
        public static int solve_B()
        {
            var depthList = Parser.StringArrToListInt(p.Lines);
            int increase = 0;
            for (int i = 0; i < depthList.Count; i++)
            {
                if (i < 3 || i == depthList.Count - 1)
                {
                    continue;
                }

                if (depthList[i] + depthList[i-1] + depthList[i-2] < depthList[i - 1] + depthList[i] + depthList[i + 1])
                {
                    increase++;
                }
            }
            
            return increase;
        }
    }
}