using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Schema;
using Microsoft.Win32.SafeHandles;

namespace AdventOfCode
{
    public class Day_18
    {
        private static Parser p = new Parser("18");
        private static bool explosionNeeded;
        public static long solve_A()
        {
            var lines = p.Lines;
            
            string SFString = lines[0];
            // Console.WriteLine(SFString);
            SFString = Reduce(SFString);

            for (int i = 1; i < lines.Length; i++)
            {
                // break;
                // Console.WriteLine("NEW SUM!");
                SFString = "[" + SFString + "," + lines[i] + "]";
                SFString = Reduce(SFString);
            }
            
            // Console.WriteLine("Sum: " + SFString);

            return CalcMagnitude(SFString);
        }

        public static long solve_B()
        {
            var lines = p.Lines;
            long maxMagnitude = -1;
            foreach (var line in lines)
            {
                foreach (var line2 in lines)
                {
                    if (line == line2)
                    {
                        continue;
                    }
                    string SFString = "[" + line + "," + line2 + "]";
                    SFString = Reduce(SFString);
                    long curMag = CalcMagnitude(SFString);
                    if (maxMagnitude < curMag)
                    {
                        maxMagnitude = curMag;
                    }
                }
                
            }
            
            return maxMagnitude;
        }

        private static string Reduce(string SFString)
        {
            string newSFString = SFString;
            bool first = true;
            explosionNeeded = true;
            while (newSFString != SFString || first)
            {
                first = false;
                SFString = newSFString;

                if (explosionNeeded)
                {
                    newSFString = ExplodeAll(SFString);
                }

                newSFString = Split(newSFString);
            }

            return newSFString;
        }

        private static string ExplodeAll(string SFString)
        {
            string newSFString = SFString;
            bool first = true;
            while (newSFString != SFString || first)
            {
                int bracketCount = 0;
                first = false;
                SFString = newSFString;

                for (int pointer = 0; pointer < SFString.Length; pointer++)
                {
                    if (SFString[pointer] == '[')
                    {
                        bracketCount++;
                    } 
                    else if (SFString[pointer] == ']')
                    {
                        bracketCount--;
                    }
                    if (bracketCount > 4 && SFString[pointer] == '[')
                    {
                        newSFString = Explode(pointer, newSFString);
                        break;
                    }
                }
                
            }

            explosionNeeded = false;
            return newSFString;
            }

        private static string Explode(int pointer, string SFString)
        {
            string newSFString = SFString;

            string setToZero = "";
            for (int i = pointer; i < SFString.Length; i++)
            {
                setToZero = setToZero + SFString[i];
                if (SFString[i] == ']')
                {
                    break;
                }
            }
            // Console.WriteLine(setToZero);
            // foreach (var item in setToZero.Split(','))
            // {
            //     Console.WriteLine(item);    
            // }
            int x = Convert.ToInt32(setToZero.Split(',')[0].Substring(1));
            int y = Convert.ToInt32(setToZero.Split(',')[1].Substring(0, setToZero.Split(',')[1].Length-1));

            string left = SFString.Substring(0, pointer);
            string middleLeft = "";
            bool foundInt = false;
            string leftAdd = "";
            for (int i = pointer; i >= 0; i--)
            {
                if (char.IsDigit(SFString[i]))
                {
                    int len = 1;
                    if (char.IsDigit(SFString[i-1]))
                    {
                        i--;
                        len = 2;
                    }

                    middleLeft = SFString.Substring(i + len, pointer - (i + len));
                    leftAdd = Convert.ToString(Convert.ToInt32(SFString.Substring(i, len)) + x );
                    left = SFString.Substring(0, i);
                    break;
                }
            }
            
            string middleRight = "";
            foundInt = false;
            string rightAdd = "";
            string right = SFString.Substring(pointer+setToZero.Length);
            for (int i = pointer+setToZero.Length; i < SFString.Length; i++)
            {
                if (char.IsDigit(SFString[i]))
                {
                    int len = 1;
                    if (char.IsDigit(SFString[i+1]))
                    {
                        len = 2;
                    }
                    // Console.WriteLine((pointer+setToZero.Length) + " " +  (i-len-pointer-setToZero.Length));
                    middleRight = SFString.Substring(pointer+setToZero.Length, (i-pointer-setToZero.Length) );
                    rightAdd = Convert.ToString(Convert.ToInt32(SFString.Substring(i, len)) + y );
                    right = SFString.Substring(i + len);
                    break;
                }
            }
            // Console.WriteLine(newSFString);
            newSFString = left + "" + leftAdd + "" + middleLeft + "0" + middleRight + "" + rightAdd + "" + right;
            // Console.WriteLine("After Explode: "+newSFString);
            return newSFString;

        }

        private static string Split(string SFString)
        {
            string newSFString = SFString;
            int bracketCount = 0;
            for (int pointer = 0; pointer < SFString.Length; pointer++)
            {
                if (SFString[pointer] == '[')
                {
                    bracketCount++;
                } 
                else if (SFString[pointer] == ']')
                {
                    bracketCount--;
                }
                if (char.IsDigit(SFString[pointer]) && char.IsDigit(SFString[pointer + 1]))
                {
                    // Console.WriteLine(SFString[pointer] + "" + SFString[pointer+1]);

                    explosionNeeded = true;
                    int digit = Convert.ToInt32(SFString.Substring(pointer, 2));
                    string splitNumbers = "[" + (digit / 2) + "," + (int) Math.Ceiling((double) digit / 2) + "]";
                    SFString = SFString.Substring(0, pointer) + splitNumbers + SFString.Substring(pointer+2);
                    break;
                }
            }
            // Console.WriteLine(SFString);
            return SFString;
        }
        
        private static long CalcMagnitude(string localSF)
        {
            long magnitude = 0;
            int localBracketCount = 0;

            if (localSF.Length == 1)
            {
                return Convert.ToInt32(localSF);
            }

            localSF = localSF.Substring(1, localSF.Length - 2);

            if (localSF.Length == 3)
            {
                return 3 * (int) char.GetNumericValue(localSF[0]) + 2 * (int) char.GetNumericValue(localSF[2]);
            }

            string firstHalf = "";
            string secondHalf = "";

            for (int i = 0; i < localSF.Length; i++)
            {
                if (localBracketCount == 0 && localSF[i] == ',')
                {
                    secondHalf = localSF.Substring(i + 1);
                    break;
                }

                firstHalf = firstHalf + localSF[i];

                if (localSF[i] == '[')
                {
                    localBracketCount++;
                }
                else if (localSF[i] == ']')
                {
                    localBracketCount--;
                }

            }

            // Console.WriteLine(firstHalf + "; " + secondHalf);
            magnitude += 3 * CalcMagnitude(firstHalf);
            magnitude += 2 * CalcMagnitude(secondHalf);

            return magnitude;
        }
    }
}