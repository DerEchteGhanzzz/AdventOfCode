using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;

namespace AdventOfCode
{
    public class Day_15
    {
        private static Parser p = new Parser("15");

        public static int solve_A()
        {
            var lines = p.Lines;
            
            var cave = GetGrid(lines);
            
            return SolveCave(cave);
        }
        
        public static int solve_B()
        {
            var lines = p.Lines;
            
            var cave = GetBigGrid(lines);

            return SolveCave(cave);
        }

        public static int[,] GetGrid(string[] lines)
        {
            int[,] cave = new int[lines[0].Length, lines.Length];
            
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    cave[x, y] = Convert.ToInt32(char.GetNumericValue(lines[y][x]));
                }
            }

            return cave;
        }
        
        public static int[,] GetBigGrid(string[] lines)
        {
            var cave = GetGrid(lines);
            
            int[,] bigCave = new int[cave.GetLength(0)*5, cave.GetLength(1)*5];
            
            for (int y = 0; y < cave.GetLength(1)*5; y++)
            {
                for (int x = 0; x < cave.GetLength(0)*5; x++)
                {

                    int plus = (x / cave.GetLength(0)) + (y / cave.GetLength(1));

                    bigCave[x, y] = ((cave[x % cave.GetLength(0), y % cave.GetLength(1)] + plus - 1) % 9 + 1);

                }
            }
            
            return bigCave;
        }

        private static int SolveCave(int[,] cave)
        {
            Node start = CreateNodes(cave);

            return DijkstraToEnd(start, new []{cave.GetLength(0) - 1, cave.GetLength(1) - 1});
        }
        
        private static int DijkstraToEnd(Node start, int[] target)
        {
            Dictionary<Node, int> distanceDict = new Dictionary<Node, int>() {{start, 0}};
            start.InDict = true;
            Dictionary<Node, int> queue = new Dictionary<Node, int>() {{start, 0}};;

            while (queue.Count > 0)
            {
                Node currentNode = queue.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
                
                if (currentNode.x == target[0] && currentNode.y == target[1])
                {
                    // var end = currentNode;
                    // while (end.x != 0 || end.y != 0)
                    // {
                    //     Console.WriteLine(end.x + ", " + end.y + ": " + end.Value + "; " + distanceDict[end]);
                    //     end = end.Previous;
                    // }
                    return distanceDict[currentNode];
                }
                
                currentNode.Visited = true;
                queue.Remove(currentNode);

                foreach (var neigh in currentNode.Neighs)
                {

                    if (neigh.Visited)
                    {
                        continue;
                    }

                    if (!neigh.InDict)
                    { 
                        distanceDict.Add(neigh, neigh.Value + distanceDict[currentNode]);
                        neigh.Previous = currentNode;
                        neigh.InDict = true;
                    }

                    if (distanceDict[currentNode] + neigh.Value < distanceDict[neigh])
                    {
                        neigh.Previous = currentNode;
                        distanceDict[neigh] = distanceDict[currentNode] + neigh.Value;
                    }

                    if (queue.ContainsKey(neigh))
                    {
                        queue[neigh] = distanceDict[neigh];
                    }
                    else
                    {
                        queue.Add(neigh, distanceDict[neigh]);
                    }
                    // AddToQueue(neigh, queue, distanceDict);
                }
                
            }
            
            return -1;
        }

        private static void AddToQueue(Node node, LinkedList<Node> queue, Dictionary<Node, int> distanceDict)
        {
            if (queue.Count == 0)
            {
                queue.AddFirst(node);
                return;
            }

            var nextNode = queue.First;
            while (true)
            {
                if (distanceDict[node] < distanceDict[nextNode.Value])
                {
                    queue.AddBefore(nextNode, node);
                    return;
                } else if (nextNode.Next == null)
                {
                    queue.AddLast(node);
                    return;
                }
                nextNode = nextNode.Next;
            }
        }

        private static Node CreateNodes(int[,] cave)
        {
            Node[,] grid = new Node[cave.GetLength(0), cave.GetLength(1)];
            for (int x = 0; x < cave.GetLength(0); x++)
            {
                for (int y = 0; y < cave.GetLength(1); y++)
                {
                    grid[x, y] = new Node(cave[x, y], x, y);
                }
            }

            for (int x = 0; x < cave.GetLength(0); x++)
            {
                for (int y = 0; y < cave.GetLength(1); y++)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (x + i >= cave.GetLength(0) || x + i < 0 || y + j >= cave.GetLength(1)
                                || y + j < 0 || Math.Abs(i) == Math.Abs(j))
                            {
                                // Console.WriteLine(i + ", " + j);
                                
                                continue;
                            }

                            grid[x, y].Neighs.Add(grid[x + i, y + j]);
                        }
                    }
                }
            }

            return grid[0, 0];

        }
    }

    public class Node
    {
        public int x;
        public int y;
        private int value;
        private bool visited;
        public List<Node> Neighs;
        private int distance;
        public bool InDict;
        private Node previous;

        public Node Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        public int Value
        {
            get { return value; }
        }

        public bool Visited
        {
            get { return visited; }
            set { visited = value; }
        }

        public Node(int value, int x, int y)
        {
            this.value = value;
            visited = false;
            Neighs = new List<Node>();
            this.x = x;
            this.y = y;
            this.InDict = false;
        }
    }
}