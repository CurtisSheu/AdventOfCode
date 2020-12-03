using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day03 : ASolution
    {
        bool[,] trees;

        public Day03() : base(3, 2020, "Toboggan Trajectory") 
        {
            trees = Input.toTwoDimensionalBoolArray('.', '#');
        }

        protected override string solvePartOne()
        {
            return checkForTreesAlongSlope(3,1).ToString();
        }

        protected override string solvePartTwo()
        {
            List<(int run, int rise)> slopes = new List<(int run, int rise)>();
            slopes.Add((1, 1));
            slopes.Add((3, 1));
            slopes.Add((5, 1));
            slopes.Add((7, 1));
            slopes.Add((1, 2));

            return productOfTreesOfMultipleSlopes(slopes).ToString();
        }

        private int checkForTreesAlongSlope(int run, int rise)
        {
            int x = 0;
            int y = 0;

            int treeCount = 0;

            while (y < trees.GetLength(0))
            {
                if (trees[y, x])
                    treeCount++;

                x += run;
                if (x >= trees.GetLength(1))
                    x -= trees.GetLength(1);
                y += rise;
            }

            return treeCount;
        }

        private int productOfTreesOfMultipleSlopes(List<(int run, int rise)> slopes)
        {
            int product = 1;

            foreach ((int run, int rise) slope in slopes)
                product *= checkForTreesAlongSlope(slope.run, slope.rise);

            return product;
        }
    }
}
