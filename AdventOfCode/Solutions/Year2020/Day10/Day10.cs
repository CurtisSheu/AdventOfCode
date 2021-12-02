using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    class Day10 : ASolution
    {
        AdapterArray adapterArray;
        public Day10() : base(10, 2020, "Adapter Array")
        {
            adapterArray = new AdapterArray(Input);
        }

        protected override string solvePartOne()
        {
            return (adapterArray.differences[1] * adapterArray.differences[3]).ToString();
        }

        protected override string solvePartTwo()
        {
            return adapterArray.CountPaths().ToString();
        }

        class AdapterArray
        {
            SortedSet<int> adapters;
            public Dictionary<int, int> differences = new Dictionary<int, int>();

            public AdapterArray(string input)
            {
                adapters = new SortedSet<int>(input.toIntArray("\r\n"));
                fillOutDifferences();
            }

            private void fillOutDifferences()
            {
                int currentVoltage = 0;

                foreach(int i in adapters)
                {
                    int difference = i - currentVoltage;

                    if (!differences.ContainsKey(difference))
                        differences.Add(difference, 0);

                    differences[difference]++;

                    currentVoltage = i;
                }

                differences[3]++;
            }

            public long CountPaths()
            {
                SortedDictionary<int, long> paths = new SortedDictionary<int, long>();
                adapters.Add(0);
                paths.Add(0, 1);

                foreach(int i in adapters)
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        if (adapters.Contains(j + i))
                        {
                            if (!paths.ContainsKey(j + i))
                                paths.Add(j + i, 0);

                            paths[j + i] += paths[i];
                        }
                    }
                }

                return paths.Values.Last();
            }
        }
    }
}