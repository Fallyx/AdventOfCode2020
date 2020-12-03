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

            int x = 0;
            int[] ys = new int[5];
            int[] treesEncountered = new int[5];
            int[,] slopes = new int[,]
            {
                {1,1},
                {1,3},
                {1,5},
                {1,7},
                {2,1}
            };

            while (x < lines.Length - 1)
            {
                for (int i = 0; i < ys.Length; i++)
                {
                    if (x % slopes[i,0] != 0) continue;
                    ys[i] += slopes[i,1];
                    if (ys[i] >= lines[x].Length) 
                    {
                        ys[i] -= lines[x].Length;
                    }
                }
                
                x++;

                for (int i = 0; i < ys.Length; i++) 
                {
                    if (x % slopes[i,0] != 0) continue;
                    if (lines[x][ys[i]] == '#') 
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