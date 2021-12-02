using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day08 : ASolution
    {
        Console console;
        public Day08() : base(8, 2020, "Handheld Halting")
        {
            console = new Console(Input);
        }

        protected override string solvePartOne()
        {
            return console.RunUntilInstructionRepeats().accumulatorValue.ToString();
        }

        protected override string solvePartTwo()
        {
            return console.ChangeInstructions().ToString();
        }

        class Console
        {
            int accumulator;
            string[] instructions;
            int instructionCounter;

            public Console(string input)
            {
                accumulator = 0;
                instructionCounter = 0;
                instructions = input.splitByNewLine();
            }

            public Console(string[] input)
            {
                accumulator = 0;
                instructionCounter = 0;
                instructions = input;
            }

            public (bool looped, int accumulatorValue) RunUntilInstructionRepeats()
            {
                bool[] visitedInstructions = new bool[instructions.Length];
                string pattern = @"(?'operation'\w{3}) (?'argument'(\+|-)\d+)";

                while (true)
                {
                    if (instructionCounter >= instructions.Length)
                        return (false, accumulator);
                    if (visitedInstructions[instructionCounter])
                        break;

                    visitedInstructions[instructionCounter] = true;

                    Match m = Regex.Match(instructions[instructionCounter], pattern);
                    switch (m.Groups["operation"].Value)
                    {
                        case "nop":
                            instructionCounter++;
                            break;
                        case "acc":
                            accumulator += Convert.ToInt32(m.Groups["argument"].Value);
                            instructionCounter++;
                            break;
                        case "jmp":
                            instructionCounter += Convert.ToInt32(m.Groups["argument"].Value);
                            break;
                    }
                }
                return (true, accumulator);
            }

            public int ChangeInstructions()
            {
                for (int i = 0; i < instructions.Length; i++)
                {
                    if (instructions[i].Substring(0,3) == "nop")
                    {
                        string[] amendedInstructions = new string[instructions.Length];
                        instructions.CopyTo(amendedInstructions,0);
                        amendedInstructions[i] = instructions[i].Replace("nop", "jmp");
                        Console amendedConsole = new Console(amendedInstructions);

                        (bool looped, int accumulator) output = amendedConsole.RunUntilInstructionRepeats();

                        if (!output.looped)
                            return accumulator;
                    }
                    else if (instructions[i].Substring(0, 3) == "jmp")
                    {
                        string[] amendedInstructions = new string[instructions.Length];
                        instructions.CopyTo(amendedInstructions, 0);
                        amendedInstructions[i] = instructions[i].Replace("jmp", "nop");
                        Console amendedConsole = new Console(amendedInstructions);

                        (bool looped, int accumulator) output = amendedConsole.RunUntilInstructionRepeats();

                        if (!output.looped)
                            return output.accumulator;
                    }
                }
                return -1;
            }
        }
    }
}