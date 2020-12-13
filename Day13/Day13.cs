using System;
using System.IO;

namespace AdventOfCode2020.Day13
{
    class Day13
    {
        const string inputPath = @"Day13/Input.txt";

        public static void Task1and2()
        {
            string[] lines = File.ReadAllLines(inputPath);

            int startTime = Int32.Parse(lines[0]);

            string[] bussesStr = lines[1].Split(',');
            int minWait = Int32.MaxValue;
            int busID = 0;

            foreach (string bus in bussesStr)
            {
                if (bus == "x") continue;

                int busDep = Int32.Parse(bus);
                int earliestDeparture = startTime;
                while (earliestDeparture % busDep != 0)
                {
                    earliestDeparture++;
                }

                if (minWait > earliestDeparture - startTime)
                {
                    minWait = earliestDeparture - startTime;
                    busID = busDep;
                }
            }

            Console.WriteLine($"Task 1: {minWait * busID}");

            int idx = 0;
            long result = 1;
            long multiplier = 1;
            foreach (string bus in bussesStr)
            {
                if (bus != "x")
                {
                    busID = Int32.Parse(bus);
                    bool found = false;
                    long tmpResult = result;
                    while (!found)
                    {
                        found = ((tmpResult += multiplier) + idx) % busID == 0;
                    }
                    result = tmpResult;
                    multiplier *= busID;
                }
                idx++;
            }
            
            Console.WriteLine($"Task 2: {result}");
        }
    }
}