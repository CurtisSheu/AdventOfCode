using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day06 : ASolution
    {
        List<GroupResponses> allGroupResponses = new List<GroupResponses>();

        public Day06() : base(6, 2020, "Custom Customs")
        {
            foreach (string input in Input.splitByDelimiters(new[] { "\r\n\r\n", "\n\n", "\r\r" }))
            {
                allGroupResponses.Add(new GroupResponses(input));
            }
        }

        protected override string solvePartOne()
        {
            int count = 0;

            foreach (GroupResponses response in allGroupResponses)
                count += response.NumberOfYesAnswers();

            return count.ToString();
        }

        protected override string solvePartTwo()
        {
            int count = 0;

            foreach (GroupResponses response in allGroupResponses)
                count += response.NumberOfSharedYesAnswers();

            return count.ToString();
        }

        class GroupResponses
        {
            Dictionary<char, int> yesAnswers = new Dictionary<char, int>();
            int peopleCount;

            public GroupResponses(string input)
            {
                interpretInput(input);
            }

            private void interpretInput(string input)
            {
                peopleCount = input.splitByNewLine().Length;

                foreach (string line in input.splitByNewLine())
                {
                    foreach (char c in line)
                    {
                        if (!yesAnswers.ContainsKey(c))
                            yesAnswers.Add(c, 0);

                        yesAnswers[c]++;
                    }
                }
            }

            public int NumberOfYesAnswers()
            {
                return yesAnswers.Count;
            }

            public int NumberOfSharedYesAnswers()
            {
                int count = 0;
                foreach (KeyValuePair<char, int> individualResponses in yesAnswers)
                    if (individualResponses.Value == peopleCount)
                        count++;

                return count;
            }
        }
    }
}