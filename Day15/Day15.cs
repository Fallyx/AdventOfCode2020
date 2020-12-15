using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day15
{
    class Day15
    {
        const string inputPath = @"Day15/Input.txt";

        public static void Task1and2() 
        {
            int turns = 1;
            Dictionary<int, int> numberSpoken = new Dictionary<int, int>();

            foreach(string line in File.ReadAllLines(inputPath)[0].Split(','))
            {
                numberSpoken.Add(Int32.Parse(line), turns++);
            }

            playMemory(new Dictionary<int, int>(numberSpoken), turns, 2020);
            playMemory(numberSpoken, turns, 30000000);
        }

        private static void playMemory(Dictionary<int, int> numberSpoken, int turns, int end)
        {
            int lastNum = 0;

            while(turns != end)
            {
                if (!numberSpoken.ContainsKey(lastNum))
                {
                    numberSpoken.Add(lastNum, turns);
                    lastNum = 0;
                }
                else
                {
                    int lastTurn = numberSpoken[lastNum];
                    numberSpoken[lastNum] = turns;
                    lastNum = turns - lastTurn;
                }
                
                turns++;
            }

            Console.WriteLine($"Task {(end == 2020 ? 1 : 2)}: {lastNum}");
        }
    }
}