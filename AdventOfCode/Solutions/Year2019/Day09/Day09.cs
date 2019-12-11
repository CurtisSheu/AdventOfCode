using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day09 : ASolution
    {
        IntcodeComputer<long> boostComputer;
        public Day09() : base(9, 2019, "Sensor Boost")
        {
            boostComputer = new IntcodeComputer<long>(Input.toLongArray(","));
        }

        protected override string solvePartOne()
        {
            return boostComputer.initialize().inputSequence(1).run().output.Dequeue().ToString();
        }

        protected override string solvePartTwo()
        {
            return boostComputer.initialize().inputSequence(2).run().output.Dequeue().ToString();
        }
    }
}
