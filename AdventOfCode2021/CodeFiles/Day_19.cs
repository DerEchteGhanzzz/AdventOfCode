using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;

namespace AdventOfCode
{
    public class Day_19
    {
        private static Parser p = new Parser("19");
        private static Scanner godScanner;
        private static List<Scanner> hasHad;

        public static int solve_A()
        {
            var lines = p.Lines;

            List<Scanner> scannerList = ParseInput(lines);
            hasHad = new List<Scanner>();
            godScanner = scannerList[0];
            hasHad.Add(scannerList[0]);
            while (hasHad.Count != scannerList.Count)
            {
                int ARNOLD = hasHad.Count;
                
                for (int i = 1; i < scannerList.Count; i++)
                {
                    
                    if (hasHad.Contains(scannerList[i]))
                    {
                        continue;
                    }
                    // Console.WriteLine(i);
                    AddToGodScanner(scannerList[i]);
                }

                if (ARNOLD == hasHad.Count)
                {
                    System.Environment.Exit(42069);
                }
            }
            // Console.WriteLine("Here:");
            // foreach (var entry in godScanner.possibleBeaconList[0])
            // {
            //     Console.WriteLine(entry);
            // }
            // Console.WriteLine("Stop");
            return godScanner.possibleBeaconList[0].Count;
        }

        public static int solve_B()
        {
            solve_A();
            var lines = p.Lines;
            int max = -1;
            int count = 0;
            foreach (var beacon in hasHad)
            {
                string[] beaconPosArr = beacon.position.Split(',');
                int x1 = Convert.ToInt32(beaconPosArr[0]);
                int y1 = Convert.ToInt32(beaconPosArr[1]);
                int z1 = Convert.ToInt32(beaconPosArr[2]);
                
                count++;
                foreach (var otherBeacon in hasHad)
                {
                    if (otherBeacon == beacon)
                    {
                        continue;
                    }
                    
                    string[] beaconPosArr2 = otherBeacon.position.Split(',');
                    int x2 = Convert.ToInt32(beaconPosArr2[0]);
                    int y2 = Convert.ToInt32(beaconPosArr2[1]);
                    int z2 = Convert.ToInt32(beaconPosArr2[2]);
                    
                    int distance = Math.Abs(x1 - x2) + Math.Abs(y1 - y2) + Math.Abs(z1 - z2);
                    // Console.WriteLine(distance);
                    if (distance > max)
                    {
                        max = distance;
                    }
                    
                }
            }
            
            return max;
        }


