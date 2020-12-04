using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day04
{
    class Day04
    {
        const string inputPath = @"Day04/Input.txt";
        const string clrPattern = "^#[0-9a-f]{6}$";
        const string pidPattern = "^\\d{9}$";
        static Regex clrRgx = new Regex(clrPattern);
        static Regex pidRgx = new Regex(pidPattern);


        public static void Task1and2()
        {
            List<Passport> passports = new List<Passport>();
            int validPasses = 0;
            int validPassesStrict = 0;
            using (StreamReader reader = new StreamReader(inputPath))
            {
                string line;
                Passport passport = new Passport();
                while((line = reader.ReadLine()) != null)
                {
                    if(line.Length == 0)
                    {
                        validPasses += (passport.isValid(false)) ? 1 : 0;
                        validPassesStrict += (passport.isValid(true)) ? 1 : 0;
                        passport = new Passport();
                        continue;
                    }

                    string[] values = line.Split(' ');
                    foreach(string val in values)
                    {
                        passport.setValue(val);
                    }
                }
                validPasses += (passport.isValid(false)) ? 1 : 0;
                validPassesStrict += (passport.isValid(true)) ? 1 : 0;
            }

            Console.WriteLine($"Task 1: {validPasses}");
            Console.WriteLine($"Task 2: {validPassesStrict}");
        }


        private class Passport
        {
            public string Byr {get; set; }
            public string Iyr {get; set; }
            public string Eyr {get; set; }
            public string Hgt {get; set; }
            public string Hcl {get; set; }
            public string Ecl {get; set; }
            public string Pid {get; set; }
            public string Cid {get; set; }


            public bool isValid(bool strict)
            {
                if (!string.IsNullOrEmpty(Byr) && !string.IsNullOrEmpty(Iyr) && !string.IsNullOrEmpty(Eyr) && !string.IsNullOrEmpty(Hgt) && !string.IsNullOrEmpty(Hcl) && !string.IsNullOrEmpty(Ecl) && !string.IsNullOrEmpty(Pid))
                {
                    
                    return (strict) ? isValidStrict() : true;
                }

                return false;
            }

            public bool isValidStrict()
            {
                int byr = int.Parse(Byr);
                if (byr < 1920 || byr > 2002) return false;

                int iyr = int.Parse(Iyr);
                if (iyr < 2010 || iyr > 2020) return false;

                int eyr = int.Parse(Eyr);
                if (eyr < 2020 || eyr > 2030) return false;

                string cmIn = Hgt.Substring(Hgt.Length - 2);
                if (cmIn == "cm") 
                {
                    int hgt = int.Parse(Hgt.Substring(0, Hgt.Length - 2));
                    if (hgt < 150 || hgt > 193) return false;
                } 
                else if (cmIn == "in")
                {
                    int hgt = int.Parse(Hgt.Substring(0, Hgt.Length - 2));
                    if (hgt < 59 || hgt > 76) return false;
                }
                else return false;

                if (!clrRgx.Match(Hcl).Success) return false;

                if (Ecl != "amb" && Ecl != "blu" && Ecl != "brn" && Ecl != "gry" && Ecl != "grn" && Ecl != "hzl" && Ecl != "oth") return false;

                if (!pidRgx.Match(Pid).Success) return false;

                return true;
            }

            public void setValue(String value)
            {
                string[] keyVal = value.Split(':');

                switch(keyVal[0])
                {
                    case "byr":
                        Byr = keyVal[1];
                        break;
                    case "iyr":
                        Iyr = keyVal[1];
                        break;
                    case "eyr":
                        Eyr = keyVal[1];
                        break;
                    case "hgt":
                        Hgt = keyVal[1];
                        break;
                    case "hcl":
                        Hcl = keyVal[1];
                        break;
                    case "ecl":
                        Ecl = keyVal[1];
                        break;
                    case "pid":
                        Pid = keyVal[1];
                        break;
                    case "cid":
                        Cid = keyVal[1];
                        break;
                    default:
                        Console.WriteLine(value);
                        break;
                }
            }
        }
    }
}