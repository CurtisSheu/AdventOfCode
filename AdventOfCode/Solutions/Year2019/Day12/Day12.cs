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

    class moon
    {
        (int x, int y, int z) currentPosition;
        (int x, int y, int z) currentVelocity;

        public moon(int x, int y, int z)
        {
            currentPosition = (x, y, z);
            currentVelocity = (0, 0, 0);
        }

        public void applyGravity(List<moon> otherMoons)
        {
            foreach (moon other in otherMoons)
            {
                if (currentPosition.x < other.currentPosition.x)
                    currentVelocity.x++;
                else if (currentPosition.x > other.currentPosition.x)
                    currentVelocity.x--;

                if (currentPosition.y < other.currentPosition.y)
                    currentVelocity.y++;
                else if (currentPosition.y > other.currentPosition.y)
                    currentVelocity.y--;

                if (currentPosition.z < other.currentPosition.z)
                    currentVelocity.z++;
                else if (currentPosition.z > other.currentPosition.z)
                    currentVelocity.z--;
            }
        }

        public void applyVelocity()
        {
            currentPosition.x += currentVelocity.x;
            currentPosition.y += currentVelocity.y;
            currentPosition.z += currentVelocity.z;
        }

        public int totalEnergy() => potentialEnergy() * kineticEnergy();
        private int potentialEnergy() => Math.Abs(currentPosition.x) + Math.Abs(currentPosition.y) + Math.Abs(currentPosition.z);
        private int kineticEnergy() => Math.Abs(currentVelocity.x) + Math.Abs(currentVelocity.y) + Math.Abs(currentVelocity.z);
    }

    class axisSystem
    {
        List<int> axisPosition;
        List<int> axisVelocity;
        public axisSystem()
        {
            axisPosition = new List<int>();
            axisVelocity = new List<int>();
        }

        public void addAxis(int x)
        {
            axisPosition.Add(x);
            axisVelocity.Add(0);
        }

        public long findPeriod()
        {
            List<int> initialAxisPosition = new List<int>(axisPosition);
            List<int> initialAxisVelocity = new List<int>(axisVelocity);
            
            long period = 0;
            while (true)
            {
                stepForwardOneTimeUnit();

                period++;
                if (axisPosition.SequenceEqual(initialAxisPosition) && axisVelocity.SequenceEqual(initialAxisVelocity))
                    return period;
            }
        }

        private void stepForwardOneTimeUnit()
        {
            for (int i = 0; i < axisPosition.Count; i++)
                for (int j = 0; j < axisPosition.Count; j++)
                    if (i != j)
                        axisVelocity[i] += modifyVelocity(axisPosition[i], axisPosition[j]);

            for (int i = 0; i < axisPosition.Count; i++)
                axisPosition[i] += axisVelocity[i];
        }

        private int modifyVelocity(int pos1, int pos2)
        {
            if (pos1 < pos2)
                return 1;
            else if (pos1 > pos2)
                return -1;

            return 0;
        }
    }
}
