using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day07
{
    class Day07
    {
        const string inputPath = @"Day07/Input.txt";

        // Child is key, Parent is saved inside value with the amount of bags of the child
        private static Dictionary<string, Dictionary<string, int>> bags = new Dictionary<string, Dictionary<string, int>>();

        public static void Task1and2()
        {
            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                string splitStr = " bags contain ";
                while((line = reader.ReadLine()) != null)
                {
                    string[] splitBags = line.Split(splitStr);
                    string[] splitSubbags = splitBags[1].Split(", ");
                    
                    for(int i = 0; i < splitSubbags.Length; i++) 
                    {
                        if (splitSubbags[i] == "no other bags.") continue;

                        int num = Int32.Parse(splitSubbags[i].Substring(0, 1));
                        int end = (i == splitSubbags.Length - 1) ? splitSubbags[i].Length - 3 : splitSubbags[i].Length - 2;
                        string[] bagNames = splitSubbags[i].Split(" bag");
                        string bagName = bagNames[0].Substring(2);

                        if (bags.ContainsKey(bagName)) 
                        {
                            bags[bagName].Add(splitBags[0], num);
                        }
                        else 
                        {
                            Dictionary<string, int> bag = new Dictionary<string, int>();
                            bag.Add(splitBags[0], num);

                            bags.Add(bagName, bag);
                        }
                    }
                }

                Console.WriteLine($"Task 1: {ContainsBag("shiny gold", new HashSet<string>()).Count}");
                Console.WriteLine($"Task 2: {InsideShinyGold("shiny gold")}");
            }
        }

        private static HashSet<string> ContainsBag(string bagName, HashSet<string> containBags) 
        {
            if (bags.ContainsKey(bagName)) 
            {
                foreach(KeyValuePair<string, int> kvPair in bags[bagName])
                {
                    containBags.Add(kvPair.Key);
                    ContainsBag(kvPair.Key, containBags);
                }
            }

            return containBags;
        }

        private static int InsideShinyGold(string bagName)
        {
            int i = 0;

            foreach(KeyValuePair<string, Dictionary<string, int>> kvPair in bags)
            {
                if (kvPair.Value.ContainsKey(bagName)) 
                {
                    i += kvPair.Value[bagName];
                    i += kvPair.Value[bagName] * InsideShinyGold(kvPair.Key);
                }
            }

            return i;
        }
    }
}