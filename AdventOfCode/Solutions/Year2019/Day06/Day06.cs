using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day06 : ASolution
    {
        Planet innerMostPlanet;

        public Day06() : base(6, 2019, "Universal Orbit Map") 
        {
            string[] orbitList = Input.splitByNewLine();
            List<Planet> innerPlanets = new List<Planet>();
            foreach (string orbit in orbitList)
            {
                string[] planetList = orbit.Split(')');

                Planet newInnerPlanet = null;
                Planet newOuterPlanet = null;
                foreach (Planet planet in innerPlanets)
                {
                    if (newInnerPlanet == null)
                        newInnerPlanet = planet.getPlanetByName(planetList[0]).p;
                    if (newOuterPlanet == null)
                        newOuterPlanet = planet.getPlanetByName(planetList[1]).p;
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

            innerMostPlanet = innerPlanets[0];
        }

        protected override string solvePartOne()
        {
            return innerMostPlanet.orbitCount(0).ToString();
        }

        protected override string solvePartTwo()
        {
            return innerMostPlanet.getPlanetByName("YOU").p.getDistanceToOtherPlanet("SAN", 0).ToString(); ;
        }
    }

    class Planet
    {
        private List<Planet> directOrbits;
        public string name { get; }
        private Planet parentPlanet { get; set; }

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
            p.parentPlanet = this;
        }

        public (int distance, Planet p) getPlanetByName(string planetToFind)
        {
            if (name == planetToFind)
                return (0, this);

            foreach(Planet p in directOrbits)
            {
                var result = p.getPlanetByName(planetToFind);
                if (result.p != null)
                    return (1 + result.distance, result.p);
            }
            return (-1, null);
        }

        public int getDistanceToOtherPlanet(string destinationPlanetName, int distanceUp)
        {
            var result = getPlanetByName(destinationPlanetName);

            if (result.p != null)
                return result.distance + distanceUp - 2;

            return parentPlanet.getDistanceToOtherPlanet(destinationPlanetName, distanceUp + 1);
        }
    }
}
