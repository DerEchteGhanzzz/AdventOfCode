using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;

namespace AdventOfCode
{
    class MasterClass
    {
        static void Main(string[] args)
        {
            bool WriteOutput = false;

            List<string> output = new List<string>();
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();

            // output.Add(Day_0.solve_A());
            // output.Add(Day_0.solve_B());
            //
            // output.Add("Day 1 A: " + Day_1.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 1 B: " + Day_1.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 2 A: " + Day_2.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 2 B: " + Day_2.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 3 A: " + Day_3.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 3 B: " + Day_3.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 4 A: " + Day_4.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 4 B: " + Day_4.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 5 A: " + Day_5.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 5 B: " + Day_5.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 6 A: " + Day_6.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 6 B: " + Day_6.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 7 A: " + Day_7.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 7 B: " + Day_7.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 8 A: " + Day_8.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 8 B: " + Day_8.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 9 A: " + Day_9.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 9 B: " + Day_9.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 10 A: " + Day_10.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 10 B: " + Day_10.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 11 A: " + Day_11.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 11 B: " + Day_11.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            // output.Add("Day 15 A: " + Day_15.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 15 B: " + Day_15.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            // output.Add("Day 16 A: " + Day_16.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 16 B: " + Day_16.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            // output.Add("Day 17 A: " + Day_17.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 17 B: " + Day_17.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            // output.Add("Day 18 A: " + Day_18.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 18 B: " + Day_18.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            // output.Add("Day 19 A: " + Day_19.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 19 B: " + Day_19.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            //
            // output.Add("Day 20 A: " + Day_20.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 20 B: " + Day_20.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            // output.Add("Day 21 A: " + Day_21.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 21 B: " + Day_21.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            // output.Add("Day 22 A: " + Day_22.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 22 B: " + Day_22.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            // output.Add("Day 23 A: " + Day_23.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 23 B: " + Day_23.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            output.Add("Day 24 A: " + Day_24.solve_A());
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 24 B: " + Day_24.solve_B());
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            // output.Add("Day 25 A: " + Day_25.solve_A());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            // output.Add("Day 25 B: " + Day_25.solve_B());
            // output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            stopWatch.Stop();
            output.Add("total ms: " + stopWatch.Elapsed.TotalMilliseconds);

            foreach (string line in output)
            {
                Console.WriteLine(line);
            }
            
            if (WriteOutput)
            {
                Parser parser = new Parser("-1");
                parser.WriteOutput(output);
            }
            
        }
    }
}
