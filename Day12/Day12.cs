using System;
using System.IO;

namespace AdventOfCode2020.Day12
{
    enum Direction: int
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    class Day12
    {
        const string inputPath = @"Day12/Input.txt";

        public static void Task1and2()
        {
            (int northSouth, int eastWest) location = (0, 0);
            (int northSouth, int eastWest) locationTask2 = (0, 0);
            (int northSouth, int eastWest) locationWP = (1, 10);
            Direction currentDirection = Direction.East;

            string[] lines = File.ReadAllLines(inputPath);

            foreach(string line in lines)
            {
                int value = Int32.Parse(line.Substring(1));
                var result = Move(line[0], value, currentDirection);
                location.northSouth += result.northSouth;
                location.eastWest += result.eastWest;
                currentDirection = result.dir;

                if (line[0] == 'F')
                {
                    locationTask2.northSouth += value * locationWP.northSouth;
                    locationTask2.eastWest += value * locationWP.eastWest; 
                }
                else 
                {
                    var resultTask2 = MoveWP(line[0], value, locationWP);
                    if (resultTask2.isSwap)
                    {
                        locationWP.northSouth = resultTask2.northSouth;
                        locationWP.eastWest = resultTask2.eastWest;
                    }
                    else 
                    {
                        locationWP.northSouth += resultTask2.northSouth;
                        locationWP.eastWest += resultTask2.eastWest;
                    }
                }
            }

            Console.WriteLine($"Task 1: {Math.Abs(location.northSouth) + Math.Abs(location.eastWest)}");
            Console.WriteLine($"Task 2: {Math.Abs(locationTask2.northSouth) + Math.Abs(locationTask2.eastWest)}");
        }

        private static (int northSouth, int eastWest, Direction dir) Move(char action, int value, Direction currentDirection)
        {
            if (action == 'N') return (value, 0, currentDirection);
            else if (action == 'S') return (-value, 0, currentDirection);
            else if (action == 'E') return (0, value, currentDirection);
            else if (action == 'W') return (0, -value, currentDirection);
            else if (action == 'L')
            {
                int a = (int)currentDirection - value / 90;
                currentDirection = (Direction)((a % 4 + 4) % 4);
                return (0, 0, currentDirection);
            }
            else if (action == 'R')
            {
                currentDirection = (Direction)(((int)currentDirection + value / 90) % 4);
                return (0, 0, currentDirection);
            }
            else if (action == 'F')
            {
                if (currentDirection == Direction.North) return (value, 0, currentDirection);
                else if (currentDirection == Direction.East) return (0, value, currentDirection);
                else if (currentDirection == Direction.South) return (-value, 0, currentDirection);
                else if (currentDirection == Direction.West) return (0, -value, currentDirection);
            }

            return (0, 0, currentDirection);
        }

        private static (int northSouth, int eastWest, bool isSwap) MoveWP(char action, int value, (int nS, int eW) wayPoint)
        {
            if (action == 'N') return (value, 0, false);
            else if (action == 'S') return (-value, 0, false);
            else if (action == 'E') return (0, value, false);
            else if (action == 'W') return (0, -value, false);
            else
            {
                int a = value / 90;
                int change = (a % 4 + 4) % 4;
                int tmp = wayPoint.eW;
                if ((change == 1 && action == 'L') || (change == 3 && action == 'R'))
                {
                    wayPoint.eW = -wayPoint.nS;
                    wayPoint.nS = tmp;
                    return (wayPoint.nS, wayPoint.eW, true);
                }
                else if (change == 2)
                {
                    wayPoint.nS = -wayPoint.nS;
                    wayPoint.eW = -wayPoint.eW;
                    return (wayPoint.nS, wayPoint.eW, true);
                }
                else if ((change == 3 && action == 'L') || (change == 1 && action == 'R'))
                {
                    wayPoint.eW = wayPoint.nS;
                    wayPoint.nS = -tmp;
                    return (wayPoint.nS, wayPoint.eW, true);
                }
            }

            return (0, 0, false);
        }
    }
}