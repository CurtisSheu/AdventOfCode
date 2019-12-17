using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2019
{
    class Day12 : ASolution
    {
        List<moon> allMoons;
        List<axisSystem> allAxes;
        public Day12() : base(12, 2019, "The N-Body Problem")
        {
            allMoons = new List<moon>();
            allAxes = new List<axisSystem>();
            allAxes.Add(new axisSystem());
            allAxes.Add(new axisSystem());
            allAxes.Add(new axisSystem());

            string pattern = @"<x=(?'x'-?\d+), y=(?'y'-?\d+), z=(?'z'-?\d+)>";

            foreach (Match m in Regex.Matches(Input, pattern))
            {
                int x = Convert.ToInt32(m.Groups["x"].Value);
                int y = Convert.ToInt32(m.Groups["y"].Value);
                int z = Convert.ToInt32(m.Groups["z"].Value);
                allMoons.Add(new moon(x, y, z));
                allAxes[0].addAxis(x);
                allAxes[1].addAxis(y);
                allAxes[2].addAxis(z);
            }
        }

        protected override string solvePartOne()
        {
            for (int steps = 0; steps < 1000; steps++)
            {
                foreach (moon m in allMoons)
                    m.applyGravity(allMoons.Where(w => w != m).ToList());

                foreach (moon m in allMoons)
                    m.applyVelocity();
            }

            int totalEnergy = 0;

            foreach (moon m in allMoons)
                totalEnergy += m.totalEnergy();

            return totalEnergy.ToString();
        }

        protected override string solvePartTwo()
        {
            List<long> periods = new List<long>();
            foreach (axisSystem axis in allAxes)
                periods.Add(axis.findPeriod());

            return Utility.LCM(periods).ToString();
        }
    }
}
