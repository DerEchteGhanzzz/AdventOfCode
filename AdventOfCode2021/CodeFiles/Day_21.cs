using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace AdventOfCode
{
    public class Day_21
    {
        private static Parser p = new Parser("21");
        private static long p1_winnings;
        private static long p2_winnings;
        private static Dictionary<int, int> waysToRoll;
        public static int solve_A()
        {
            var lines = p.Lines;
            
            Player p1 = new Player(Convert.ToInt32(lines[0].Split(':')[1]));
            Player p2 = new Player(Convert.ToInt32(lines[1].Split(':')[1]));
            
            int die = 1;
            while (p1.Score < 1000 && p2.Score < 1000)
            {
                p1.MoveForwards((die - 1) % 100 + 1 + (die - 1) % 100 + 2 + (die - 1) % 100 + 3);
                die += 3;
                if (p1.Score >= 1000)
                {
                    break;
                }
                p2.MoveForwards((die - 1) % 100 + 1 + (die - 1) % 100 + 2 + (die - 1) % 100 + 3);
                die += 3;
                if (p2.Score >= 1000)
                {
                    break;
                }
            }
            return Math.Min(p2.Score, p1.Score)*(die-1);
        }
        
        public static long solve_B()
        {
            var lines = p.Lines;

            waysToRoll = new Dictionary<int, int>();

            int die = 3;

            for (int i = 1; i < die+1; i++)
            {
                for (int j = 1; j < die+1; j++)
                {
                    for (int k = 1; k < die+1; k++)
                    {
                        if (waysToRoll.ContainsKey(i + j + k))
                        {
                            waysToRoll[i + j + k] += 1;
                            continue;
                        }
                        waysToRoll.Add(i+j+k, 1);
                    }
                }
            }
            
            int p1 = Convert.ToInt32(lines[0].Split(':')[1]);
            int p2 = Convert.ToInt32(lines[1].Split(':')[1]);
            long p1_score = 0;
            long p2_score = 0;
            
            PlayGame(p1, p1_score, p2, p2_score, 0, 1);
            
            return Math.Max(p1_winnings, p2_winnings);
        }

        private static void PlayGame(int p1, long p1_score, int p2, long p2_score, int turn, long different_universes)
        {
            
            if (p2_score >= 21)
            {
                if (turn == 1)
                {
                    p1_winnings += different_universes;
                    return;
                }
                p2_winnings += different_universes;
                return;
            }
            
            foreach (var entry in waysToRoll)
            {
                PlayGame(p2, p2_score, 
                         (p1 + entry.Key - 1) % 10 + 1, p1_score + (p1 + entry.Key - 1) % 10 + 1, 
                         (turn + 1) % 2, different_universes*entry.Value);
            }
            return;
        }
    }

    public class Player
    {
        public int Score;
        public int Position;
        
        public Player(int startingPos)
        {
            this.Position = startingPos;
            this.Score = 0;
        }

        public int MoveForwards(int forwards)
        {
            Position += forwards;
            Position = (Position - 1) % 10 + 1;
            this.Score += Position;
            return Score;
        }
    }
}