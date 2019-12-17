using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2019
{
    class Day10 : ASolution
    {
        List<Asteroid> asteroids;
        Asteroid mostVisible;
        public Day10() : base(10, 2019, "Monitoring Station")
        {
            asteroids = new List<Asteroid>();
            string[] rows = Input.splitByNewLine();

            int y = 0;
            foreach (string row in rows)
            {
                int x = 0;
                foreach (char c in row)
                {
                    if (c == '#')
                        asteroids.Add(new Asteroid(x, y));
                    x++;
                }
                y++;
            }

            foreach (Asteroid asteroid in asteroids)
            {
                foreach (Asteroid other in asteroids)
                {
                    if (asteroid != other)
                        asteroid.checkAsteroidVisibility(other);
                }
            }

            int maxVisibility = 0;
            foreach (Asteroid asteroid in asteroids)
            {
                if (asteroid.countSeen() > maxVisibility)
                {
                    maxVisibility = asteroid.countSeen();
                    mostVisible = asteroid;
                }
            }
        }

        protected override string solvePartOne()
        {
            return mostVisible.countSeen().ToString();
        }

        protected override string solvePartTwo()
        {
            Asteroid lastDestroyed = mostVisible.destroyNAsteroids(200);
            return (lastDestroyed.x * 100 + lastDestroyed.y).ToString();
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }
    }
}
