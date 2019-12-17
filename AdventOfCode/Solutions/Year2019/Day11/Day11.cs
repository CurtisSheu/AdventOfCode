using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day11 : ASolution
    {
        public Day11() : base(11, 2019, "Space Police") { }

        protected override string solvePartOne()
        {
            paintRobot robot = new paintRobot(Input.toLongArray(","), paintColor.black);
            robot.run();
            return robot.numberOfPaintedTiles().ToString();
        }

        protected override string solvePartTwo()
        {
            paintRobot robot = new paintRobot(Input.toLongArray(","), paintColor.white);
            robot.run();
            return robot.printPaintGrid();
        }
    }
}
