using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class reaction
    {
        public (long amount, string name) outputChemical;
        public List<(long amount, string name)> inputChemicals { get; }

        public reaction(long amount, string name)
        {
            outputChemical = (amount, name);
            inputChemicals = new List<(long amount, string name)>();
        }

        public void addInput((long amount, string name) newInput)
        {
            inputChemicals.Add(newInput);
        }
    }
}
