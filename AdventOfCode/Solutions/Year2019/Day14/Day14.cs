using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2019
{
    class Day14 : ASolution
    {
        reactionCollection allReactions;

        public Day14() : base(14, 2019, "Space Stoichiometry")
        {
            allReactions = new reactionCollection(Input.splitByNewLine());
        }

        protected override string solvePartOne()
        {
            allReactions.resetReserves();
            return allReactions.calculateOreAmounts(1, "FUEL").ToString();
        }

        protected override string solvePartTwo()
        {
            allReactions.resetReserves();
            return allReactions.calculateFuelGivenOreAmount(1000000000000).ToString();
        }
    }
}
