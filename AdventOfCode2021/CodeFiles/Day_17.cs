using System;
using System.Xml.Schema;

namespace AdventOfCode
{
    public class Day_17
    {
        // private static Parser p = new Parser("17");
        private static int xMinTarget = 265;
        private static int xMaxTarget = 287;
        private static int yMinTarget = -103;
        private static int yMaxTarget = -58;
        
        public static int solve_A()
        {
            return Math.Abs(yMinTarget+1)*(Math.Abs(yMinTarget+1)+1)/2;
        }
        
        public static int solve_B()
        {
            return ShootProbe()[1];
        }

        public static int[] ShootProbe()
        {

            int yMax = 0;
            int total = 0;

            int n = (int) Math.Floor(-1 * Math.Sqrt(1 + 8 * xMinTarget) / 2);

            for (int i = n; i <= xMaxTarget; i++)
            {
                for (int j = yMinTarget; -(j+1) >= yMinTarget; j++)
                {
                    int hori = i;
                    int vert = j;
                    int x = 0;
                    int y = 0;

                    while (x <= xMaxTarget && y >= yMinTarget)
                    {
                        // Console.WriteLine(x + ", " + y);
                        x += hori;
                        hori -= Math.Sign(hori);
                        
                        y += vert;
                        vert -= 1;
                        
                        if (x <= xMaxTarget && x >= xMinTarget && y <= yMaxTarget && y >= yMinTarget)
                        {
                            total += 1;
                            break;
                        }
                    }
                    
                    if (j > yMax)
                    {
                        yMax = j;
                    }
                }
            }

            int highestY = 0;
            while (yMax > 0)
            {
                highestY += yMax;
                yMax -= 1;
            }
            
            return new []{highestY, total};
        }
    }
}