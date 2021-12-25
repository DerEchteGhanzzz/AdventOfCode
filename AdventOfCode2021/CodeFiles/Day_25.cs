using System;
using System.Xml.Schema;

namespace AdventOfCode
{
    public class Day_25
    {
        private static Parser p = new Parser("25");
        private static int moves;
        public static int solve_A()
        {
            var lines = p.Lines;
            
            char[,] cucumberGrid = new char[lines[0].Length, lines.Length];
            
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    cucumberGrid[x, y] = lines[y][x];
                }
            }
            
            // Console.WriteLine();
            // for (int i = 0; i < cucumberGrid.GetLength(1); i++)
            // {
            //     for (int j = 0; j < cucumberGrid.GetLength(0); j++)
            //     {
            //         Console.Write(cucumberGrid[j,i]);
            //     }
            //     Console.Write("\n");
            // }
            
            int count = 0;
            moves = -1;
            while (moves != 0)
            {
                moves = 0;
                cucumberGrid = MoveSeacucumbers("east", cucumberGrid);
                // Console.WriteLine(count);
                // for (int i = 0; i < cucumberGrid.GetLength(1); i++)
                // {
                //     for (int j = 0; j < cucumberGrid.GetLength(0); j++)
                //     {
                //         Console.Write(cucumberGrid[j,i]);
                //     }
                //     Console.Write("\n");
                // }
                cucumberGrid = MoveSeacucumbers("south", cucumberGrid);
                
                // Console.WriteLine(count);
                // for (int i = 0; i < cucumberGrid.GetLength(1); i++)
                // {
                //     for (int j = 0; j < cucumberGrid.GetLength(0); j++)
                //     {
                //         Console.Write(cucumberGrid[j,i]);
                //     }
                //     Console.Write("\n");
                // }
                count++;
            }
            
            return count;
        }
        
        public static int solve_B()
        {
            var lines = p.Lines;

            return 0;
        }

        public static char[,] MoveSeacucumbers(string direction, char[,] cucumberGrid)
        {
            char[,] newGrid = new char[cucumberGrid.GetLength(0), cucumberGrid.GetLength(1)];
            
            for (int y = 0; y < cucumberGrid.GetLength(1); y++)
            {
                for (int x = 0; x < cucumberGrid.GetLength(0); x++)
                {
                    // if ((cucumberGrid[x, y] == 'v' && direction == "east") || 
                    //     (cucumberGrid[x, y] == '>' && direction == "south"))
                    // {
                    //     newGrid[x, y] = cucumberGrid[x, y];
                    //     continue;
                    // }
                    
                    if (newGrid[x, y] != '\0')
                    {
                        continue;
                    }
                    
                    if (direction == "east")
                    {
                        if (cucumberGrid[(x + 1) % cucumberGrid.GetLength(0), y] == '.' && cucumberGrid[x,y] == '>')
                        {
                            newGrid[(x + 1) % cucumberGrid.GetLength(0), y] = '>';
                            newGrid[x, y] = '.';
                            
                            moves++;
                            continue;
                        }
                        // Console.WriteLine(cucumberGrid[x,y]);
                        newGrid[x, y] = cucumberGrid[x,y];
                        continue;
                    }
                    
                    if (cucumberGrid[x, (y + 1) % cucumberGrid.GetLength(1)] == '.' && cucumberGrid[x,y] == 'v')
                    {
                        newGrid[x, (y + 1) % cucumberGrid.GetLength(1)] = 'v';
                        newGrid[x, y] = '.';
                        moves++;
                        continue;
                    }
                        
                    newGrid[x, y] = cucumberGrid[x,y];;
                    continue;
                    
                }
            }
            return newGrid;
        }
    }
}