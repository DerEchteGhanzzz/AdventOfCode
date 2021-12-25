using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;

namespace AdventOfCode
{
    public class Day_4
    {
        private static Parser p = new Parser("4");
        public static int solve_A()
        {
            var lines = p.Lines;
            string[] CallArray = lines[0].Split(',');
            
            List<BingoSheet> SheetList = CreateSheetList(lines);

            foreach (string currentCall in CallArray)
            {
                int CallNumb = Int32.Parse(currentCall);

                foreach (var bingoSheet in SheetList)
                {
                    bingoSheet.Call(CallNumb);
                    if (bingoSheet.Bingo)
                    {
                        return bingoSheet.CountScore();
                    }
                }
            }
            
            return 0;
        }
        
        public static int solve_B()
        {
            var lines = p.Lines;
            string[] CallArray = lines[0].Split(',');
            
            List<BingoSheet> SheetList = CreateSheetList(lines);
            List<BingoSheet> WonSheets = new List<BingoSheet>();
            foreach (string currentCall in CallArray)
            {
                int CallNumb = Int32.Parse(currentCall);

                foreach (var bingoSheet in SheetList)
                {
                    if (bingoSheet.Bingo)
                    {
                        continue;
                    }
                    bingoSheet.Call(CallNumb);
                    if (bingoSheet.Bingo)
                    {
                        if (SheetList.Count-1 > WonSheets.Count)
                        {
                            WonSheets.Add(bingoSheet);
                        }
                        else
                        {
                            return bingoSheet.CountScore();
                        }
                    }
                }
            }
            
            return 0;
        }
        
        private static List<BingoSheet> CreateSheetList(string[] lines)
        {
            List<BingoSheet> SheetList = new List<BingoSheet>();
            Dictionary<string, int> current_sheet = new Dictionary<string, int>();
            int y = 0;
            int x = 0;
            for (int i = 2; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    SheetList.Add(new BingoSheet(current_sheet, x, y));
                    current_sheet = new Dictionary<string, int>();
                    // Console.WriteLine();
                    y = 0;
                }
                else
                {
                    x = 0;
                    foreach (string number in lines[i].Split(' '))
                    {
                        if (number == "")
                        {
                            continue;
                        }
                        
                        string current_coord = x+","+y;
                        current_sheet.Add(current_coord, Int32.Parse(number));
                        // Console.WriteLine(current_coord);
                        x += 1;
                    }
                    y += 1;
                }

            }
            SheetList.Add(new BingoSheet(current_sheet, x, y));
            return SheetList;
        }
    }

    public class BingoSheet
    {
        public Dictionary<string, int> sheet;
        private int width;
        private int height;
        private int last;
        private List<int> hasHad;
        private int bingoInt;
        public bool Bingo;

        public int Width
        {
            set { width = value; }
        }

        public int Height
        {
            set { height = value; }
        }

        public int Last
        {
            get { return last; }
        }

        public int BingoInt
        {
            set { bingoInt = value; }
        }

        public BingoSheet(Dictionary<string, int> sheet, int width, int height)
        {
            this.sheet = sheet;
            this.Bingo = false;
            this.hasHad = new List<int>();
            this.bingoInt = 5;
            this.width = width;
            this.height = height;
        }

        public void Call(int current)
        {
            this.last = current;
            hasHad.Add(current);
            this.CheckBingo();
        }

        public void CheckBingo()
        {
            bool Present = false;
            int[] coords = new int[2];
            foreach (KeyValuePair<string, int> entry in this.sheet)
            {
                if (entry.Value == last)
                {
                    Present = true;
                    coords[0] = Int32.Parse(entry.Key.Split(',')[0]);
                    coords[1] = Int32.Parse(entry.Key.Split(',')[1]);
                }
            }

            if (RowChecker(coords, true) | RowChecker(coords, false))
            {
                this.Bingo = true;
            }
        }

        private bool RowChecker(int[] coords, bool hori)
        {
            int count = 0;
            int i;
            int exstreme;
            if (hori)
            {
                exstreme = this.width;
                i = coords[0];
            }
            else
            {
                exstreme = this.height;
                i = coords[1];
            }
            
            for (int x = i - 4; x <= i + 4; x++)
            {
                if (x < 0 | x > exstreme-1)
                {
                    // Console.WriteLine(x);
                    continue;
                }

                string current_coords;
                if (hori)
                {
                    current_coords = x + ","+coords[1];
                }
                else
                {
                    current_coords = coords[0] + "," + x;
                }
                
                // Console.WriteLine(current_coords + ", " + this.last);
                if (this.hasHad.Contains(this.sheet[current_coords]))
                {
                    count += 1;
                    if (count == this.bingoInt)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }

            }

            return false;
        }

        public int CountScore()
        {
            int sum = 0;
            foreach (KeyValuePair<string, int> entry in this.sheet)
            {
                if (hasHad.Contains(entry.Value))
                {
                    continue;
                }
                else
                {
                    sum += entry.Value;
                }
            }

            return sum * this.last;
        }
    }
}