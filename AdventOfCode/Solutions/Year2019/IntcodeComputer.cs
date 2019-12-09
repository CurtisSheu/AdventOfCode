using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    class IntcodeComputer
    {
        int[] intcode;
        public int[] memory { get; private set; }

        public List<int> input { get; private set; }
        public List<int> output { get; private set; }

        public IntcodeComputer(int[] input)
        {
            intcode = input;
            initialize();
        }

        public IntcodeComputer initialize(int? noun = null, int? verb = null)
        {
            memory = new int[intcode.Length];
            intcode.CopyTo(memory, 0);
            if (noun != null) memory[1] = noun.Value;
            if (verb != null) memory[2] = verb.Value;
            input = new List<int>();
            output = new List<int>();
            return this;
        }

        public IntcodeComputer run(int? inp = null)
        {
            if (inp != null) input.Add(inp.Value);

            (Mode[] modes, OpCode opcode) = parseInstruction(memory[0]);
            int instructionCounter = 0;

            while (opcode != OpCode.halt)
            {
                int location = memory[instructionCounter + 1];
                if (opcode == OpCode.input || opcode == OpCode.output)
                {
                    switch (opcode)
                    {
                        case OpCode.input:
                            memory[location] = input[0];
                            break;
                        case OpCode.output:
                            int val1 = parseMode(modes[0], memory[instructionCounter + 1]);
                            output.Add(val1);
                            break;
                    }
                    instructionCounter += 2;
                }                
                else if (opcode == OpCode.jumpIfFalse || opcode == OpCode.jumpIfTrue)
                {
                    int val1 = parseMode(modes[0], memory[instructionCounter + 1]);
                    int val2 = parseMode(modes[1], memory[instructionCounter + 2]);
                    bool jump = false;

                    switch (opcode)
                    {
                        case OpCode.jumpIfTrue:
                            if (val1 != 0)
                            {
                                instructionCounter = val2;
                                jump = true;
                            }
                            break;
                        case OpCode.jumpIfFalse:
                            if (val1 == 0)
                            {
                                instructionCounter = val2;
                                jump = true;
                            }
                            break;
                    }
                    if (!jump)
                        instructionCounter += 3;
                }
                else
                {
                    int val1 = parseMode(modes[0], memory[instructionCounter + 1]);
                    int val2 = parseMode(modes[1], memory[instructionCounter + 2]);
                    int outputLocation = memory[instructionCounter + 3];

                    switch (opcode)
                    {
                        case OpCode.add:
                            memory[outputLocation] = val1 + val2;
                            break;
                        case OpCode.multiply:
                            memory[outputLocation] = val1 * val2;
                            break;
                        case OpCode.lessThan:
                            memory[outputLocation] = val1 < val2 ? 1 : 0;
                            break;
                        case OpCode.equals:
                            memory[outputLocation] = val1 == val2 ? 1 : 0;
                            break;
                    }
                    instructionCounter += 4;
                }


                (modes, opcode) = parseInstruction(memory[instructionCounter]);
            }

            return this;
        }

        private int parseMode(Mode mode, int value)
        {
            if (mode == Mode.position)
                return memory[value];
            else
                return value;
        }

        public string Diagnose()
        {
            for (int i = 0; i < output.Count - 1; i++)
                if (output[i] != 0)
                    return "Diagonsis failed";

            return output.Last().ToString();
        }

        private (Mode[] modes, OpCode opcode) parseInstruction(int input)
        {
            string instruction = input.ToString("D5");

            OpCode opcode = Enum.Parse<OpCode>(instruction.Substring(3));
            Mode[] modes = instruction.Substring(0, 3).reverse()
                .Select<char, Mode>(c => Enum.Parse<Mode>(c.ToString()))
                .ToArray();

            return (modes, opcode);
        }

        enum Mode { position = 0, immediate = 1}
        enum OpCode { add = 1, multiply = 2, input = 3, output = 4, jumpIfTrue = 5, jumpIfFalse = 6, lessThan = 7, equals = 8, halt = 99};
    }
}
