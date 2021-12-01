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
            bool WriteOutput = true;

            List<string> output = new List<string>();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            output.Add("Day 0 A: " + Day_0.solution_A);
            output.Add("Day 0 B: " + Day_0.solution_B);
            
            output.Add("Day 1 A: " + Day_1.solution_A);
            output.Add("Day 1 B: " + Day_1.solution_B);
            
            stopWatch.Stop();
            output.Add("ms: " + stopWatch.Elapsed.TotalMilliseconds);

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
