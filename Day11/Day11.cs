using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day11
{
    class Day11
    {
        const string inputPath = @"Day11/Input.txt";

        public static void Task1and2() 
        {
            string[] lines = File.ReadAllLines(inputPath);
            int width = lines[0].Length;

            char[,] prev = new char[lines.Length, width];

            for(int i = 0; i < lines.Length; i++)
            {
                for (int x = 0; x < width; x++)
                {
                    prev[i, x] = lines[i][x];
                }
            }

            char[,] prevTask2 = prev.Clone() as char[,];

            Task1(prev);
            Task2(prevTask2);
        }

        private static void Task1(char[,] prev)
        {
            char[,] next = prev.Clone() as char[,];
            bool changed;

            do
            {
                changed = false;

                for (int y = 0; y < prev.GetLength(0); y++)
                {
                    for (int x = 0; x < prev.GetLength(1); x++)
                    {
                        int adjacentOccupied = 0;

                        if (y - 1 >= 0 && x - 1 >= 0 && prev[y-1, x-1] == '#') adjacentOccupied++;
                        if (y - 1 >= 0 && prev[y-1, x] == '#') adjacentOccupied++;
                        if (y - 1 >= 0 && x + 1 < prev.GetLength(1) && prev[y-1, x+1] == '#') adjacentOccupied++;

                        if (x - 1 >= 0 && prev[y, x-1] == '#') adjacentOccupied++;
                        if (x + 1 < prev.GetLength(1) && prev[y, x+1] == '#') adjacentOccupied++;

                        if (y + 1 < prev.GetLength(0) && x - 1 >= 0 && prev[y+1, x-1] == '#') adjacentOccupied++;
                        if (y + 1 < prev.GetLength(0) && prev[y+1,x] == '#') adjacentOccupied++;
                        if (y + 1 < prev.GetLength(0) && x + 1 < prev.GetLength(1) && prev[y+1,x+1] == '#') adjacentOccupied++;

                        if (prev[y, x] == 'L' && adjacentOccupied == 0) 
                        {
                            next[y, x] = '#';
                            changed = true;
                        }
                        else if (prev[y, x] == '#' && adjacentOccupied >= 4)
                        {
                            next[y, x] = 'L';
                            changed = true;
                        }
                    }
                }

                prev = next.Clone() as char[,];
            } while(changed);

            Console.WriteLine($"Task 1: {prev.Cast<char>().ToArray().Count(c => c == '#')}");
        }

        private static void Task2(char[,] prev)
        {
            char[,] next = prev.Clone() as char[,];
            bool changed;

            do
            {
                changed = false;

                for (int y = 0; y < prev.GetLength(0); y++)
                {
                    for (int x = 0; x < prev.GetLength(1); x++)
                    {
                        int adjacentOccupied = 0;
                        bool[] found = new bool[] { false, false, false, false, false, false, false, false };
                        int searchRadius = 1;

                        while(found.Any(f => f == false))
                        {
                            if (!found[0] && (y - searchRadius < 0 || x - searchRadius < 0)) found[0] = true;
                            if (!found[0] && (prev[y-searchRadius, x-searchRadius] == '#' || prev[y-searchRadius, x-searchRadius] == 'L')) 
                            {
                                if (prev[y-searchRadius, x-searchRadius] == '#') adjacentOccupied++;
                                found[0] = true;
                            } 

                            if (!found[1] && y - searchRadius < 0) found[1] = true;
                            if (!found[1] && (prev[y-searchRadius, x] == '#' || prev[y-searchRadius, x] == 'L')) 
                            {
                                if (prev[y-searchRadius, x] == '#') adjacentOccupied++;
                                found[1] = true;
                            }

                            if (!found[2] && (y - searchRadius < 0 || x + searchRadius >= prev.GetLength(1))) found[2] = true;
                            if (!found[2] && (prev[y-searchRadius, x+searchRadius] == '#' || prev[y-searchRadius, x+searchRadius] == 'L')) 
                            {
                                if (prev[y-searchRadius, x+searchRadius] == '#') adjacentOccupied++;
                                found[2] = true;
                            }


                            if (!found[3] && x - searchRadius < 0) found[3] = true;
                            if (!found[3] && (prev[y, x-searchRadius] == '#' || prev[y, x-searchRadius] == 'L')) 
                            {
                                if (prev[y, x-searchRadius] == '#') adjacentOccupied++;
                                found[3] = true;
                            }

                            if (!found[4] && x + searchRadius >= prev.GetLength(1)) found[4] = true;
                            if (!found[4] && (prev[y, x+searchRadius] == '#' || prev[y, x+searchRadius] == 'L')) 
                            {
                                if (prev[y, x+searchRadius] == '#') adjacentOccupied++;
                                found[4] = true;
                            }


                            if (!found[5] && (y + searchRadius >= prev.GetLength(0) || x - searchRadius < 0)) found[5] = true;
                            if (!found[5] && (prev[y+searchRadius, x-searchRadius] == '#' || prev[y+searchRadius, x-searchRadius] == 'L')) 
                            {
                                if (prev[y+searchRadius, x-searchRadius] == '#') adjacentOccupied++;
                                found[5] = true;
                            }

                            if (!found[6] && y + searchRadius >= prev.GetLength(0)) found[6] = true;
                            if (!found[6] && (prev[y+searchRadius,x] == '#' || prev[y+searchRadius,x] == 'L')) 
                            {
                                if (prev[y+searchRadius,x] == '#') adjacentOccupied++;
                                found[6] = true;
                            }

                            if (!found[7] && (y + searchRadius >= prev.GetLength(0) || x + searchRadius >= prev.GetLength(1))) found[7] = true;
                            if (!found[7] && (prev[y+searchRadius,x+searchRadius] == '#' || prev[y+searchRadius,x+searchRadius] == 'L')) 
                            {
                                if (prev[y+searchRadius,x+searchRadius] == '#') adjacentOccupied++;
                                found[7] = true;
                            }

                            searchRadius++;
                        }

                        if (prev[y, x] == 'L' && adjacentOccupied == 0) 
                        {
                            next[y, x] = '#';
                            changed = true;
                        }
                        else if (prev[y, x] == '#' && adjacentOccupied >= 5)
                        {
                            next[y, x] = 'L';
                            changed = true;
                        }
                    }
                }

                prev = next.Clone() as char[,];

            } while(changed);

            Console.WriteLine($"Task 2: {prev.Cast<char>().ToArray().Count(c => c == '#')}");
        }
    }
}