using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day05 : ASolution
    {
        string[] inputLines;
        public Day05() : base(5, 2020, "Binary Boarding")
        {
            inputLines = Input.splitByNewLine();
        }

        protected override string solvePartOne()
        {
            int max = 0;

            foreach (string line in inputLines)
                if (calculateSeatNumber(line) > max)
                    max = calculateSeatNumber(line);

            return max.ToString();
        }

        protected override string solvePartTwo()
        {
            HashSet<int> allSeats = new HashSet<int>();

            foreach (string line in inputLines)
                allSeats.Add(calculateSeatNumber(line));

            foreach (int seatNumber in allSeats)
                if (!allSeats.Contains(seatNumber + 1) && allSeats.Contains(seatNumber + 2))
                    return (seatNumber + 1).ToString();

            return string.Empty;
        }

        private int calculateSeatNumber(string input)
        {
            input = input.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');

            return Convert.ToInt32(input, 2);
        }
    }
}
