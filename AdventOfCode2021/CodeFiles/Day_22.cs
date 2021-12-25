using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

namespace AdventOfCode
{
    public class Day_22
    {
        private static Parser p = new Parser("22");

        public static int solve_A()
        {
            var lines = p.Lines;

            List<string[]> instructions =  ParseInput(lines);

            Dictionary<string, bool> reactorCore = new Dictionary<string, bool>();

            foreach (var instruct in instructions)
            {
                int x1 = Convert.ToInt32(instruct[1].Split(',')[0]);
                int x2 = Convert.ToInt32(instruct[2].Split(',')[0]);
                
                int y1 = Convert.ToInt32(instruct[1].Split(',')[1]);
                int y2 = Convert.ToInt32(instruct[2].Split(',')[1]);
                
                int z1 = Convert.ToInt32(instruct[1].Split(',')[2]);
                int z2 = Convert.ToInt32(instruct[2].Split(',')[2]);
                
                if (x1 < -50 || x1 > 50 || x2 < -50 || x2 > 50|| y1 < -50 || y1 > 50|| y2 < -50 || y2 > 50
                    || z1 < -50 || z1 > 50|| z2 < -50 || z2 > 50)
                {
                    continue;
                }
                
                bool on = false;
                
                if (instruct[0] == "on")
                {
                    on = true;
                }
                
                for (int i = x1; i <= x2; i++)
                {

                    for (int j = y1; j <= y2; j++)
                    {

                        for (int k = z1; k <= z2; k++)
                        {

                            string curCoord = i + "," + j + "," + k;
                            if (reactorCore.ContainsKey(curCoord))
                            {
                                if (on)
                                {
                                    reactorCore[curCoord] = on;
                                }
                                else
                                {
                                    reactorCore.Remove(curCoord);
                                }
                                
                            }
                            else
                            {
                                if (on)
                                {
                                    reactorCore.Add(curCoord, on);
                                }
                            }
                        }
                    }
                }
            }
            
            return reactorCore.Count;
        }
        
        public static long solve_B()
        {
            var lines = p.Lines;

            List<string[]> instructions =  ParseInput(lines);

            List<Cube> cubeList = new List<Cube>();
            
            foreach (var instruct in instructions)
            {
                Cube cube = new Cube(instruct);
                if (cubeList.Count == 0 && cube.On)
                {
                    cubeList.Add(cube);
                    continue;
                }
                
                // Console.WriteLine();
                // Console.WriteLine(instruct[0] + " " + instruct[1] + "; " + instruct[2]);
                // Console.WriteLine();
                
                List<Cube> newCubes = new List<Cube>();
                long netto = 0;

                if (cube.On)
                {
                    newCubes.Add(cube);
                }
                
                foreach (var coreCube in cubeList)
                {
                    if (!coreCube.On)
                    {
                        continue;
                    }
                    // Console.WriteLine(coreCube.initialInstructions[0] + " " + coreCube.initialInstructions[1] + "; " + coreCube.initialInstructions[2]);
                    Cube intersection = IntersectionArea(coreCube, cube);
                    intersection.volume *= -1*Math.Sign(coreCube.volume);
                    if (intersection.On)
                    {
                        newCubes.Add(intersection);
                    }

                }
                cubeList.AddRange(newCubes);
            }
            
            long end = 0;
            foreach (var cube in cubeList)
            {
                if (cube.On)
                {
                    end += cube.volume;
                }
            }
            
            return end;
        }
        private static Cube IntersectionArea(Cube cubeA, Cube cubeB)
        {

            if (!(cubeA.x2 >= cubeB.x1 && cubeA.x1 <= cubeB.x2 && cubeA.y2 >= cubeB.y1 && cubeA.y1 <= cubeB.y2 &&
                cubeA.z2 >= cubeB.z1 && cubeA.z1 <= cubeB.z2))
            {
                string[] stop = new[] {"off", "0,0,0", "0,0,0"};
                // Console.WriteLine("exit");
                return new Cube(stop);
            }
            
            if (cubeA.x1 < cubeB.x1 && cubeB.x2 < cubeA.x2 && cubeA.y1 < cubeB.y1 && cubeB.y2 < cubeA.y2 &&
                cubeA.z1 < cubeB.z1 && cubeB.z2 < cubeA.z2)
            {
                Cube cubeBIncubeA = new Cube(cubeB.initialInstructions);
                cubeBIncubeA.On = true;
                return cubeBIncubeA;
            }
            
            if (cubeA.x1 > cubeB.x1 && cubeB.x2 > cubeA.x2 && cubeA.y1 > cubeB.y1 && cubeB.y2 > cubeA.y2 &&
                cubeA.z1 > cubeB.z1 && cubeB.z2 > cubeA.z2)
            {
                cubeA.On = false;
                string[] stop = new[] {"off", "0,0,0", "0,0,0"};
                // Console.WriteLine("exit2");
                return new Cube(stop);
            }

            if (cubeB.volume == 1)
            {
                var theSameCube = new Cube(cubeB.initialInstructions);
                theSameCube.On = true;
                return theSameCube;
            }
            
            // Console.WriteLine(cubeA.x1 + "," + cubeA.y1 + "," + cubeA.z1 + "; " + cubeA.x2 + "," + cubeA.y2 + "," + cubeA.z2);
            int[] intersect1 = new int[3];
            int[] intersect2 = new int[3];
            
            if (cubeA.x2 >= cubeB.x1 && cubeB.x1 >= cubeA.x1)
            {
                intersect1[0] = cubeB.x1;
            }
            else
            {
                intersect1[0] = cubeA.x1;
            }
            
            if (cubeA.x2 >= cubeB.x2 && cubeB.x2 >= cubeA.x1)
            {
                intersect2[0] = cubeB.x2;
            }
            else
            {
                intersect2[0] = cubeA.x2;
            }
            
            if (cubeA.y2 >= cubeB.y1 && cubeB.y1 >= cubeA.y1)
            {
                intersect1[1] = cubeB.y1;
            }
            else
            {
                intersect1[1] = cubeA.y1;
            }
            // Console.WriteLine(cubeA.y2 + ", " + cubeB.y2 + ", " + cubeA.y1);
            if (cubeA.y2 >= cubeB.y2 && cubeB.y2 >= cubeA.y1)
            {
                
                intersect2[1] = cubeB.y2;
            }
            else
            {
                intersect2[1] = cubeA.y2;
            }
            
            if (cubeA.z2 >= cubeB.z1 && cubeB.z1 >= cubeA.z1)
            {
                intersect1[2] = cubeB.z1;
            }
            else
            {
                intersect1[2] = cubeA.z1;
            }
            
            if (cubeA.z2 >= cubeB.z2 && cubeB.z2 >= cubeA.z1)
            {
                intersect2[2] = cubeB.z2;
            }
            else
            {
                intersect2[2] = cubeA.z2;
            }

            string intersectX1 = Math.Min(intersect1[0],intersect2[0]) + "," + Math.Min(intersect1[1],intersect2[1]) + "," + Math.Min(intersect1[2],intersect2[2]);
            string intersectX2 = Math.Max(intersect1[0],intersect2[0]) + "," + Math.Max(intersect1[1],intersect2[1]) + "," + Math.Max(intersect1[2],intersect2[2]);
            string[] instruction = new[] {"on", intersectX1, intersectX2};
            Cube intersection = new Cube(instruction);
            // Console.WriteLine("Intersection");
            // Console.WriteLine(intersectX1 + "; " + intersectX2);
            // Console.WriteLine(intersection.volume);

            return intersection;
        }

