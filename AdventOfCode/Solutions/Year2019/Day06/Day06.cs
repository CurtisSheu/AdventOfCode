using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day06 : ASolution
    {
        public Day06() : base(6, 2019, "Universal Orbit Map") { }

        protected override string solvePartOne()
        {
            string[] orbitList = Input.splitByNewLine();
            List<Planet> innerPlanets = new List<Planet>();
            foreach(string orbit in orbitList)
            {
                string[] planetList = orbit.Split(')');

                Planet newInnerPlanet = null;
                Planet newOuterPlanet = null;
                foreach (Planet planet in innerPlanets)
                {
                    if (newInnerPlanet == null)
                        newInnerPlanet = planet.getPlanetByName(planetList[0]);
                    if (newOuterPlanet == null)
                        newOuterPlanet = planet.getPlanetByName(planetList[1]);
                }
                if (newInnerPlanet == null)
                {
                    newInnerPlanet = new Planet(planetList[0]);
                    innerPlanets.Add(newInnerPlanet);
                }
                if (newOuterPlanet == null)
                    newOuterPlanet = new Planet(planetList[1]);
                else if (innerPlanets.Contains(newOuterPlanet))
                    innerPlanets.Remove(newOuterPlanet);

                newInnerPlanet.addDirectOrbit(newOuterPlanet);
            }
            return innerPlanets[0].orbitCount(0).ToString();
        }

        protected override string solvePartTwo()
        {
            return "";
        }
    }

    class Planet
    {
        private List<Planet> directOrbits;
        public string name { get; }

        public Planet(string n)
        {
            name = n;
            directOrbits = new List<Planet>();
        }

        public int orbitCount(int level)
        {
            int count = 0;
            foreach (Planet p in directOrbits)
                count += p.orbitCount(level + 1);

            return count + level;
        }

        public void addDirectOrbit(Planet p)
        {
            directOrbits.Add(p);
        }

        public Planet getPlanetByName(string planetToFind)
        {
            if (name == planetToFind)
                return this;

            foreach(Planet p in directOrbits)
            {
                var result = p.getPlanetByName(planetToFind);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}
