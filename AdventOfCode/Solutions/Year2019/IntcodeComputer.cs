using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    class IntcodeComputer<T> where T : IComparable<T>
    {
        private T[] intCode;
        public Dictionary<long, T> memory { get; private set; }

        public bool paused { get; private set; }
        public Queue<T> input { get; private set; }
        public Queue<T> output { get; private set; }

        private long instructionCounter;
        private long relativeBase;

        public IntcodeComputer(T[] input)
        {
            intCode = input;
        }

        public IntcodeComputer<T> initialize(int? noun = null, int? verb = null)
        {
            instructionCounter = 0;
            relativeBase = 0;
            copyIntcodeToMemory();
            if (noun != null) memory[1] = (T)(object)noun.Value;
            if (verb != null) memory[2] = (T)(object)verb.Value;
            input = new Queue<T>();
            output = new Queue<T>();
            return this;
        }

        private void copyIntcodeToMemory()
        {
            long x = 0;
            memory = new Dictionary<long, T>();
            foreach(T i in intCode)
            {
                memory.Add(x, i);
                x++;
            }
        }

        public IntcodeComputer<T> inputSequence(params T[] inp)
        {
            foreach (T i in inp)
                input.Enqueue(i);

            return this;
        }

        public IntcodeComputer<T> run(int? inp = null)
        {
            if (inp != null) inputSequence((T)(object)inp.Value);
            paused = false;

            (Mode[] modes, OpCode opcode) = parseInstruction(memory[instructionCounter]);

            while (opcode != OpCode.halt)
            {
                if (opcode == OpCode.input || opcode == OpCode.output || opcode == OpCode.adjust)
                {
                    switch (opcode)
                    {
                        case OpCode.input:
                            if (input.Count > 0)
                            {
                                parseWriteMode(modes[0], memory[instructionCounter + 1], input.Dequeue());
                            }
                            else
                            {
                                paused = true;
                                return this;
                            }
                            break;
                        case OpCode.output:
                            dynamic val1 = parseReadMode(modes[0], memory[instructionCounter + 1]);
                            output.Enqueue(val1);
                            break;
                        case OpCode.adjust:
                            relativeBase = relativeBase + (dynamic)parseReadMode(modes[0], memory[instructionCounter + 1]);
                            break;
                    }
                    instructionCounter += 2;
                }
                else if (opcode == OpCode.jumpIfFalse || opcode == OpCode.jumpIfTrue)
                {
                    dynamic val1 = parseReadMode(modes[0], memory[instructionCounter + 1]);
                    dynamic val2 = parseReadMode(modes[1], memory[instructionCounter + 2]);
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
                else if (opcode == OpCode.add || opcode == OpCode.multiply || opcode == OpCode.lessThan || opcode == OpCode.equals)
                {
                    dynamic val1 = parseReadMode(modes[0], memory[instructionCounter + 1]);
                    dynamic val2 = parseReadMode(modes[1], memory[instructionCounter + 2]);
                    dynamic valueToWrite = 0;

                    switch (opcode)
                    {
                        case OpCode.add:
                            valueToWrite = val1 + val2;
                            break;
                        case OpCode.multiply:
                            valueToWrite = val1 * val2;
                            break;
                        case OpCode.lessThan:
                            valueToWrite = val1 < val2 ? 1 : 0;
                            break;
                        case OpCode.equals:
                            valueToWrite = val1 == val2 ? 1 : 0;
                            break;
                    }

                    parseWriteMode(modes[2], memory[instructionCounter + 3], valueToWrite);
                    instructionCounter += 4;
                }
                else
                    throw new NotImplementedException();


                (modes, opcode) = parseInstruction(memory[instructionCounter]);
            }

            return this;
        }

        public string Diagnose()
        {
            while (output.Count > 1)
                if ((int)(object)output.Dequeue() != 0)
                    return "Diagonsis failed";

            return output.Dequeue().ToString();
        }

        private T parseReadMode(Mode mode, T value)
        {
            if (mode == Mode.position)
            {
                if (!memory.ContainsKey((dynamic)value))
                    memory.Add((dynamic)value, default(T));

                return memory[(dynamic)value];
            }
            else if (mode == Mode.relative)
            {
                if (!memory.ContainsKey((dynamic)value + relativeBase))
                    memory.Add((dynamic)value + relativeBase, default(T));

                return memory[(dynamic)value + relativeBase];
            }
            else
                return value;
        }

        private void parseWriteMode(Mode mode, T writeLocation, T valueToWrite)
        {
            if (mode == Mode.position)
                memory[(dynamic)writeLocation] = valueToWrite;
            else if (mode == Mode.relative)
                memory[(dynamic)writeLocation + relativeBase] = valueToWrite;
            else
                throw new NotImplementedException();
        }

        private (Mode[] modes, OpCode opcode) parseInstruction(T input)
        {
            string instruction = Convert.ToInt32(input).ToString("D5");

            OpCode opcode = Enum.Parse<OpCode>(instruction.Substring(3));
            Mode[] modes = instruction.Substring(0, 3).reverse()
                .Select<char, Mode>(c => Enum.Parse<Mode>(c.ToString()))
                .ToArray();

            return (modes, opcode);
        }

        enum Mode { position = 0, immediate = 1, relative = 2}
        enum OpCode { add = 1, multiply = 2, input = 3, output = 4, jumpIfTrue = 5, jumpIfFalse = 6, lessThan = 7, equals = 8, adjust=9, halt = 99};
    }
}
