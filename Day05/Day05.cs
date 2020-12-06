using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day05
{
    class Day05
    {
        const string inputPath = @"Day05/Input.txt";

        public static void Task1and2()
        {
            int highestSeatId = 0;
            List<int> seatIds = new List<int>();

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string seat;
                while ((seat = reader.ReadLine()) != null)
                {
                    /*  This approach takes longer than my CalculateRowCol method
                        string seatIdstr = Regex.Replace(seat, "(F|L)", "0");
                        seatIdstr = Regex.Replace(seatIdstr, "(B|R)", "1");
                        int seatId = Convert.ToInt32(seatIdstr, 2);
                    */

                    int row = CalulateRowCol(seat, 0, 7, 0, 127);
                    int col = CalulateRowCol(seat, 7, 10, 0, 7);

                    int seatId = row * 8 + col;

                    seatIds.Add(seatId);

                    if (seatId > highestSeatId) highestSeatId = seatId;
                }
            }

            Console.WriteLine($"Task 1: {highestSeatId}");

            seatIds.Sort();

            for(int i = 0; i < seatIds.Count - 1; i++)
            {
                if(seatIds[i+1] - seatIds[i] != 1)
                {
                    Console.WriteLine($"Task 2: {seatIds[i] + 1}");
                    break;
                }
            }
        }

        private static int CalulateRowCol(string seat, int start, int end, int min, int max)
        {
            int found = 0;
            for(int i = start; i < end; i++)
            {
                if (seat[i] == 'F' || seat[i] == 'L')
                {
                    max = (int)Math.Floor((double)(min + max) / 2);
                    found = min;
                }
                else if (seat[i] == 'B' || seat[i] == 'R')
                {
                    min = (int)Math.Ceiling((double)(min + max) / 2);
                    found = max;
                }
            }

            return found;
        }
    }
}