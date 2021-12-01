using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Parser
    {   
        private string path;
        private string day;
        public string[] Lines;
        private string TextFileLocation = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName,"AdventOfCode2021/TextFiles");
        
        public Parser(string day)
        {
            this.day = day;
            this.path = TextFileLocation;
            if (day != "-1")
                ParseLines();
        }
        
        private void ParseLines()
        {
            
            this.Lines = System.IO.File.ReadAllLines(path:this.path + "/Day_" + day + ".txt");

            return;
        }

        public void WriteOutput(List<string> output)
        {
            TextWriter tw = new StreamWriter(this.path+"/Output.txt");
        
            foreach (String s in output)
                tw.WriteLine(s);

            tw.Close();
        }
        
        public static List<int> StringArrToListInt(string[] list)
        {
            List<int> IntList = new List<int>();
            
            foreach (var item in list)
            {
                IntList.Add(Int32.Parse(item));
            }

            return IntList;
        }
    }
}