using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day16
{
    class Day16
    {
        const string inputPath = @"Day16/Input.txt";

        public static void Task1and2()
        {
            Dictionary<string, Field> fields = new Dictionary<string, Field>();
            int readState = 0;
            int[] myTicket = new int[1];
            int scanningErrorRate = 0;
            int[,] validTickets = new int[1,1];
            int validTicketsIdx = 0;
            string[] lines = File.ReadAllLines(inputPath);

            for(int i = 0; i <lines.Length; i++)
            {
                if (lines[i] == "your ticket:" || lines[i] == "nearby tickets:") continue;
                if (lines[i] == "") 
                {
                    readState++;
                }
                else if (readState == 0)
                {
                    Field f = ReadField(lines[i]);
                    fields.Add(f.Name, f);
                }
                else if (readState == 1)
                {
                    myTicket = lines[i].Split(',').Select(a => Int32.Parse(a)).ToArray();
                    validTickets = new int[lines.Length - i - 3, myTicket.Length];
                }
                else if (readState == 2)
                {
                    int[] ticket = lines[i].Split(',').Select(a => Int32.Parse(a)).ToArray();
                    int errors = ValidateTicketFields(fields, ticket);

                    if (errors == 0)
                    {
                        for (int x = 0; x < ticket.Length; x++)
                        {
                            validTickets[validTicketsIdx,x] = ticket[x];
                        }
                        validTicketsIdx++;
                    }

                    scanningErrorRate += errors;
                }
            }

            Console.WriteLine($"Task 1: {scanningErrorRate}");

            GetMatchingFieldIdx(fields, validTickets);

            while(fields.Any(l => l.Value.Idxs.Count > 0))
            {
                KeyValuePair<string, Field> rmvIdx = fields.First(l => l.Value.Idxs.Count == 1);
                fields[rmvIdx.Key].RealIdx = rmvIdx.Value.Idxs.First();

                foreach(KeyValuePair<string, Field> field in fields)
                {
                    fields[field.Key].Idxs.Remove(fields[rmvIdx.Key].RealIdx);
                }
            }

            long departureValue = 1;
            List<int> departureIndices = fields.Where(f => f.Key.StartsWith("departure")).Select(k => k.Value.RealIdx).ToList();

            foreach(int i in departureIndices)
            {
                departureValue *= myTicket[i];
            }

            Console.WriteLine($"Task 2: {departureValue}");
        }

        private static Field ReadField(string line)
        {
            string[] info = line.Split(':');
            string[] infoNums = info[1].Split(' ');
            string[] nums1 = infoNums[1].Split('-');
            string[] nums2 = infoNums[3].Split('-');

            return new Field(info[0], Int32.Parse(nums1[0]), Int32.Parse(nums1[1]), Int32.Parse(nums2[0]), Int32.Parse(nums2[1]));
        }

        private static int ValidateTicketFields(Dictionary<string, Field> fields, int[] nums)
        {
            int errorRate = 0;

            foreach(int n in nums)
            {
                bool found = false;
                foreach(KeyValuePair<string, Field> kvPair in fields)
                {
                    if(kvPair.Value.IsInRange(n))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found) errorRate += n;
            }

            return errorRate;
        }

        private static void GetMatchingFieldIdx(Dictionary<string, Field> fields, int[,] validTickets)
        {
            for(int i = 0; i < validTickets.GetLength(1); i++)
            {
                int[] tmp = Enumerable.Range(0, validTickets.GetLength(0)).Select(x => validTickets[x, i]).ToArray();
                tmp = tmp.TakeWhile(n => n != 0).ToArray();

                foreach(KeyValuePair<string, Field> kvPair in fields)
                {
                    if(tmp.All(n => kvPair.Value.IsInRange(n)))
                    {
                        kvPair.Value.Idxs.Add(i);
                    }
                }
            }
        }

        private class Field
        {
            public string Name {get; set; }
            public int Min1 {get; set; }
            public int Max1 {get; set; }
            public int Min2 {get; set; }
            public int Max2 {get; set; }
            public List<int> Idxs {get; set; }
            public int RealIdx {get; set; }

            public Field(string name, int min1, int max1, int min2, int max2)
            {
                Name = name;
                Min1 = min1;
                Max1 = max1;
                Min2 = min2;
                Max2 = max2;
                Idxs = new List<int>();
                RealIdx = -1;
            }

            public bool IsInRange(int number)
            {
                return (number >= Min1 && number <= Max1) || (number >= Min2 && number <= Max2);
            }
        }
    }
}