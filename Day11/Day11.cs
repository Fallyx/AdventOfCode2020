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

            Seats(prev, 4);
            Seats(prev, 5, false);
        }

        private static void Seats(char[,] prev, int freeSeat, bool onlyAdj = true)
        {
            char[,] next = prev.Clone() as char[,];
            bool changed;
            (bool found, int occupied) result;

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
                            
                            if (!found[0])
                            {
                                result = IsOccupied(prev, y - searchRadius, x - searchRadius, onlyAdj);
                                found[0] = result.found;
                                adjacentOccupied += result.occupied;
                            }
                            if (!found[1])
                            {
                                result = IsOccupied(prev, y - searchRadius, x, onlyAdj);
                                found[1] = result.found;
                                adjacentOccupied += result.occupied;
                            }
                            if (!found[2])
                            {
                                result = IsOccupied(prev, y - searchRadius, x + searchRadius, onlyAdj);
                                found[2] = result.found;
                                adjacentOccupied += result.occupied;
                            }

                            if (!found[3])
                            {
                                result = IsOccupied(prev, y, x - searchRadius, onlyAdj);
                                found[3] = result.found;
                                adjacentOccupied += result.occupied;
                            }
                            if (!found[4])
                            {
                                result = IsOccupied(prev, y, x + searchRadius, onlyAdj);
                                found[4] = result.found;
                                adjacentOccupied += result.occupied;
                            }

                            if (!found[5])
                            {
                                result = IsOccupied(prev, y + searchRadius, x - searchRadius, onlyAdj);
                                found[5] = result.found;
                                adjacentOccupied += result.occupied;
                            }
                            if (!found[6])
                            {
                                result = IsOccupied(prev, y + searchRadius, x, onlyAdj);
                                found[6] = result.found;
                                adjacentOccupied += result.occupied;
                            }
                            if (!found[7])
                            {
                                result = IsOccupied(prev, y + searchRadius, x + searchRadius, onlyAdj);
                                found[7] = result.found;
                                adjacentOccupied += result.occupied;
                            }

                            searchRadius++;
                        }

                        if (prev[y, x] == 'L' && adjacentOccupied == 0) 
                        {
                            next[y, x] = '#';
                            changed = true;
                        }
                        else if (prev[y, x] == '#' && adjacentOccupied >= freeSeat)
                        {
                            next[y, x] = 'L';
                            changed = true;
                        }
                    }
                }

                prev = next.Clone() as char[,];
            } while(changed);

            Console.WriteLine($"Task {(onlyAdj ? 1 : 2)}: {prev.Cast<char>().ToArray().Count(c => c == '#')}");
        }

        private static (bool found, int occupied) IsOccupied(char[,] seats, int y, int x, bool onlyAdj)
        {
            if (y < 0 || x < 0 || y >= seats.GetLength(0) || x >= seats.GetLength(1))
                return (true, 0);
            else if (seats[y, x] == '#') 
                return (true, 1);
            else if (seats[y, x] == 'L' || onlyAdj) 
                return (true, 0);
            else 
                return (false, 0);
        }
    }
}