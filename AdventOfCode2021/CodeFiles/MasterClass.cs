using System;
using System.CodeDom;
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
            
            output.Add(Day_0.solution_A);
            output.Add(Day_0.solution_B);
            
            output.Add("Day 1 A: " + Day_1.solution_A);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 1 B: " + Day_1.solution_B);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            output.Add("Day 2 A: " + Day_2.solution_A);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 2 B: " + Day_2.solution_B);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            output.Add("Day 3 A: " + Day_3.solution_A);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 3 B: " + Day_3.solution_B);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            output.Add("Day 4 A: " + Day_4.solution_A);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 4 B: " + Day_4.solution_B);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            output.Add("Day 5 A: " + Day_5.solution_A);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 5 B: " + Day_5.solution_B);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            output.Add("Day 6 A: " + Day_6.solution_A);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 6 B: " + Day_6.solution_B);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            output.Add("Day 7 A: " + Day_7.solution_A);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 7 B: " + Day_7.solution_B);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            output.Add("Day 8 A: " + Day_8.solution_A);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 8 B: " + Day_8.solution_B);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            output.Add("Day 9 A: " + Day_9.solution_A);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 9 B: " + Day_9.solution_B);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
            output.Add("Day 10 A: " + Day_10.solution_A);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            output.Add("Day 10 B: " + Day_10.solution_B);
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);
            
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
