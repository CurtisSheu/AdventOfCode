using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class reaction
    {
        public (int amount, string name) outputChemical;
        public List<(int amount, string name)> inputChemicals { get; }

        public reaction(int amount, string name)
        {
            outputChemical = (amount, name);
            inputChemicals = new List<(int amount, string name)>();
        }

        public void addInput((int amount, string name) newInput)
        {
            inputChemicals.Add(newInput);
        }
    }
}
