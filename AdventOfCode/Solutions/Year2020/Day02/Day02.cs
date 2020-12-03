using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day02 : ASolution
    {
        List<passwordInformation> passwords = new List<passwordInformation>();

        public Day02() : base(2, 2020, "Password Philosophy") 
        {
            string pattern = @"(?'min'\d+)-(?'max'\d+) (?'check'.): (?'password'[a-z]+)";

            foreach(Match m in Regex.Matches(Input, pattern))
                passwords.Add(new passwordInformation(Convert.ToInt32(m.Groups["min"].Value), Convert.ToInt32(m.Groups["max"].Value), Convert.ToChar(m.Groups["check"].Value), m.Groups["password"].Value));
        }

        protected override string solvePartOne()
        {
            int count = 0;

            foreach (passwordInformation password in passwords)
                if (password.CharCountValid())
                    count++;

            return count.ToString();
        }

        protected override string solvePartTwo()
        {
            int count = 0;

            foreach (passwordInformation password in passwords)
                if (password.CharPositionValid())
                    count++;

            return count.ToString();
        }
    }

    class passwordInformation
    {
        int firstValue;
        int secondValue;
        char chk;
        string password;

        public passwordInformation(int first, int second, char check, string passwordValue)
        {
            firstValue = first;
            secondValue = second;
            chk = check;
            password = passwordValue;
        }

        public bool CharCountValid()
        {
            return password.Count(c => c == chk) <= secondValue && password.Count(c => c == chk) >= firstValue;
        }

        public bool CharPositionValid()
        {
            return password[firstValue - 1] == chk ^ password[secondValue - 1] == chk;
        }
    }
}
