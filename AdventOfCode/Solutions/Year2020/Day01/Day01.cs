using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day01 : ASolution
    {
        public Day01() : base(1, 2020, "Report Repair") { }

        protected override string solvePartOne()
        {
            return findProductOfTwoNumbers(Input.toIntArray("\n"), 2020).ToString();
        }

        protected override string solvePartTwo()
        {
            return findProductOfThreeNumbers(Input.toIntArray("\n"), 2020).ToString();
        }

        private int findProductOfTwoNumbers(int[] values, int sum)
        {
            for (int i = 0; i < values.Length; i++)
                for (int j = i + 1; j < values.Length; j++)
                    if (values[i] + values[j] == sum)
                        return values[i] * values[j];

            return -1;
        }

        private int findProductOfThreeNumbers(int[] values, int sum)
        {
            for (int i = 0; i < values.Length; i++)
                for (int j = i + 1; j < values.Length; j++)
                    for (int k = j + 1; k < values.Length; k++)
                        if (values[i] + values[j] + values[k] == sum)
                            return values[i] * values[j] * values[k];

            return -1;
        }
    }
}
