using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day06
{
    class Day06
    {
        const string inputPath = @"Day06/Input.txt";

        public static void Task1and2()
        {
            Dictionary<char, int> groupQuestions = new Dictionary<char, int>();
            int sumYes = 0;
            int sumAllYes = 0;
            int countPersons = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while((line = reader.ReadLine()) != null)
                {
                    if(line.Length == 0)
                    {
                        sumYes += groupQuestions.Count;
                        foreach(KeyValuePair<char, int> kv in groupQuestions)
                        {
                            if (kv.Value == countPersons) sumAllYes++;
                        }
                        groupQuestions.Clear();
                        countPersons = 0;
                        continue;
                    }

                    countPersons++;

                    foreach(char c in line)
                    {
                        if (!groupQuestions.ContainsKey(c))
                        {
                            groupQuestions.Add(c, 1);
                        }
                        else 
                        {
                            groupQuestions[c]++;
                        }
                    }
                }

                sumYes += groupQuestions.Count;
                foreach(KeyValuePair<char, int> kv in groupQuestions)
                {
                    if (kv.Value == countPersons) sumAllYes++;
                }

                Console.WriteLine($"Task 1: {sumYes}");
                Console.WriteLine($"Task 2: {sumAllYes}");
            }
        }
    }
}