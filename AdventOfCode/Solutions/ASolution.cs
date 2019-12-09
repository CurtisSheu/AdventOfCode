using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AdventOfCode.Solutions
{
    abstract class ASolution
    {
        Lazy<string> _input, _part1, _part2;

        public int day { get; }
        public int year { get; }
        public string title { get; }
        public string DebugInput { get; set; }
        public string Input => DebugInput != null ? DebugInput : (string.IsNullOrEmpty(_input.Value) ? null : _input.Value);
        public string Part1 => string.IsNullOrEmpty(_part1.Value) ? "" : _part1.Value;
        public string Part2 => string.IsNullOrEmpty(_part2.Value) ? "" : _part2.Value;

        protected ASolution(int d, int y, string t)
        {
            day = d;
            year = y;
            title = t;
            _input = new Lazy<string>(() => loadInput());
            _part1 = new Lazy<string>(() => solvePartOne());
            _part2 = new Lazy<string>(() => solvePartTwo());
        }
        
        public void Solve(int part = 0)
        {
            if (Input == null) return;

            bool hasOutput = false;
            string output = $"--- Day {day}: {title} --- \n";

            if (part != 2)
            {
                if (Part1 != string.Empty)
                {
                    output += $"Part 1: {Part1}\n";
                    hasOutput = true;
                }
                else
                {
                    output += "Part 1: Unsolved\n";
                    if (part == 1) hasOutput = true;
                }
            }
            if (part != 1)
            {
                if (Part2 != string.Empty)
                {
                    output += $"Part 2: {Part2}\n";
                    hasOutput = true;
                }
                else
                {
                    output += "Part 2: Unsolved\n";
                    if (part == 2) hasOutput = true;
                }
            }

            if (hasOutput) Console.WriteLine(output);
        }

        private string loadInput()
        {
            string inputFilePath = $"../../../Solutions/Year{year}/Day{day.ToString("D2")}/input.txt";
            string inputURL = $"https://adventofcode.com/{year}/day/{day}/input";
            string input = string.Empty;

            if (File.Exists(inputFilePath))
                input = File.ReadAllText(inputFilePath);

            return input;
        }

        protected abstract string solvePartOne();
        protected abstract string solvePartTwo();
    }
}
