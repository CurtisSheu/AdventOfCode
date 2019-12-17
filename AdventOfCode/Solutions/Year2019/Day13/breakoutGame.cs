using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class breakoutGame
    {
        IntcodeComputer<long> software;
        Dictionary<(long x, long y), screenElement> screen;
        private int screenWidth;
        private int screenHeight;
        private int score;
        private int blockCount;
        private (int x, int y) currentBallPosition;
        private (int x, int y) currentPaddlePosition;

        public breakoutGame(long[] input)
        {
            software = new IntcodeComputer<long>(input);
            screen = new Dictionary<(long x, long y), screenElement>();
            score = 0;
            screenWidth = 0;
            screenHeight = 0;
            blockCount = 0;
        }

        public int run(bool quarters = false)
        {
            if (quarters)
                software.initialize().memory[0] = 2;

            while (calculateScreen())
            {
                //drawScreen();
                inputPaddleDirection();
                //System.Threading.Thread.Sleep(100);
            }

            if (quarters)
                return score;
            return blockCount;
        }

        private void inputPaddleDirection()
        {
            if (currentBallPosition.x < currentPaddlePosition.x)
                software.inputSequence(-1);
            else if (currentBallPosition.x > currentPaddlePosition.x)
                software.inputSequence(1);
            else
                software.inputSequence(0);
        }

        private bool calculateScreen()
        {
            Queue<long> output = software.run().output;

            while (output.Count > 0)
            {
                (int x, int y) position = ((int)output.Dequeue(), (int)output.Dequeue());

                if (position.x != -1)
                {
                    screenElement element = Enum.Parse<screenElement>(output.Dequeue().ToString());
                    screen[position] = element;
                    if (position.x > screenWidth) screenWidth = position.x;
                    if (position.y > screenHeight) screenHeight = position.y;

                    if (element == screenElement.block)
                        blockCount++;
                    else if (element == screenElement.ball)
                        currentBallPosition = position;
                    else if (element == screenElement.paddle)
                        currentPaddlePosition = position;
                }
                else
                {
                    score = (int)output.Dequeue();
                }
            }

            return software.paused;
        }

        private void drawScreen()
        {
            Console.Clear();
            Console.WriteLine("Score: {0}", score.ToString());
            for (int y = 0; y <= screenHeight; y++)
            {
                for (int x = 0; x <= screenWidth; x++)
                {
                    char element = ' ';
                    switch (screen[(x, y)])
                    {
                        case screenElement.wall:
                            element = '+';
                            break;
                        case screenElement.block:
                            element = 'X';
                            break;
                        case screenElement.paddle:
                            element = '=';
                            break;
                        case screenElement.ball:
                            element = '0';
                            break;
                    }

                    Console.Write(element);
                }
                Console.Write("\n");
            }
        }

        enum screenElement { empty = 0, wall = 1, block = 2, paddle = 3, ball = 4 }
    }
}
