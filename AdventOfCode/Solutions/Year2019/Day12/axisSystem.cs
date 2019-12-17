using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
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
