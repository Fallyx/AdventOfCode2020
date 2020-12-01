using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day01
{
    class Day01
    {
        const string inputPath = @"Day01/Input.txt";
        public static void Task1()
        {
            List<int> numbers = new List<int>();
            bool foundFirst = false;
            bool foundSecond = false;

            using (StreamReader reader = new StreamReader(inputPath)) 
            {
                string line;
                while((line = reader.ReadLine()) != null) 
                {
                    numbers.Add(int.Parse(line));
                }
            }

            for (int i = 0; i < numbers.Count - 1; i++) 
            {
                for (int x = 1; x < numbers.Count; x++) 
                {
                    if (!foundFirst && numbers[i] + numbers[x] == 2020) 
                    {
                        Console.WriteLine($"Task1: {numbers[i] * numbers[x]}");
                        foundFirst = true;
                    }

                    for(int y = 2; y < numbers.Count; y++) 
                    {
                        if (!foundSecond && numbers[i] + numbers[x] + numbers[y] == 2020)
                        {
                            Console.WriteLine($"Task 2: {numbers[i] * numbers[x] * numbers[y]}");
                            foundSecond = true;
                        }

                        if (foundFirst && foundSecond) 
                        {
                            return;
                        }
                    }
                }
            }
        }
    }    
}