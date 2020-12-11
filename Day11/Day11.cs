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

            char[] prev = new char[lines.Length * width];

            for(int i = 0; i < lines.Length; i++)
            {
                for (int x = 0; x < width; x++)
                {
                    prev[i * width + x] = lines[i][x];
                }
            }

            Task1(prev, width);
        //    Task2(prev, width);
        }

        private static void Task1(char[] prev, int width)
        {
            char[] next = new char[prev.Length];
            Array.Copy(prev, next, prev.Length);

            bool changed;

            int idx;

            do 
            {
                changed = false;

                for(int y = 0; y < prev.Length; y += width)
                {
                    for(int x = 0; x < width; x++)
                    {
                        if (y == 80 && x == 9)
                        {
                            Console.Write("");
                        }

                        int adjacentOccupied = 0;

                        if (y - width + x - 1 >= 0 && y - width + x - 1 >= y - width && prev[(y-width) + (x-1)] == '#') adjacentOccupied++;
                        if (y - width >= 0 && prev[(y-width) + x] == '#') adjacentOccupied++;
                        if (y - width + x + 1 >= 0 && y - width + x + 1 < y && prev[(y-width) + (x+1)] == '#') adjacentOccupied++;

                        if (y + x - 1 >= y && prev[y + (x-1)] == '#') adjacentOccupied++;
                        if (y + x + 1 < y + width && prev[y + (x+1)] == '#') adjacentOccupied++;

                        if (y + width + x - 1 < prev.Length && y + width + x - 1 >= y + width && prev[(y+width) + (x-1)] == '#') adjacentOccupied++;
                        if (y + width < prev.Length && prev[(y+width) + x] == '#') adjacentOccupied++;
                        if (y + width + x + 1 < prev.Length && y + width + x + 1 < y + 2 * width && prev[(y+width) + (x+1)] == '#') adjacentOccupied++;

                        if (prev[y + x] == 'L' && adjacentOccupied == 0) 
                        {
                            next[y + x] = '#';
                            changed = true;
                        }
                        else if (prev[y + x] == '#' && adjacentOccupied >= 4)
                        {
                            next[y + x] = 'L';
                            changed = true;
                        }
                    }
                }

                Array.Copy(next, prev, prev.Length);

            } while(changed);
            
            Console.WriteLine($"Task 1: {prev.Count(c => c == '#')}");
        }


        private static void Task2(char[] prev, int width)
        {
            char[] next = new char[prev.Length];
            Array.Copy(prev, next, prev.Length);

            bool changed;
            int idx;

            do 
            {
                changed = false;

                for(int y = 0; y < prev.Length; y += width)
                {
                    for(int x = 0; x < width; x++)
                    {
                        if (y == 80 && x == 9)
                        {
                            Console.Write("");
                        }

                        int adjacentOccupied = 0;
                        bool[] found = new bool[] { false, false, false, false, false, false, false, false };

                        int searchRadius = 0;

                        while(found.Any(f => f == false))
                        {
                            ++searchRadius;
                            idx =  y - (searchRadius * width) + x - searchRadius;

                            if (!found[0] && idx >= 0 
                                          && idx >= y - width 
                                          && prev[idx] == '#' || prev[idx] == 'L')
                            {
                                if (prev[idx] == '#') adjacentOccupied++;
                                found[0] = true;
                            }
                            if (!found[1] && y - (searchRadius * width) >= 0 
                                          && prev[(y-searchRadius*width) + x] == '#')
                            {
                                adjacentOccupied++;
                                found[1] = true;
                            }

                            if (!found[2] && y - (searchRadius * width) + x + searchRadius >= 0 
                                          && y - (searchRadius * width) + x + searchRadius < y 
                                          && prev[(y-searchRadius*width) + (x+searchRadius)] == '#')
                            {
                                adjacentOccupied++;
                                found[2] = true;
                            }

                            if (!found[3] && y + x - searchRadius >= y 
                                          && prev[y + (x-searchRadius)] == '#') 
                            {
                                adjacentOccupied++;
                                found[3] = true;
                            }
                            if (!found[4] && y + x + searchRadius < y + width 
                                          && prev[y + (x+searchRadius)] == '#') {
                                adjacentOccupied++;
                                found[4] = true;
                            }

                            if (!found[5] && y + (searchRadius * width) + x - 1 < prev.Length 
                                          && y + (searchRadius * width) + x - 1 >= y + width 
                                          && prev[(y+searchRadius*width) + (x-1)] == '#')
                            { 
                                adjacentOccupied++;
                                found[5] = true;
                            }
                            if (!found[6] && y + (searchRadius * width) < prev.Length 
                                          && prev[(y+searchRadius*width) + x] == '#')
                            { 
                                adjacentOccupied++;
                                found[6] = true;
                            }
                            if (!found[7] && y + (searchRadius * width) + x + searchRadius < prev.Length 
                                          && y + (searchRadius * width) + x + searchRadius < y + 2 * width 
                                          && prev[(y+searchRadius*width) + (x+searchRadius)] == '#')
                            {
                                adjacentOccupied++;
                                found[7] = true;
                            }
                        }
                        

                        if (prev[y + x] == 'L' && adjacentOccupied == 0) 
                        {
                            next[y + x] = '#';
                            changed = true;
                        }
                        else if (prev[y + x] == '#' && adjacentOccupied >= 5)
                        {
                            next[y + x] = 'L';
                            changed = true;
                        }
                    }
                }

                Array.Copy(next, prev, prev.Length);

            } while(changed);


            Console.WriteLine($"Task 2: {prev.Count(c => c == '#')}");
        }
    }
}