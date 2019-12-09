using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions
{
    class SolutionCollector : IEnumerable<ASolution>
    {
        IEnumerable<ASolution> solutions;

        public SolutionCollector(int year, int[] days) => solutions = loadSolutions(year, days);

        public ASolution getSolution(int day)
        {
            try
            {
                return solutions.Single(solutions => solutions.day == day);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public IEnumerator<ASolution> GetEnumerator()
        {
            return solutions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerable<ASolution> loadSolutions(int year, int[] days)
        {
            foreach (int day in days)
            {
                var solution = Type.GetType($"AdventOfCode.Solutions.Year{year}.Day{day.ToString("D2")}");
                if (solution != null)
                    yield return (ASolution)Activator.CreateInstance(solution);
            }
        }
    }
}
