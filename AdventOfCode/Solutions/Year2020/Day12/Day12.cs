using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day12 : ASolution
    {
        InstructionsInterpreter instructionsInterpreter;

        public Day12() : base(12, 2020, "Rain Risk")
        {
            instructionsInterpreter = new InstructionsInterpreter();
        }

        protected override string solvePartOne()
        {
            return instructionsInterpreter.RunAndCalculateManhattanDistance(Input).ToString();
        }

        protected override string solvePartTwo()
        {
            return instructionsInterpreter.WaypointRunAndCalculateManhattanDistance(Input).ToString();
        }

        class InstructionsInterpreter
        {
            int x;
            int y;
            int waypointX;
            int waypointY;
            direction currentFacing;
            public InstructionsInterpreter()
            {
                reset();
            }

            private void reset()
            {
                x = 0;
                y = 0;
                currentFacing = direction.east;
            }

            public int RunAndCalculateManhattanDistance(string input)
            {
                reset();
                string pattern = @"(?'command'[SENWLRF])(?'magnitude'\d+)";
                foreach (Match m in Regex.Matches(input, pattern)) 
                {
                    char command = m.Groups["command"].Value[0];
                    int magnitude = Convert.ToInt32(m.Groups["magnitude"].Value);

                    switch (command)
                    {
                        case 'E':
                            moveInDirection(direction.east, magnitude, ref x, ref y);
                            break;
                        case 'S':
                            moveInDirection(direction.south, magnitude, ref x, ref y);
                            break;
                        case 'W':
                            moveInDirection(direction.west, magnitude, ref x, ref y);
                            break;
                        case 'N':
                            moveInDirection(direction.north, magnitude, ref x, ref y);
                            break;
                        case 'F':
                            moveInDirection(currentFacing, magnitude, ref x, ref y);
                            break;
                        case 'L':
                            turn(turnDirection.left, magnitude);
                            break;
                        case 'R':
                            turn(turnDirection.right, magnitude);
                            break;
                    }
                }

                return Utility.calculateManhattanDistance((0, 0), (x, y));
            }

            public int WaypointRunAndCalculateManhattanDistance(string input, int wayX = 10, int wayY = 1)
            {
                reset();
                waypointX = wayX;
                waypointY = wayY;

                string pattern = @"(?'command'[SENWLRF])(?'magnitude'\d+)";
                foreach (Match m in Regex.Matches(input, pattern))
                {
                    char command = m.Groups["command"].Value[0];
                    int magnitude = Convert.ToInt32(m.Groups["magnitude"].Value);

                    switch (command)
                    {
                        case 'E':
                            moveInDirection(direction.east, magnitude, ref waypointX, ref waypointY);
                            break;
                        case 'S':
                            moveInDirection(direction.south, magnitude, ref waypointX, ref waypointY);
                            break;
                        case 'W':
                            moveInDirection(direction.west, magnitude, ref waypointX, ref waypointY);
                            break;
                        case 'N':
                            moveInDirection(direction.north, magnitude, ref waypointX, ref waypointY);
                            break;
                        case 'F':
                            x += waypointX * magnitude;
                            y += waypointY * magnitude;
                            break;
                        case 'L':
                            turnWaypoint(turnDirection.left, magnitude);
                            break;
                        case 'R':
                            turnWaypoint(turnDirection.right, magnitude);
                            break;
                    }
                }

                return Utility.calculateManhattanDistance((0, 0), (x, y));
            }

            private void moveInDirection(direction directionToMove, int magnitude, ref int xToMove, ref int yToMove)
            {
                switch(directionToMove)
                {
                    case direction.east:
                        xToMove += magnitude;
                        break;
                    case direction.south:
                        yToMove -= magnitude;
                        break;
                    case direction.west:
                        xToMove -= magnitude;
                        break;
                    case direction.north:
                        yToMove += magnitude;
                        break;
                }
            }

            private void turn(turnDirection direction, int degrees)
            {
                currentFacing = (direction)(((int)currentFacing + (int)direction * (degrees / 90)).mod(4));
            }

            private void turnWaypoint(turnDirection direction, int degrees)
            {
                int loops = degrees / 90;

                for (int i = 0; i < loops; i++)
                {
                    int tempWaypointX = 0;
                    int tempWaypointY = 0;
                    switch(direction)
                    {
                        case turnDirection.left:
                            tempWaypointX = waypointY * -1;
                            tempWaypointY = waypointX;
                            break;
                        case turnDirection.right:
                            tempWaypointX = waypointY;
                            tempWaypointY = waypointX * -1;
                            break;
                    }

                    waypointX = tempWaypointX;
                    waypointY = tempWaypointY;
                }
            }

            enum direction { east = 0, south = 1, west = 2, north = 3}
            enum turnDirection { left = -1, right = 1 }
        }
    }
}