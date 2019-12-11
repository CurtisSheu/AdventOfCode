using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    class Day07 : ASolution
    {
        IntcodeComputer<int> amplifier;
        int[] initialMemory;
        public Day07() : base(7, 2019, "Amplification Circuit")
        {
            initialMemory = Input.toIntArray(",");
            amplifier = new IntcodeComputer<int>(initialMemory);
        }

        protected override string solvePartOne()
        {
            var permutations = Enumerable.Range(0, 5).getPermutations();
            int highestOutput = int.MinValue;
            foreach (var permutation in permutations)
            {
                int output = 0;
                foreach (int i in permutation.ToArray())
                {
                    output = amplifier.initialize().inputSequence(i, output).run().output.Dequeue();
                }
                if (output > highestOutput) highestOutput = output;
            }
            return highestOutput.ToString();
        }

        protected override string solvePartTwo()
        {
            var permutations = Enumerable.Range(5, 5).getPermutations();
            int highestOutput = int.MinValue;
            foreach (var permutation in permutations)
            {
                var amplifiers = new Queue<IntcodeComputer<int>>();
                for (int i = 0; i < 5; i++) amplifiers.Enqueue(new IntcodeComputer<int>(initialMemory).initialize().inputSequence(permutation.ToArray()[i]));

                int output = 0;
                
                while (amplifiers.Count > 0)
                {
                    var amp = amplifiers.Dequeue();
                    amp.inputSequence(output);

                    if (amp.run().paused)
                        amplifiers.Enqueue(amp);

                    output = amp.output.Dequeue();
                }

                if (output > highestOutput) highestOutput = output;
            }
            return highestOutput.ToString();
        }
    }
}
