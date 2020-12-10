using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day09
{
    class Day09
    {
        const string inputPath = @"Day09/Input.txt";
        const int preamble = 25;

        public static void Task1and2()
        {
            Queue numbers = new Queue();
            List<int> numbersTask2 = new List<int>();

            int invalidNum = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;

                while((line = reader.ReadLine()) != null)
                {
                    int num = Int32.Parse(line);
                    numbersTask2.Add(num);

                    if (numbers.Count < preamble)
                    {
                        numbers.Enqueue(num);
                        continue;
                    }

                    var numArray = numbers.ToArray();

                    var result = numArray.Select((n1, idx) =>
                        new {n1, n2 = numArray.Take(idx).FirstOrDefault(
                            n2 => (int)n1 + (int)n2 == num
                        )})
                        .Where(pair => (pair.n1 != pair.n2) && (pair.n1 != null && pair.n2 != null));

                    if (result.Count() == 0)
                    {
                        invalidNum = num;
                        Console.WriteLine($"Task 1: {invalidNum}");
                        break;
                    }

                    numbers.Dequeue();
                    numbers.Enqueue(num);
                }
            }

            for(int i = 0; i < numbersTask2.Count; i++)
            {
                int x = i;
                int num = numbersTask2[i];
                int min = num;
                int max = num;

                while (num < invalidNum)
                {
                    num += numbersTask2[++x];
                    min = Math.Min(min, numbersTask2[x]);
                    max = Math.Max(max, numbersTask2[x]);
                }

                if (num == invalidNum)
                {
                    Console.WriteLine($"Task 2: {min + max}");
                    break;
                }
            }
        }
    }
}