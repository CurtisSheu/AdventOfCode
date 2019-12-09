using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day05 : ASolution
    {
        IntcodeComputer machine;

        public Day05() : base(5, 2019, "Sunny with a Chance of Asteroids")
        {
            machine = new IntcodeComputer(Input.toIntArray(","));
        }
            
        protected override string solvePartOne()
        {
            machine.initialize().run(1);
            return machine.Diagnose();
        }

        protected override string solvePartTwo()
        {
            machine.initialize().run(5);
            return machine.Diagnose();
        }
    }
}
