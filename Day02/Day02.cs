using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day02
{
    class Day02
    {
        const string inputPath = @"Day02/Input.txt";

        public static void Task1and2() 
        {
            List<string> passwordLines = new List<string>();
            int validPass1 = 0;
            int validPass2 = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    passwordLines.Add(line);
                }
            }

            foreach (var passwordLine in passwordLines)
            {
                var passWithPolicy = GetPassWithPolicy(passwordLine);

                int amount = passWithPolicy.pass.Count(c => c == passWithPolicy.letter);

                if (amount >= passWithPolicy.min && amount <= passWithPolicy.max)
                {
                    validPass1++;
                }

                if(isValidPass(passWithPolicy))
                {
                    validPass2++;
                }
            }

            Console.WriteLine($"Task 1: {validPass1}");
            Console.WriteLine($"Task 2: {validPass2}");
        }


        private static (int min, int max, char letter, string pass) GetPassWithPolicy(string line)
        {
            string[] splittedLine = line.Split(' ');
            string[] splittedNum = splittedLine[0].Split('-');
            int min = int.Parse(splittedNum[0]);
            int max = int.Parse(splittedNum[1]);
            char letter = char.Parse(splittedLine[1].Substring(0,1));

            return (min, max, letter, splittedLine[2]);
        }

        private static bool isValidPass((int min, int max, char letter, string pass) passWithPolicy) 
        {
            bool isValid = false;
            int index1 = passWithPolicy.min - 1;
            int index2 = passWithPolicy.max - 1;

            if (passWithPolicy.pass[index1] == passWithPolicy.letter)
            {
                isValid = !isValid;
            }

            if (passWithPolicy.pass[index2] == passWithPolicy.letter)
            {
                isValid = !isValid;
            }

            return isValid;
        }
    }
}