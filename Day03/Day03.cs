using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day03
{
    class Day03
    {
        const string inputPath = @"Day03/Input.txt";

        public static void Task1and2() 
        {
            var lines = File.ReadAllLines(inputPath);

            int[] xs = new int[5];
            int y = 0;
            int[] treesEncountered = new int[5];
            int[,] slopes = new int[,]
            {
                {1,1},
                {3,1},
                {5,1},
                {7,1},
                {1,2}
            };

            while (y < lines.Length - 1)
            {
                for (int i = 0; i < xs.Length; i++)
                {
                    if (y % slopes[i,1] != 0) continue;
                    xs[i] += slopes[i,0];
                    if (xs[i] >= lines[y].Length) 
                    {
                        xs[i] -= lines[y].Length;
                    }
                }
                
                y++;

                for (int i = 0; i < xs.Length; i++) 
                {
                    if (y % slopes[i,1] != 0) continue;
                    if (lines[y][xs[i]] == '#') 
                    {
                        treesEncountered[i]++;
                    }
                }
            }

            Console.WriteLine($"Task 1: {treesEncountered[1]}");
            long task2 = 1;
            for (int i = 0; i < treesEncountered.Length; i++)
            {
                task2 *= treesEncountered[i];
            }
            Console.WriteLine($"Task 2: {task2}");
        }
    }
}