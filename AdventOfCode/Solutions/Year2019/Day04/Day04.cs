using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day04 : ASolution
    {
        int minRange;
        int maxRange;

        public Day04() : base(4, 2019, "Secure Container")
        {
            int[] range = Input.toIntArray("-");
            minRange = range[0];
            maxRange = range[1];
        }

        protected override string solvePartOne()
        {
            int count = 0;
            for (int i = minRange; i <= maxRange; i++)
                if (checkPasswordRequirements(i))
                    count++;

            return count.ToString(); ;
        }

        protected override string solvePartTwo()
        {
            int count = 0;
            for (int i = minRange; i <= maxRange; i++)
                if (checkPasswordRequirements(i, true))
                    count++;

            return count.ToString();
        }

        private bool checkPasswordRequirements(int input, bool part2 = false)
        {
            int[] digits = input.toDigitArray();
            bool doub = false;

            for (int j = 1; j < digits.Length; j++)
            {
                if (digits[j] < digits[j - 1])
                    return false;
                else if (digits[j] == digits[j - 1])
                {
                    if (part2)
                    {
                        bool secondCheck = true;
                        if (j - 2 >= 0 && digits[j - 2] == digits[j])
                            secondCheck = false;
                        if (j + 1 < digits.Length && digits[j + 1] == digits[j])
                            secondCheck = false;

                        if (secondCheck)
                            doub = true;
                    }
                    else doub = true;
                }
            }

            return doub;
        }
    }
}
