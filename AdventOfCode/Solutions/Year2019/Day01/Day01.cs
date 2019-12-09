using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019
{
    class Day01 : ASolution
    {
        public Day01() : base(1, 2019, "The Tyranny of the Rocket Equation") { }

        protected override string solvePartOne()
        {
            return Input.toIntArray("\n").Select(Fuel).Sum().ToString();
        }

        protected override string solvePartTwo()
        {
            return Input.toIntArray("\n").Select(fuelRecursive).Sum().ToString();
        }

        private int Fuel(int module)
        {
            return module / 3 - 2;
        }

        private int fuelRecursive(int mass)
        {
            int newAmount = mass / 3 - 2;
            if (newAmount <= 0)
                return 0;
            else
                return newAmount + fuelRecursive(newAmount);
        }
    }
}
