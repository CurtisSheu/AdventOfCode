using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
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

            foreach (Planet p in directOrbits)
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
