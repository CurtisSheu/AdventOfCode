using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day13 : ASolution
    {
        ShuttleScheduleCalculator scheduleCalculator;
        public Day13() : base(13, 2020, "Shuttle Search")
        {
            scheduleCalculator = new ShuttleScheduleCalculator(Input);
        }

        protected override string solvePartOne()
        {
            return scheduleCalculator.calculateFirstBus().ToString();
        }

        protected override string solvePartTwo()
        {
            return scheduleCalculator.CalculateSequence().ToString();
        }
        
        class ShuttleScheduleCalculator
        {
            int departureTime;
            Dictionary<int, int> busTimes;
            public ShuttleScheduleCalculator(string input)
            {
                string[] inputLines = input.splitByNewLine();

                departureTime = Convert.ToInt32(inputLines[0]);

                busTimes = new Dictionary<int, int>();
                string pattern = @"(?'busTime'\w+),?";
                int offset = 0;
                foreach (Match m in Regex.Matches(inputLines[1], pattern))
                {
                    if (m.Groups["busTime"].Value != "x")
                        busTimes.Add(Convert.ToInt32(m.Groups["busTime"].Value), offset);

                    offset++;
                }
            }

            public int calculateFirstBus()
            {
                int minWait = int.MaxValue;
                int lowestBus = -1;
                foreach(int time in busTimes.Keys)
                {
                    int calculatedTime = time;

                    while (calculatedTime < departureTime)
                        calculatedTime += time;

                    if (calculatedTime - departureTime < minWait)
                    {
                        minWait = calculatedTime - departureTime;
                        lowestBus = time;
                    }
                }

                return minWait * lowestBus;
            }

            public long CalculateSequence()
            {

                long currentMultiple = -1;
                long checkTime = -1;
                foreach (KeyValuePair<int, int> busTime in busTimes) 
                {
                    if (currentMultiple == -1)
                    {
                        currentMultiple = busTime.Key;
                        checkTime = busTime.Key;
                    }
                    else
                    {
                        while ((checkTime + busTime.Value) % busTime.Key != 0)
                            checkTime += currentMultiple;

                        currentMultiple = Utility.LCM(new List<long> { currentMultiple, busTime.Key });
                    }
                }

                return checkTime;
            }
            
        }
    }
}