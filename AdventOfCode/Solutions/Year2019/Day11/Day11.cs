using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day11 : ASolution
    {
        IntcodeComputer<Int64> robotBrain;

        public Day11() : base(11, 2019, "Space Police")
        {
            robotBrain = new IntcodeComputer<long>(Input.toLongArray(",")).initialize();
        }

        protected override string solvePartOne()
        {
            Dictionary<(int x, int y), paintColor> paintGrid = new Dictionary<(int x, int y), paintColor>();
            (int x, int y) currentPosition = (0, 0);
            paintGrid[currentPosition] = paintColor.black;
            robotFacing currentFacing = robotFacing.north;

            while (robotBrain.inputSequence((long)paintGrid[currentPosition]).run().paused)
            {
                paintColor newColor = (paintColor)robotBrain.output.Dequeue();
                paintGrid[currentPosition] = newColor;

                currentFacing = interpretNewTurnInstruction(currentFacing, (turnInstruction)robotBrain.output.Dequeue());

                switch (currentFacing)
                {
                    case robotFacing.north:
                        currentPosition.y++;
                        break;
                    case robotFacing.east:
                        currentPosition.x++;
                        break;
                    case robotFacing.south:
                        currentPosition.y--;
                        break;
                    case robotFacing.west:
                        currentPosition.x--;
                        break;
                }

                if (!paintGrid.ContainsKey(currentPosition))
                    paintGrid[currentPosition] = paintColor.black;
            }

            return (paintGrid.Count - 1).ToString();
        }

        protected override string solvePartTwo()
        {
            return "";
        }

        private robotFacing interpretNewTurnInstruction(robotFacing currentFacing, turnInstruction instruction)
        {
            int integerRepresentation = (int)currentFacing;
            if (instruction == turnInstruction.turnLeft)
                integerRepresentation--;
            else
                integerRepresentation++;

            if (integerRepresentation < 0) integerRepresentation = 3;
            if (integerRepresentation > 3) integerRepresentation = 0;

            return (robotFacing)integerRepresentation;
        }

        enum robotFacing { north = 0, east = 1, south = 2, west = 3 }
        enum paintColor { black = 0, white = 1}
        enum turnInstruction { turnLeft = 0, turnRight = 1 }
    }
}
