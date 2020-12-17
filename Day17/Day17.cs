using System;
using System.IO;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day17
{
    class Day17
    {
        const string inputPath = @"Day17/Input.txt";

        public static void Task1and2()
        {
            HashSet<Vector3> currentCycle = new HashSet<Vector3>();
            HashSet<Vector3> nextCycle = new HashSet<Vector3>();
            HashSet<Vector4> currentCycleT2 = new HashSet<Vector4>();
            HashSet<Vector4> nextCycleT2 = new HashSet<Vector4>();
            int cycles = 1;
            string[] lines = File.ReadAllLines(inputPath);

            int startXLength = lines[0].Length;
            int startYLength = lines.Length;

            for(int y = 0; y < startYLength; y++)
            {
                for(int x = 0; x < startXLength; x++)
                {
                    if( lines[y][x] == '#')
                    {
                        currentCycle.Add(new Vector3(x, y, 0));
                        currentCycleT2.Add(new Vector4(x, y, 0, 0));
                    }
                }
            }

            while(cycles <= 6)
            {
                nextCycle.Clear();
                nextCycleT2.Clear();

                for (float x = -cycles; x <= startXLength + cycles; x++)
                {
                    for (float y = -cycles; y <= startYLength + cycles; y++)
                    {
                        for (float z = -cycles; z <= cycles; z++)
                        {
                            Vector3 coord = new Vector3(x, y, z);
                            int neighbors = GetNeighborsCount(currentCycle, coord);
                            if (currentCycle.Contains(coord) && (neighbors == 2 || neighbors == 3)) nextCycle.Add(coord);
                            else if (neighbors == 3) nextCycle.Add(coord);

                            for (float w = -cycles; w <= cycles; w++)
                            {
                                Vector4 coordT2 = new Vector4(x, y, z, w);
                                neighbors = GetNeighborsCount(currentCycleT2, coordT2);
                                if (currentCycleT2.Contains(coordT2) && (neighbors == 2 || neighbors == 3)) nextCycleT2.Add(coordT2);
                                else if (neighbors == 3) nextCycleT2.Add(coordT2);
                            }
                        }
                    }
                }

                currentCycle = new HashSet<Vector3>(nextCycle);
                currentCycleT2 = new HashSet<Vector4>(nextCycleT2);
                cycles++;
            }

            Console.WriteLine($"Task 1: {currentCycle.Count}");
            Console.WriteLine($"Task 2: {currentCycleT2.Count}");
        }

        private static int GetNeighborsCount(HashSet<Vector3> currentCycle, Vector3 cube)
        {
            int neighbors = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        if (x == 0 && y == 0 && z == 0) continue;
                        if (currentCycle.Contains(new Vector3(cube.X + x, cube.Y + y, cube.Z + z))) neighbors++;
                    }
                } 
            }

            return neighbors;
        }

        private static int GetNeighborsCount(HashSet<Vector4> currentCycle, Vector4 cube)
        {
            int neighbors = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        for (int w = -1; w <= 1; w++)
                        {
                            if (x == 0 && y == 0 && z == 0 && w == 0) continue;
                            if (currentCycle.Contains(new Vector4(cube.X + x, cube.Y + y, cube.Z + z, cube.W + w))) neighbors++;
                        }
                    }
                }
            }

            return neighbors;
        }
    }
}