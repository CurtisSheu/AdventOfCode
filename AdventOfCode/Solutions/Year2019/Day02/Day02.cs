using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day02 : ASolution
    {
        IntcodeComputer machine;

        public Day02() : base(2, 2019, "1202 Program Alarm")
        {
            machine = new IntcodeComputer(Input.toIntArray(","));
        }

        protected override string solvePartOne()
        {
            return machine.initialize(12, 2).run().memory[0].ToString();
        }

        protected override string solvePartTwo()
        {
            for (int i = 0; i <= 99; i++)
                for (int j = 0; j <= 99; j++)
                    if (machine.initialize(i, j).run().memory[0] == 19690720)
                        return (100 * i + j).ToString();

            return "";
        }
    }
}
