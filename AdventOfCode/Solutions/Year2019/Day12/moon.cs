using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
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
}
