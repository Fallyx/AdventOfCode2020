using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    class Day10
    {
        const string inputPath = @"Day10/Input.txt";

        public static void Task1and2()
        {
            string[] lines = File.ReadAllLines(inputPath);

            List<int> adapters = lines.ToList().ConvertAll(x => Int32.Parse(x));
            adapters.Sort();
            adapters.Add(adapters.Max() + 3);

            int joltage = 0;
            int oneDiff = 0;
            int threeDiff = 0;
            Dictionary<int, long> arrangements = adapters.ToDictionary(k => k, v => 0L);

            arrangements.Add(0, 1);

            foreach(int adapter in adapters)
            {
                if (adapter == joltage + 1) oneDiff++; 
                else if (adapter == joltage + 3) threeDiff++; 

                joltage = adapter;

                arrangements[adapter] += arrangements.GetValueOrDefault(adapter - 1) + arrangements.GetValueOrDefault(adapter - 2) + arrangements.GetValueOrDefault(adapter - 3);
            }

            Console.WriteLine($"Task 1: {oneDiff * threeDiff}");
            Console.WriteLine($"Task 2: {arrangements[adapters.Max()]}");
        }
    }
}