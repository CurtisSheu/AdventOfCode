using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day08 : ASolution
    {
        int[] picture;
        public Day08() : base(8, 2019, "Space Image Format")
        {
            picture = Input.toIntArray();
        }

        protected override string solvePartOne()
        {
            return corruptionCheck(6, 25).ToString();
        }

        protected override string solvePartTwo()
        {
            int?[,] finalPicture = interpretPicture(6, 25);

            string result = "\n";
            for (int i = 0; i < finalPicture.GetLength(0); i++)
            {
                for (int j = 0; j < finalPicture.GetLength(1); j++)
                {
                    result += (finalPicture[i, j] == 0) ? " " : 0.ToString();
                }
                result += "\n";
            }

            return result;
        }

        private int corruptionCheck(int height, int width)
        {
            var layers = picture.Split(height * width);

            int lowestCountZeros = int.MaxValue, result = 0;
            foreach (var layer in layers)
            {
                int countZeros = 0, countOnes = 0, countTwos = 0;
                foreach (int pixel in layer)
                { 
                    if (pixel == 0)
                        countZeros++;
                    else if (pixel == 1)
                        countOnes++;
                    else if (pixel == 2)
                        countTwos++;
                }

                if (countZeros < lowestCountZeros)
                {
                    lowestCountZeros = countZeros;
                    result = countOnes * countTwos;
                }
            }
            
            return result;
        }

        private int?[,] interpretPicture(int height, int width)
        {
            int?[,] finalLayer = new int?[height, width];

            var layers = picture.Split(width).Split(height);

            foreach (var layer in layers)
            {
                int r = 0;
                foreach (var row in layer)
                {
                    int p = 0;
                    foreach(int pixel in row)
                    {
                        if (finalLayer[r, p] == null && pixel != 2)
                            finalLayer[r, p] = pixel;
                        p++;
                    }
                    r++;
                }
            }

            return finalLayer;
        }
    }
}
