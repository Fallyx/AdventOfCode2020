using System;
using System.Diagnostics;

namespace AdventOfCode2020
{
    class Program
    {
        private static Stopwatch swTot = new Stopwatch();
        private static Stopwatch swDay = new Stopwatch();

        static void Main(string[] args)
        {
            Console.WriteLine("Run a single day [1-25] or [a]ll:");
            String input = Console.ReadLine();

            if (input == "a") {
                FullRun();
            } 

            int day;
            bool success = int.TryParse(input, out day);

            if (!success) {
                return;
            }

            SingleRun(day);
        }

        private static void FullRun() 
        {
            swTot.Start();

            #region day 1
            swDay.Start();
            Day01.Day01.Task1();
            swDay.Stop();
            Console.WriteLine($"Day 01 elapsed time: {swDay.Elapsed}\n");
            #endregion

            swTot.Stop();
            Console.WriteLine($"\nTotal elapsed time: {swTot.Elapsed}");
        }

        private static void SingleRun(int day) 
        {
            swDay.Start();

            switch (day)
            {
                case 1:
                    Day01.Day01.Task1();
                    break;  
                default:
                    break;
            }

            swDay.Stop();
            Console.WriteLine($"Day {day.ToString("D2")} elapsed time: {swDay.Elapsed}\n");
        }
    }
}
