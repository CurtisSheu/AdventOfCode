using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day07 : ASolution
    {
        RulesCollection allRules;

        public Day07() : base(7, 2020, "Handy Haversacks")
        {
            allRules = new RulesCollection(Input);
        }

        protected override string solvePartOne()
        {
            return allRules.NumberOfBagsContainingDescriptor("shiny gold").ToString();
        }

        protected override string solvePartTwo()
        {
            return allRules.NumberOfBagsInParentBag("shiny gold").ToString();
        }

        class RulesCollection
        {
            Dictionary<string, bagContents> allBagContents = new Dictionary<string, bagContents>();

            public RulesCollection(string input)
            {
                string pattern = @"(?'parentBag'\w+ \w+) bags contain (?'contents'.+)";
                foreach (Match m in Regex.Matches(input, pattern))
                    addNewBag(m.Groups["parentBag"].Value, m.Groups["contents"].Value);
            }

            private void addNewBag(string description, string contents)
            {
                allBagContents.Add(description, new bagContents(contents, this));
            }

            public int NumberOfBagsContainingDescriptor(string descriptor)
            {
                int count = 0;
                foreach (KeyValuePair<string, bagContents> bag in allBagContents)
                    if (bag.Value.ContainsDescriptor(descriptor))
                        count++;

                return count;
            }
            
            public bool ParentBagContainsDescriptor(string parentBag, string descriptor)
            {
                if (allBagContents[parentBag].ContainsDescriptor(descriptor))
                    return true;

                return false;
            }

            public int NumberOfBagsInParentBag(string descriptor)
            {
                return allBagContents[descriptor].NumberOfBags();
            }

            class bagContents
            {
                Dictionary<string, int> contents = new Dictionary<string, int>();
                RulesCollection allBagContents;

                public bagContents(string contents, RulesCollection rulesCollection) 
                {
                    allBagContents = rulesCollection;
                    string pattern = @"(?'amount'\d+) (?'descriptor'\w+ \w+) bags?(,|.)";

                    foreach (Match m in Regex.Matches(contents, pattern))
                        addNewContent(m.Groups["descriptor"].Value, Convert.ToInt32(m.Groups["amount"].Value));
                }

                private void addNewContent(string description, int number)
                {
                    contents.Add(description, number);
                }

                public bool ContainsDescriptor(string descriptor)
                {
                    if (contents.ContainsKey(descriptor))
                        return true;

                    foreach (string parentBag in contents.Keys)
                        if (allBagContents.ParentBagContainsDescriptor(parentBag, descriptor))
                            return true;

                    return false;
                }

                public int NumberOfBags()
                {
                    int count = 0;

                    foreach (KeyValuePair<string, int> rule in contents)
                        count += rule.Value + rule.Value * allBagContents.NumberOfBagsInParentBag(rule.Key);

                    return count;
                }
            }
        }
    }
}