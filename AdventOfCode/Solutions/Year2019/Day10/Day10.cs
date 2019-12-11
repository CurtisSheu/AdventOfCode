﻿using System;
using System.Collections.Generic;
using System.Numerics;

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

    class Asteroid
    {
        public int x { get; }
        public int y { get; }
        Dictionary<(int, int), List<Asteroid>> seenAsteroids;

        public Asteroid(int xCoord, int yCoord)
        {
            x = xCoord;
            y = yCoord;
            seenAsteroids = new Dictionary<(int, int), List<Asteroid>>();
        }

        public void checkAsteroidVisibility(Asteroid other)
        {
            int deltaX = other.x - x;
            int deltaY = y - other.y;

            int gcd = Utility.GCD(deltaX, deltaY);

            deltaX /= gcd;
            deltaY /= gcd;

            if (deltaX == 0) deltaY = deltaY / Math.Abs(deltaY);
            if (deltaY == 0) deltaX = deltaX / Math.Abs(deltaX);

            if (!seenAsteroids.ContainsKey((deltaX, deltaY)))
                seenAsteroids.Add((deltaX, deltaY), new List<Asteroid>());

            seenAsteroids[(deltaX, deltaY)].Add(other);
        }

        public Asteroid destroyNAsteroids(int n)
        {
            Vector2 laserVector = new Vector2(-.0001f, 1);
            Asteroid closestAsteroid = null;

            for (int i = 0; i < n; i++)
            {
                double closestAngle = double.MaxValue;
                (int x, int y) closestVector = (0, 0);

                foreach ((int x, int y) otherVector in seenAsteroids.Keys)
                {
                    double vectorAngle = laserVector.angleToVector(new Vector2(otherVector.x, otherVector.y));
                    if (vectorAngle < closestAngle && vectorAngle != 0)
                    {
                        closestAngle = vectorAngle;
                        closestVector = otherVector;
                    }
                }

                int closestDistance = int.MaxValue;
                foreach (Asteroid other in seenAsteroids[closestVector])
                {
                    int distance = Utility.calculateManhattanDistance((x, y), (other.x, other.y));
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestAsteroid = other;
                    }
                }

                seenAsteroids[closestVector].Remove(closestAsteroid);
                if (seenAsteroids[closestVector].Count == 0)
                    seenAsteroids.Remove(closestVector);

                laserVector = new Vector2(closestVector.x, closestVector.y);
            }

            return closestAsteroid;
        }

        public int countSeen()
        {
            return seenAsteroids.Count;
        }
    }
}
