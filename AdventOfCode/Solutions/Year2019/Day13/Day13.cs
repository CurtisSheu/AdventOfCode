using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day13 : ASolution
    {
        breakoutGame breakout;
        public Day13() : base(13, 2019, "Care Package")
        {
            breakout = new breakoutGame(Input.toLongArray(","));
        }

        protected override string solvePartOne()
        {
            return breakout.run().ToString();
        }

        protected override string solvePartTwo()
        {
            return breakout.run(true).ToString();
        }
    }
}
