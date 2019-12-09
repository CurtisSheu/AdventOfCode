using AdventOfCode.Solutions;
using System;

namespace AdventOfCode
{
    class Program
    {
        public static Config config = Config.Get("config.json");
        static SolutionCollector solutions = new SolutionCollector(config.Year, config.Days);

        static void Main(string[] args)
        {
            foreach(ASolution solution in solutions)
            {
                solution.Solve();
            }
            Console.Read();
        }
    }
}
