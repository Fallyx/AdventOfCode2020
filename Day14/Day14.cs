using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day14
{
    class Day14
    {
        const string inputPath = @"Day14/Input.txt";
        public static void Task1and2()
        {
            string[] lines = File.ReadAllLines(inputPath);
            string mask = "";
            Dictionary<string, long> memory = new Dictionary<string, long>();
            Dictionary<string, long> memoryTask2 = new Dictionary<string, long>();
            int amountX = 0;
            int permutations = 0;

            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    // Part 1
                    mask = line.Split(" = ")[1];
                    // Part 2
                    amountX = 0;
                    foreach(char c in mask) 
                    {
                        if (c == 'X') amountX++;
                    }
                    permutations = (int)Math.Pow(2, amountX);
                    continue;
                }
                
                string[] memVal = Regex.Matches(line, "\\d+").OfType<Match>().Select(m => m.Groups[0].Value).ToArray();

                // Part 1
                if (!memory.ContainsKey(memVal[0])) memory.Add(memVal[0], 0);

                int tmp = Int32.Parse(memVal[1]);
                char[] resultVal = Convert.ToString(tmp, 2).PadLeft(36, '0').ToCharArray();

                for(int i = 0; i < mask.Length; i++)
                {
                    if (mask[i] == 'X') continue;
                    resultVal[i] = mask[i];
                }
                
                // Part 2
                tmp = Int32.Parse(memVal[0]);
                char[] resultMem = Convert.ToString(tmp, 2).PadLeft(36, '0').ToCharArray();

                for(int i = 0; i < permutations; i++)
                {
                    char[] memId = new char[mask.Length];
                    char[] bits = Convert.ToString(i, 2).PadLeft((int)amountX, '0').ToCharArray();
                    int bitIdx = 0;

                    for (int x = 0; x < mask.Length; x++)
                    {
                        if (mask[x] == '0') memId[x] = resultMem[x];
                        else if (mask[x] == '1') memId[x] = '1';
                        else
                        {
                            memId[x] = bits[bitIdx];
                            bitIdx++;
                        }
                    }

                    string memIdStr = new string(memId);

                    if (!memoryTask2.ContainsKey(memIdStr)) memoryTask2.Add(memIdStr, Int32.Parse(memVal[1]));
                    else memoryTask2[memIdStr] = Int32.Parse(memVal[1]);
                }

                memory[memVal[0]] = Convert.ToInt64(new string(resultVal), 2);
            }

            Console.WriteLine($"Task 1: {memory.Sum(v => v.Value)}");
            Console.WriteLine($"Task 2: {memoryTask2.Sum(v => v.Value)}");
        }
    }
}