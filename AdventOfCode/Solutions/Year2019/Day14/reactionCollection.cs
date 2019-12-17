using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2019
{
    class reactionCollection
    {
        Dictionary<string, reaction> allReactions = new Dictionary<string, reaction>();
        Dictionary<string, int> leftOverMaterials = new Dictionary<string, int>();

        public reactionCollection(string[] reactionLines)
        {
            string pattern = @"(?'qty'\d+) (?'name'[A-Z]+)";

            foreach (string reaction in reactionLines)
            {
                MatchCollection matches = Regex.Matches(reaction, pattern);

                string outputChemical = matches[matches.Count - 1].Groups["name"].Value;
                int outputAmount = Convert.ToInt32(matches[matches.Count - 1].Groups["qty"].Value);

                allReactions.Add(outputChemical, new reaction(outputAmount, outputChemical));

                for (int i = 0; i < matches.Count - 1; i++)
                    allReactions[outputChemical].addInput((Convert.ToInt32(matches[i].Groups["qty"].Value), matches[i].Groups["name"].Value));
            }
        }

        public int calculateFuelGivenOreAmount(long oreAmount)
        {
            int fuel = 0;
            while (oreAmount > 0)
            {
                oreAmount -= calculateOreAmounts(1, "FUEL");
                fuel++;
            }

            if (oreAmount < 0)
                fuel--;

            return fuel;
        }

        public int calculateOreAmounts(int inputAmount, string chemicalInput)
        {
            int oreAmount = 0;

            int amountProduced = allReactions[chemicalInput].outputChemical.amount;
            int timesToRun = inputAmount / amountProduced + 1;
            if (inputAmount % amountProduced == 0)
                timesToRun--;

            foreach ((int amount, string name) input in allReactions[chemicalInput].inputChemicals)
            {
                int inputNeeded = checkReserves(timesToRun * input.amount, input.name);

                if (inputNeeded > 0)
                {
                    if (input.name == "ORE")
                        oreAmount += inputNeeded;
                    else
                        oreAmount += calculateOreAmounts(inputNeeded, input.name);
                }
            }

            if (!leftOverMaterials.ContainsKey(chemicalInput))
                leftOverMaterials.Add(chemicalInput, 0);

            leftOverMaterials[chemicalInput] += timesToRun * amountProduced - inputAmount;

            return oreAmount;
        }

        private int checkReserves(int amountNeeded, string chemicalInput)
        {
            if (leftOverMaterials.ContainsKey(chemicalInput))
            {
                int amountNeededAfterReserves = amountNeeded - leftOverMaterials[chemicalInput];
                leftOverMaterials[chemicalInput] -= amountNeeded;

                if (leftOverMaterials[chemicalInput] <= 0)
                    leftOverMaterials.Remove(chemicalInput);

                return amountNeededAfterReserves;
            }
            else
            {
                return amountNeeded;
            }
        }

        public void resetReserves() => leftOverMaterials.Clear();
    }
}
