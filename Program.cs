using System;
using System.Diagnostics;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        private static void FullRun() 
        {
            Stopwatch swTot = new Stopwatch();
            Stopwatch swDay = new Stopwatch();

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

        private static void SingleRun() 
        {

        }
    }
}
