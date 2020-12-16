using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day16
{
    class Day16
    {
        const string inputPath = @"Day16/Input.txt";

        public static void Task1()
        {
            Dictionary<string, (int min1, int max1, int min2, int max2)> fields = new Dictionary<string, (int, int, int, int)>();
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
                    continue;
                }

                if (readState == 0)
                {
                    ReadFields(fields, lines[i]);
                }
                else if (readState == 1)
                {
                    myTicket = lines[i].Split(',').Select(a => Int32.Parse(a)).ToArray();
                    validTickets = new int[lines.Length - i - 3, myTicket.Length];
                }
                else if (readState == 2)
                {
                    int[] ticket = lines[i].Split(',').Select(a => Int32.Parse(a)).ToArray();

                    int errors = validateFields(fields, ticket);

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

            Dictionary<string, List<int>> fieldIndices = new Dictionary<string, List<int> >();
            string[] fieldName = new string[validTickets.GetLength(1)];

            getMatchingFieldIdx(fields, fieldIndices, fieldName, validTickets);

            while(fieldIndices.Any(l => l.Value.Count > 0))
            {
                KeyValuePair<string, List<int>> rmvIdx = fieldIndices.First(l => l.Value.Count == 1);
                fieldName[rmvIdx.Value.First()] = rmvIdx.Key;
                int delId = rmvIdx.Value.First();

                foreach(KeyValuePair<string, List<int>> field in fieldIndices)
                {
                    fieldIndices[field.Key].Remove(delId);
                }
            }

            long departureValue = 1;

            List<int> departureIndices = Enumerable.Range(0, fieldName.Length).Where(i => fieldName[i].StartsWith("departure")).ToList();

            foreach(int i in departureIndices)
            {
                departureValue *= myTicket[i];
            }

            Console.WriteLine($"Task 2: {departureValue}");
        }

        private static void ReadFields(Dictionary<string, (int min1, int max1, int min2, int max2)> fields, string line)
        {
            string[] info = line.Split(':');
            string[] infoNums = info[1].Split(' ');
            string[] nums1 = infoNums[1].Split('-');
            string[] nums2 = infoNums[3].Split('-');
            fields.Add(info[0], (Int32.Parse(nums1[0]), Int32.Parse(nums1[1]), Int32.Parse(nums2[0]), Int32.Parse(nums2[1])));
        }

        private static int validateFields(Dictionary<string, (int min1, int max1, int min2, int max2)> fields, int[] nums)
        {   
            int errorRate = 0;

            foreach(int n in nums)
            {
                bool found = false;
                foreach(KeyValuePair<string, (int, int, int, int)> kvPair in fields)
                {
                    if ((n >= kvPair.Value.Item1 && n <= kvPair.Value.Item2) || (n >= kvPair.Value.Item3 && n <= kvPair.Value.Item4))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found) errorRate += n;
            }
            return errorRate;
        }

        private static void getMatchingFieldIdx(Dictionary<string, (int min1, int max1, int min2, int max2)> fields, Dictionary<string, List<int>> fieldIndices, string[] fieldName, int[,] validTickets)
        {
            for(int i = 0; i < validTickets.GetLength(1); i++)
            {
                int[] tmp = Enumerable.Range(0, validTickets.GetLength(0)).Select(x => validTickets[x, i]).ToArray();

                tmp = tmp.TakeWhile(n => n != 0).ToArray();

                foreach(KeyValuePair<string, (int, int, int, int)> kvPair in fields)
                {
                    if(tmp.All(n => (n >= kvPair.Value.Item1 && n <= kvPair.Value.Item2) || (n >= kvPair.Value.Item3 && n <= kvPair.Value.Item4)))
                    {
                        if (!fieldIndices.ContainsKey(kvPair.Key)) fieldIndices.Add(kvPair.Key, new List<int>());

                        fieldIndices[kvPair.Key].Add(i);
                    }
                }
            }
        }

        private class Field
        {
            public int Min1 {get; set; }
            public int Max1 {get; set; }
            public int Min2 {get; set; }
            public int Max2 {get; set; }
            public List<int> Idxs {get; set; }

            public bool isInRange(int number)
            {
                return (number >= Min1 && number <= Max1) || (number >= Min2 && number <= Max2);
            }
        }
    }
}