        private static void AddToGodScanner(Scanner budgetScanner)
        {
            foreach (var godBeacon in godScanner.possibleBeaconList[0])
            {
                foreach (var rotation in budgetScanner.possibleBeaconList)
                {
                    HashSet<string> finalPos = new HashSet<string>();
                    // Console.WriteLine();
                    int count = 0;
                    foreach (var budgetBeacon in rotation)
                    {
                        
                        foreach (var bonusBeacon in rotation)
                        {
                            if (budgetBeacon == bonusBeacon)
                            {
                                continue;
                            }
                            string[] godBeaconArr = godBeacon.Split(',');
                            string[] bonusBeaconArr = bonusBeacon.Split(',');
                            string[] beaconArr = budgetBeacon.Split(',');
                            string[] endPointArr = new string[3];
                            for (int i = 0; i < 3; i++)
                            {
                                endPointArr[i] = Convert.ToString(-1*Convert.ToInt32(beaconArr[i]) + 
                                                                Convert.ToInt32(godBeaconArr[i]) + 
                                                                Convert.ToInt32(bonusBeaconArr[i]));
                            }
                            
                            string endPoint = endPointArr[0] + "," + endPointArr[1] + "," + endPointArr[2];
                            
                            if (godScanner.possibleBeaconList[0].Contains(endPoint))
                            {
                                // Console.WriteLine("Budget: " + budgetBeacon);
                                // Console.WriteLine(endPoint);
                                // Console.WriteLine(count);
                                count++;
                                int count2 = 0;
                                string beaconPos = "";
                                foreach (var bonusExtraBeacon in rotation)
                                {
                                    string[] endPointArr2 = new string[3];
                                    string[] bonusExtraBeaconArr = bonusExtraBeacon.Split(',');
                                    string[] beaconPosArr = new string[3];
                                    for (int i = 0; i < 3; i++)
                                    {
                                        endPointArr2[i] = Convert.ToString(-1*Convert.ToInt32(beaconArr[i]) + 
                                                                        Convert.ToInt32(godBeaconArr[i]) + 
                                                                        Convert.ToInt32(bonusExtraBeaconArr[i]));
                                        beaconPosArr[i] = Convert.ToString(-1 * Convert.ToInt32(beaconArr[i]) +
                                                                        Convert.ToInt32(godBeaconArr[i]));
                                        // Console.WriteLine(beaconArr[i] + " " + godBeaconArr[i] + " " + bonusExtraBeaconArr[i]);
                                    }
                                    
                                    string finalBeacon = endPointArr2[0] + "," + endPointArr2[1] + "," +
                                                         endPointArr2[2];
                                    beaconPos = beaconPosArr[0] + "," + beaconPosArr[1] + "," +
                                                       beaconPosArr[2];
                                    finalPos.Add(finalBeacon);
                                    if (godScanner.possibleBeaconList[0].Contains(finalBeacon))
                                    {
                                        // Console.WriteLine("hi");
                                        count2++;
                                    }
                                }
                                // Console.WriteLine(count2);
                                if (count2 >= 11)
                                {
                                    // Console.WriteLine("hi");
                                    godScanner.possibleBeaconList[0].UnionWith(finalPos);
                                    Console.WriteLine("Adding Beacon " + hasHad.Count);
                                    budgetScanner.position = beaconPos;
                                    hasHad.Add(budgetScanner);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static List<Scanner> ParseInput(string[] lines)
        {
            List<Scanner> scannerList = new List<Scanner>();
            int count = -1;
            HashSet<string> coordStrings = new HashSet<string>();
            foreach (var line in lines)
            {
                if (line == "")
                {
                    scannerList.Add(new Scanner(count, coordStrings));
                    continue;
                }
                else if (line.Substring(0, 3) == "---")
                {
                    count++;
                    coordStrings = new HashSet<string>();
                }
                else
                {
                    coordStrings.Add(line);
                }
            }

            scannerList.Add(new Scanner(count, coordStrings));
            return scannerList;
        }
    }
}

    public class Scanner
    {
        public List<HashSet<string>> possibleBeaconList;
        private int serial;
        public string position;

        public Scanner(int serial, HashSet<string> beacons)
        {
            this.serial = serial;
            if (serial != 0)
            {
                this.possibleBeaconList = getAllRotations(beacons);
                Console.WriteLine();
                int count = 0;
                // foreach (var set in possibleBeaconList)
                // {
                //     count++;
                //     // Console.WriteLine();
                //     foreach (var str in set)
                //     {
                //         
                //         Console.WriteLine(count+ ": " + str);
                //     }
                // }
            }
            else
            {
                this.position = "0,0,0";
                this.possibleBeaconList= new List<HashSet<string>>() {beacons};
            }
        }

        public List<HashSet<string>> getAllRotations(HashSet<string> beacons)
        {
            this.possibleBeaconList = new List<HashSet<string>>();
            HashSet<string> hasHad = new HashSet<string>();

            for (int i = 0; i < 8; i++)
            {
                var bitString = ToBinary(i);
                
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (k == j)
                        {
                            continue;
                        }
                        for (int l = 0; l < 3; l++)
                        {
                            if (l == k || l == j)
                            {
                                continue;
                            }

                            string hadString = "";
                            HashSet<string> t_beacons = new HashSet<string>();
                            foreach (var beacon in beacons)
                            {
                                string[] beaconArr = beacon.Split(',');
                                string[] newBeaconArr = new string[3];
                                newBeaconArr[0] = beaconArr[j];
                                newBeaconArr[1] = beaconArr[k];
                                newBeaconArr[2] = beaconArr[l];

                                for (int m = 0; m < 3; m++)
                                {
                                    if (bitString[m] == '1')
                                    {
                                        newBeaconArr[m] = Convert.ToString(Convert.ToInt32(newBeaconArr[m])*-1);
                                    } 
                                }

                                hadString = hadString + newBeaconArr[0] + "," + newBeaconArr[1] + "," +
                                            newBeaconArr[2] + " ";
                                t_beacons.Add(newBeaconArr[0] + "," + newBeaconArr[1] + "," + newBeaconArr[2]);
                            }

                            if (hasHad.Add(hadString))
                            {
                                this.possibleBeaconList.Add(t_beacons); 
                            }
                        }
                    }
                }
            }
            
            return possibleBeaconList;
        }
        
        private string ToBinary(int x)
        {
            int len = 3;
            char[] buff = new char[len];
 
            for (int i = len-1; i >= 0 ; i--)
            {
                int mask = 1 << i;
                buff[len-1 - i] = (x & mask) != 0 ? '1' : '0';
            }
 
            return new string(buff);
        }
    }