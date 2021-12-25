using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;

namespace AdventOfCode
{
    public class Day_16
    {
        private static int pointer = 0;
        private static string bitString = "";
        private static int versionCount = 0;
        private static Parser p = new Parser("16");

        public static long solve_A()
        {
            var lines = p.Lines;

            var byteArray = ParseBytes(lines);
            
            return versionCount;
        }
        
        public static long solve_B()
        {
            pointer = 0;
            bitString = "";
            versionCount = 0;
            var lines = p.Lines;
            
            return ParseBytes(lines);
        }

        public static Dictionary<string, string> GetHexDict()
        {
            return new Dictionary<string, string>()
            {
                {"0", "0000"},
                {"1", "0001"},
                {"2", "0010"},
                {"3", "0011"},
                {"4", "0100"},
                {"5", "0101"},
                {"6", "0110"},
                {"7", "0111"},
                {"8", "1000"},
                {"9", "1001"},
                {"A", "1010"},
                {"B", "1011"},
                {"C", "1100"},
                {"D", "1101"},
                {"E", "1110"},
                {"F", "1111"},
            };
        }

        public static long ParseBytes(string[] lines)
        {
            var hexDict = GetHexDict();

            bitString = "";
            foreach (var ch in lines[0])
            {
                var bit = ch + "";
                bitString = bitString + hexDict[bit];
            }
            
            // byteList.Add(ManagePackets(curByte, -1));
            
            return GetParsedBitstring();
        }

        public static long GetParsedBitstring()
        {
            int version = Convert.ToInt32(bitString.Substring(pointer, 3), 2);
            pointer += 3;
            versionCount += version;
            int opCode = Convert.ToInt32(bitString.Substring(pointer, 3), 2);
            pointer += 3;
            
            if (opCode == 4)
            {
                return ParseValue();
            }
            
            int type = Convert.ToInt32(bitString.Substring(pointer, 1));
            pointer += 1;

            List<long> valueList = new List<long>();
            
            if (type == 0)
            {
                int nextPointerPosition = pointer + 15 + Convert.ToInt32(bitString.Substring(pointer, 15), 2);
                pointer += 15;
                
                valueList = new List<long>();

                while (pointer < nextPointerPosition)
                {
                    valueList.Add(GetParsedBitstring());
                    
                }

                return calculateResult(valueList, opCode);
            }
            
            int subpacketCount = Convert.ToInt32(bitString.Substring(pointer, 11), 2);
            pointer += 11;
            
            valueList = new List<long>();

            for (int count = 0; count < subpacketCount; count++)
            {
                valueList.Add(GetParsedBitstring());
            }
            
            return calculateResult(valueList, opCode);
        }

        private static long ParseValue()
        {
            string packetValue = "";
            
            while (true)
            {
                int next = Convert.ToInt32(bitString.Substring(pointer, 1));
                pointer += 1;

                packetValue = packetValue + bitString.Substring(pointer, 4);
                pointer += 4;
                
                if (next == 0)
                {
                    return Convert.ToInt64(packetValue, 2);
                }
            }
        }

        private static long calculateResult(List<long> valueList, int opCode)
        {
            switch (opCode)
            {
                case 0:
                    return valueList.Sum();
                case 1:
                    return valueList.Aggregate((x, y) => x * y);
                case 2:
                    return valueList.Min();
                case 3:
                    return valueList.Max();
                case 5:
                    return valueList[0] > valueList[1] ? 1 : 0;
                case 6:
                    return valueList[0] < valueList[1] ? 1 : 0;
                case 7:
                    return valueList[0] == valueList[1] ? 1 : 0;
                default:
                    Console.WriteLine("ERROR");
                    return 0;
            }
        }
    }
}