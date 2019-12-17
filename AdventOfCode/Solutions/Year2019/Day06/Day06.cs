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
}
