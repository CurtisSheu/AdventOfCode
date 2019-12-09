using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day03 : ASolution
    {
        Dictionary<(int x, int y), int> wire1 = new Dictionary<(int, int), int>();
        Dictionary<(int x, int y), int> wire2 = new Dictionary<(int, int), int>();

        public Day03() : base(3, 2019, "Crossed Wires")
        {
            string[] wires = Input.splitByNewLine();
            evaluateWire(wires[0].Split(','), wire1);
            evaluateWire(wires[1].Split(','), wire2);
        }

        protected override string solvePartOne()
        {
            int minimumDistance = int.MaxValue;
            foreach(KeyValuePair<(int, int), int> pos in wire1)
                if(wire2.ContainsKey(pos.Key) && calculateManhattanDistance(pos.Key) < minimumDistance)
                    minimumDistance = calculateManhattanDistance(pos.Key);

            return minimumDistance.ToString();
        }

        protected override string solvePartTwo()
        {
            int minimumDistance = int.MaxValue;
            foreach (KeyValuePair<(int, int), int> pos in wire1)
                if(wire2.ContainsKey(pos.Key) && pos.Value + wire2[pos.Key] < minimumDistance)
                    minimumDistance = pos.Value+ wire2[pos.Key];

            return minimumDistance.ToString();
        }

        private int calculateManhattanDistance((int x, int y) pos)
        {
            return Math.Abs(pos.x) + Math.Abs(pos.y);
        }

        private void evaluateWire(string[] instructions, Dictionary<(int, int), int> wireLengths)
        {
            (int x, int y) pos = (0, 0);
            int wireLength = 0;
            foreach(string instruction in instructions)
            {
                int length = int.Parse(instruction.Substring(1));
                switch (instruction[0])
                {
                    case 'R':
                        for (int i = 0; i < length; i++)
                        {
                            pos.x++;
                            wireLength++;
                            evaluatePosition(pos, wireLengths, wireLength);
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < length; i++)
                        {
                            pos.x--;
                            wireLength++;
                            evaluatePosition(pos, wireLengths, wireLength);
                        }
                        break;
                    case 'U':
                        for (int i = 0; i < length; i++)
                        {
                            pos.y++;
                            wireLength++;
                            evaluatePosition(pos, wireLengths, wireLength);
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < length; i++)
                        {
                            pos.y--;
                            wireLength++;
                            evaluatePosition(pos, wireLengths, wireLength);
                        }
                        break;
                }
            }
        }

        private void evaluatePosition((int x, int y) pos, Dictionary<(int, int), int> wireLengths, int wireLength)
        {
            if (!wireLengths.ContainsKey(pos)) wireLengths[pos] = wireLength;
        }
    }
}
