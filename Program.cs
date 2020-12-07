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
            string input;
            if (args.Length != 0)
            {
                input = args[0];
            }
            else
            {
                Console.WriteLine("Run a single day [1-25] or [a]ll:");
                input = Console.ReadLine();
                Console.WriteLine("---------------------------------\n");
            }
            

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

            #region day 01
            swDay.Start();
            Day01.Day01.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 01 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 02
            swDay.Restart();
            Day02.Day02.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 02 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 03
            swDay.Restart();
            Day03.Day03.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 03 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 04
            swDay.Restart();
            Day04.Day04.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 04 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 05
            swDay.Restart();
            Day05.Day05.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 05 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 06
            swDay.Restart();
            Day06.Day06.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 06 elapsed time: {swDay.Elapsed}\n");
            #endregion

            #region day 07
            swDay.Restart();
            Day07.Day07.Task1and2();
            swDay.Stop();
            Console.WriteLine($"Day 07 elapsed time: {swDay.Elapsed}\n");
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
                    Day01.Day01.Task1and2();
                    break;
                case 2:
                    Day02.Day02.Task1and2();
                    break;
                case 3:
                    Day03.Day03.Task1and2();
                    break;
                case 4:
                    Day04.Day04.Task1and2();
                    break;
                case 5:
                    Day05.Day05.Task1and2();
                    break;
                case 6:
                    Day06.Day06.Task1and2();
                    break;
                case 7:
                    Day07.Day07.Task1and2();
                    break;
                default:
                    break;
            }

            swDay.Stop();
            Console.WriteLine($"Day {day.ToString("D2")} elapsed time: {swDay.Elapsed}\n");
        }
    }
}
