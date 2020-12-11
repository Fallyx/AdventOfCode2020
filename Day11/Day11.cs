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
                            if (!found[0]) 
                            {
                                if (prev[y-searchRadius, x-searchRadius] == '#') 
                                {
                                    adjacentOccupied++;
                                    found[0] = true;
                                }
                                else if (prev[y-searchRadius, x-searchRadius] == 'L' || onlyAdj) 
                                    found[0] = true;
                            } 

                            if (!found[1] && y - searchRadius < 0) found[1] = true;
                            if (!found[1]) 
                            {
                                if (prev[y-searchRadius, x] == '#') 
                                {
                                    adjacentOccupied++;
                                    found[1] = true;
                                }
                                else if (prev[y-searchRadius, x] == 'L' || onlyAdj)
                                    found[1] = true;
                            }

                            if (!found[2] && (y - searchRadius < 0 || x + searchRadius >= prev.GetLength(1))) found[2] = true;
                            if (!found[2]) 
                            {
                                if (prev[y-searchRadius, x+searchRadius] == '#')
                                {
                                    adjacentOccupied++;
                                    found[2] = true;
                                }
                                else if (prev[y-searchRadius, x+searchRadius] == 'L' || onlyAdj)
                                    found[2] = true;
                            }


                            if (!found[3] && x - searchRadius < 0) found[3] = true;
                            if (!found[3]) 
                            {
                                if (prev[y, x-searchRadius] == '#') 
                                {
                                    adjacentOccupied++;
                                    found[3] = true;
                                }
                                else if (prev[y, x-searchRadius] == 'L' || onlyAdj)
                                    found[3] = true;
                            }

                            if (!found[4] && x + searchRadius >= prev.GetLength(1)) found[4] = true;
                            if (!found[4]) 
                            {
                                if (prev[y, x+searchRadius] == '#') 
                                {
                                    adjacentOccupied++;
                                    found[4] = true;
                                }
                                else if (prev[y, x+searchRadius] == 'L' || onlyAdj)
                                    found[4] = true;
                            }


                            if (!found[5] && (y + searchRadius >= prev.GetLength(0) || x - searchRadius < 0)) found[5] = true;
                            if (!found[5]) 
                            {
                                if (prev[y+searchRadius, x-searchRadius] == '#') 
                                {
                                    adjacentOccupied++;
                                    found[5] = true;
                                }
                                else if (prev[y+searchRadius, x-searchRadius] == 'L' || onlyAdj)
                                    found[5] = true;
                            }

                            if (!found[6] && y + searchRadius >= prev.GetLength(0)) found[6] = true;
                            if (!found[6]) 
                            {
                                if (prev[y+searchRadius,x] == '#') 
                                {
                                    adjacentOccupied++;
                                    found[6] = true;
                                }
                                else if (prev[y+searchRadius,x] == 'L' || onlyAdj)
                                    found[6] = true;
                            }

                            if (!found[7] && (y + searchRadius >= prev.GetLength(0) || x + searchRadius >= prev.GetLength(1))) found[7] = true;
                            if (!found[7]) 
                            {
                                if (prev[y+searchRadius,x+searchRadius] == '#') 
                                {
                                    adjacentOccupied++;
                                    found[7] = true;
                                }
                                else if (prev[y+searchRadius,x+searchRadius] == 'L' || onlyAdj)
                                    found[7] = true;
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
    }
}