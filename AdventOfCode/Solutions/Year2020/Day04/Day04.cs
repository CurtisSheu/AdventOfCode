using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day04 : ASolution
    {
        List<Passport> passports = new List<Passport>();

        public Day04() : base(4, 2020, "Passport Processing")
        {
            string[] passportInputs = Input.splitByDelimiters(new[] { "\r\n\r\n", "\n\n", "\r\r" });

            foreach (string input in passportInputs)
                passports.Add(new Passport(input));
        }

        protected override string solvePartOne()
        {
            int count = 0;
            string[] keysToCheck = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

            foreach (Passport p in passports)
                if (p.CheckIfKeysExist(keysToCheck))
                    count++;

            return count.ToString();
        }

        protected override string solvePartTwo()
        {
            int count = 0;

            foreach (Passport p in passports)
                if (p.StrictCheck())
                    count++;

            return count.ToString();
        }
    }

    class Passport
    {
        Dictionary<string, string> entries = new Dictionary<string, string>();

        public Passport(string input)
        {
            interpretInputString(input);
        }

        private void interpretInputString(string input)
        {
            string pattern = @"(?'key'[^:\s]+):(?'value'[^:\s]+)";

            foreach (Match match in Regex.Matches(input, pattern))
                entries.Add(match.Groups["key"].Value, match.Groups["value"].Value);
        }

        public bool CheckIfKeysExist(string[] keysToCheck)
        {
            foreach (string key in keysToCheck)
                if (!entries.ContainsKey(key))
                    return false;

            return true;
        }

        public bool StrictCheck()
        {
            if (checkKeyYear("byr", 1920, 2002))
                return false;
            if (checkKeyYear("iyr", 2010, 2020))
                return false;
            if (checkKeyYear("eyr", 2020, 2030))
                return false;
            if (!checkHeight())
                return false;
            if (!checkHairColor())
                return false;
            if (!checkEyeColor())
                return false;
            if (!checkPassportID())
                return false;

            return true;
        }

        private bool checkKeyYear(string key, int min, int max)
        {
            if (!entries.ContainsKey(key) || checkIfValueIsIntAndWithinRange(entries[key], min, max))
                return false;

            return true;
        }

        private bool checkIfValueIsIntAndWithinRange(string value, int min, int max)
        {
            if (!int.TryParse(value, out int result) || result < min || result > max)
                return false;

            return true;
        }

        private bool checkHeight()
        {
            if (!entries.ContainsKey("hgt"))
                return false;

            string pattern = @"(?'numericalValue'\d+)(?'measurement'(cm|in))";

            MatchCollection matches = Regex.Matches(entries["hgt"], pattern);

            if(matches.Count == 1)
            {
                if (matches[0].Groups["measurement"].Value == "cm" && checkIfValueIsIntAndWithinRange(matches[0].Groups["numericalValue"].Value, 150, 193))
                    return true;
                else if (matches[0].Groups["measurement"].Value == "in" && checkIfValueIsIntAndWithinRange(matches[0].Groups["numericalValue"].Value, 59, 76))
                    return true;
            }

            return false;
        }

        private bool checkHairColor()
        {
            string pattern = @"#[0-9|a-f]{6}";
            if (!entries.ContainsKey("hcl") || entries["hcl"].Length !=7 || !Regex.IsMatch(entries["hcl"], pattern))
                return false;

            return true;
        }

        private bool checkEyeColor()
        {
            string pattern = @"(amb|blu|brn|gry|grn|hzl|oth)";
            if (!entries.ContainsKey("ecl") || entries["ecl"].Length != 3 || !Regex.IsMatch(entries["ecl"], pattern))
                return false;

            return true;
        }

        private bool checkPassportID()
        {
            string pattern = @"\d{9}";
            if (!entries.ContainsKey("pid") || entries["pid"].Length != 9 || !Regex.IsMatch(entries["pid"], pattern))
                return false;

            return true;
        }
    }
}
