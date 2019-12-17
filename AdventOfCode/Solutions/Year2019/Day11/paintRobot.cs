using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class paintRobot
    {
        IntcodeComputer<long> robotBrain;
        Dictionary<(int x, int y), paintColor> paintGrid;
        HashSet<(int x, int y)> painted;
        (int x, int y) currentPosition;
        robotFacing currentFacing;


        public paintRobot(long[] input, paintColor initialColor)
        {
            robotBrain = new IntcodeComputer<long>(input).initialize();
            paintGrid = new Dictionary<(int x, int y), paintColor>();
            painted = new HashSet<(int x, int y)>();
            currentPosition = (0, 0);
            paintGrid[currentPosition] = initialColor;
            currentFacing = robotFacing.north;
        }

        public void run()
        {
            while (robotBrain.inputSequence((long)paintGrid[currentPosition]).run().paused)
            {
                paintColor newColor = (paintColor)robotBrain.output.Dequeue();
                paintGrid[currentPosition] = newColor;

                if (!painted.Contains(currentPosition)) painted.Add(currentPosition);

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
        }

        public int numberOfPaintedTiles() => painted.Count;

        public string printPaintGrid()
        {
            int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;

            foreach ((int x, int y) coord in paintGrid.Keys)
            {
                if (coord.x < minX) minX = coord.x;
                if (coord.y < minY) minY = coord.y;
                if (coord.x > maxX) maxX = coord.x;
                if (coord.y > maxY) maxY = coord.y;
            }

            string result = "\n";


            for (int y = maxY; y >= minY; y--)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    result += paintGrid.ContainsKey((x, y)) && paintGrid[(x, y)] == paintColor.white ? 0.ToString() : " ";
                }
                result += "\n";
            }
            return result;
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
    }

    enum robotFacing { north = 0, east = 1, south = 2, west = 3 }
    enum paintColor { black = 0, white = 1 }
    enum turnInstruction { turnLeft = 0, turnRight = 1 }
}