        private static List<string[]> ParseInput(string[] lines)
        {
            List<string[]> instructions = new List<string[]>();

            foreach (var line in lines)
            {
                string[] coordsArr = new string[3];
                coordsArr[0] = line.Split(' ')[0];
                var co_ords = line.Split(' ')[1];

                string coord1 = "";
                string coord2 = "";

                foreach (var coord in co_ords.Split(','))
                {
                    coord1 = coord1 + coord.Split(new char[] {'.', '='})[1] + ",";
                    
                    coord2 = coord2 + coord.Split(new char[] {'.', '='})[3] + ",";
                }

                coordsArr[1] = coord1.Substring(0, coord1.Length-1);
                coordsArr[2] = coord2.Substring(0, coord2.Length-1);
                
                instructions.Add(coordsArr);
            }

            return instructions;
        }
    }

    class Cube
    {
        private List<int[]> corners;

        public long volume;

        public int x1;
        public int x2;

        public int y1;
        public int y2;
        
        public int z1;
        public int z2;
        
        public bool On;

        public string[] initialInstructions;
        
        public Cube(string[] instruct)
        {

            this.initialInstructions = instruct;
            
            this.On = this.initialInstructions[0] == "on" ? true : false;

            this.SetCorners();
            
            this.CalcVolume();
        }

        private void SetCorners()
        {
            this.corners = new List<int[]>();
            this.x1 = Convert.ToInt32(this.initialInstructions[1].Split(',')[0]);
            this.x2 = Convert.ToInt32(this.initialInstructions[2].Split(',')[0]);
            int[] x = new[] {x1, x2};
            this.y1 = Convert.ToInt32(this.initialInstructions[1].Split(',')[1]);
            this.y2 = Convert.ToInt32(this.initialInstructions[2].Split(',')[1]);
            int[] y = new[] {y1, y2};
            this.z1 = Convert.ToInt32(this.initialInstructions[1].Split(',')[2]);
            this.z2 = Convert.ToInt32(this.initialInstructions[2].Split(',')[2]);
            int[] z = new[] {z1, z2};
            
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        int[] corner = new int[3];
                        corner[0] = x[i];
                        corner[1] = y[j];
                        corner[2] = z[k];

                        this.x1 = x[i] < x1 ? x[i] : x1;
                        this.y1 = y[j] < y1 ? y[j] : y1;
                        this.z1 = z[k] < z1 ? z[k] : z1;
                        
                        this.x2 = x[i] > x2 ? x[i] : x2;
                        this.y2 = y[j] > y2 ? y[j] : y2;
                        this.z2 = z[k] > z2 ? z[k] : z2;
                        
                        this.corners.Add(corner);
                        // Console.WriteLine(corner[0]+","+corner[1]+","+corner[2]);
                    }
                }
            }
        }

        private void CalcVolume()
        {
            // Console.WriteLine(x1+","+y1+","+z1 + "; "+x2+","+y2+","+z2 );
            this.volume = ((long)Math.Abs(x1 - x2)+1) * (long)(Math.Abs(y1 - y2)+1) * (long)(Math.Abs(z1 - z2)+1);
        }
    }
}