using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day09 : ASolution
    {
        CypherDecoder cypherDecoder;
        public Day09() : base(9, 2020, "Encoding Error")
        {
            cypherDecoder = new CypherDecoder(Input);
        }

        protected override string solvePartOne()
        {
            return cypherDecoder.FindFaultyValue().ToString();
        }

        protected override string solvePartTwo()
        {
            return cypherDecoder.FindEncryptionWeakness().ToString();
        }

        class CypherDecoder
        {
            int[] sequence;
            int bufferSize;
            public CypherDecoder(string input)
            {
                sequence = input.toIntArray("\r\n");
                bufferSize = 25;
            }

            public int FindFaultyValue()
            {
                int first = 0;
                int last = bufferSize;

                for(int i = bufferSize; i < sequence.Length; i++)
                {
                    if (!findValidSum(first, last, sequence[i]))
                    {
                        return sequence[i];
                    }
                    else
                    {
                        first++;
                        last++;
                    }
                }

                return -1;
            }

            public int FindEncryptionWeakness()
            {
                int faultyValue = FindFaultyValue();

                int sum = sequence[0];
                int first, last = first = 0;

                while (true)
                {
                    if (sum == faultyValue)
                    {
                        int min, max = min = sequence[first];
                        for (int i = first; i <= last; i++)
                        {
                            if (sequence[i] > max)
                                max = sequence[i];
                            else if (sequence[i] < min)
                                min = sequence[i];
                        }
                        return max + min;
                    }
                    else if (sum > faultyValue)
                    {
                        sum -= sequence[first];
                        first++;
                    }
                    else if (sum < faultyValue)
                    {
                        last++;
                        sum += sequence[last];
                    }
                }
            }

            private bool findValidSum(int first, int last, int sum)
            {
                for (int i = first; i < last; i++)
                {
                    for (int j = i + 1; j < last; j++)
                    {
                        if (sequence[i] + sequence[j] == sum && sequence[i] * 2 != sum)
                            return true;
                    }
                }

                return false;
            }
        }
    }
}