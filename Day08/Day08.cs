using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day08
{
    class Day08
    {
        const string inputPath = @"Day08/Input.txt";

        private static int accumulator = 0;

        public static void Task1and2()
        {
            int lineCount = File.ReadLines(inputPath).Count();

            (string op, int arg, bool isExec)[] instructions = new (string op, int arg, bool isExec)[lineCount];
            int idx = 0;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] split = line.Split(' ');

                    instructions[idx] = (split[0], Int32.Parse(split[1]), false);
                    idx++;
                }
            }
            
            (string op, int arg, bool isExec)[] instructionsCopy = new (string op, int arg, bool isExec)[instructions.Length];
            Array.Copy(instructions, instructionsCopy, instructions.Length);

            DoInstructions(instructions);
            Console.WriteLine($"Task 1: {accumulator}");
            Task2(instructionsCopy);
            Console.WriteLine($"Task 2: {accumulator}");
        }


        private static bool DoInstructions((string op, int arg, bool isExec)[] instructions)
        {
            int idx = 0;
            bool hasExited = true;

            while(idx < instructions.Length)
            {
                if(instructions[idx].isExec)
                {
                    hasExited = false;
                    break;
                }

                instructions[idx].isExec = true;

                if (instructions[idx].op == "acc")
                {
                    accumulator += instructions[idx].arg;
                    idx++;
                }
                else if (instructions[idx].op == "jmp")
                {
                    idx += instructions[idx].arg;
                }
                else if (instructions[idx].op == "nop")
                {
                    idx++;
                }
            }

            return hasExited;
        }

        private static void Task2((string op, int arg, bool isExec)[] instructions)
        {
            for(int i = 0; i < instructions.Length; i++)
            {
                accumulator = 0;

                if(instructions[i].op == "jmp" || instructions[i].op == "nop")
                {
                    (string op, int arg, bool isExec)[] instructionsCopy = new (string op, int arg, bool isExec)[instructions.Length];

                    Array.Copy(instructions, instructionsCopy, instructions.Length);

                    if (instructions[i].op == "jmp")
                    {
                        instructionsCopy[i].op = "nop";
                    }
                    else 
                    {
                        instructionsCopy[i].op = "jmp";
                    }

                    if (DoInstructions(instructionsCopy))
                    {
                        return;
                    }
                }
            }
        }
    }
}