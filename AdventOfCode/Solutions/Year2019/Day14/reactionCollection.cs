using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2019
{
    class reactionCollection
    {
        Dictionary<string, reaction> allReactions = new Dictionary<string, reaction>();
        Dictionary<string, long> leftOverMaterials = new Dictionary<string, long>();

        public reactionCollection(string[] reactionLines)
        {
            string pattern = @"(?'qty'\d+) (?'name'[A-Z]+)";

            foreach (string reaction in reactionLines)
            {
                MatchCollection matches = Regex.Matches(reaction, pattern);

                string outputChemical = matches[matches.Count - 1].Groups["name"].Value;
                long outputAmount = Convert.ToInt64(matches[matches.Count - 1].Groups["qty"].Value);

                allReactions.Add(outputChemical, new reaction(outputAmount, outputChemical));

                for (int i = 0; i < matches.Count - 1; i++)
                    allReactions[outputChemical].addInput((Convert.ToInt64(matches[i].Groups["qty"].Value), matches[i].Groups["name"].Value));
            }
        }

        public long calculateFuelGivenOreAmount(long oreAmount)
        {
            long baseOreValue = calculateOreAmounts(1, "FUEL");
            oreAmount -= baseOreValue;
            long fuel = 1;

            long prediction = oreAmount / baseOreValue / 2;
            
            while (true)
            {
                var tempLeftOverMaterials = leftOverMaterials.ToDictionary(entry => entry.Key, entry => entry.Value);
                long oreToConsume = calculateOreAmounts((long)prediction, "FUEL");

                if (oreToConsume > oreAmount)
                {
                    if (prediction <= 1)
                        break;
                    prediction /= 2;
                    leftOverMaterials = tempLeftOverMaterials.ToDictionary(entry => entry.Key, entry => entry.Value);
                }
                else
                {
                    oreAmount -= oreToConsume;
                    fuel += (long)prediction;
                }
            }


            return fuel;
        }

        public long calculateOreAmounts(long inputAmount, string chemicalInput)
        {
            long oreAmount = 0;

            long amountProduced = allReactions[chemicalInput].outputChemical.amount;
            long timesToRun = inputAmount / amountProduced + 1;
            if (inputAmount % amountProduced == 0)
                timesToRun--;

            foreach ((long amount, string name) input in allReactions[chemicalInput].inputChemicals)
            {
                long inputNeeded = checkReserves(timesToRun * input.amount, input.name);

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

        private long checkReserves(long amountNeeded, string chemicalInput)
        {
            if (leftOverMaterials.ContainsKey(chemicalInput))
            {
                long amountNeededAfterReserves = amountNeeded - leftOverMaterials[chemicalInput];
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
