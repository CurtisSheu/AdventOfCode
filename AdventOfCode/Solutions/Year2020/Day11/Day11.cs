using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    class Day11 : ASolution
    {
        SeatMap seatMap;
        public Day11() : base(11, 2020, "Seating System")
        {
        }

        protected override string solvePartOne()
        {
            seatMap = new SeatMap(Input);
            return seatMap.RunUntilStasis().ToString();
        }

        protected override string solvePartTwo()
        {
            seatMap = new SeatMap(Input);
            return seatMap.RunUntilStasis(true, 5).ToString();
        }

        class SeatMap
        {
            seat[,] allSeats;
            public SeatMap(string input)
            {
                string[] lines = input.splitByNewLine();
                allSeats = new seat[lines[0].Length, lines.Length];
                for (int y = 0; y < allSeats.GetLength(1); y++)
                {
                    for (int x = 0; x < allSeats.GetLength(0); x++)
                    {
                        allSeats[x, y] = lines[y][x] == 'L' ? seat.unoccupied : seat.floor;
                    }
                }
            }

            public int RunUntilStasis(bool lineOfSight = false, int visibleOccupied = 4)
            {
                bool changed = true;

                while (changed)
                {
                    changed = false;

                    seat[,] newAllSeats = (seat[,])allSeats.Clone();

                    for(int j = 0; j < allSeats.GetLength(1); j++)
                    {
                        for (int i = 0; i < allSeats.GetLength(0); i++)
                        {
                            newAllSeats[i, j] = checkSeat(i, j, lineOfSight, visibleOccupied);
                            if (newAllSeats[i, j] != allSeats[i, j])
                                changed = true;
                        }
                    }

                    allSeats = (seat[,])newAllSeats.Clone();
                }

                int count = 0;

                foreach (seat s in allSeats)
                    if (s == seat.occupied)
                        count++;

                return count;
            }

            private seat checkSeat(int x, int y, bool lineOfSight = false, int visibleOccuped = 4)
            {
                if (allSeats[x, y] == seat.floor)
                    return seat.floor;

                seat[] adjacentSeats;
                if (lineOfSight)
                    adjacentSeats = getLineOfSightSeats(x, y);
                else
                    adjacentSeats = allSeats.GetAdjacentElements(x, y);

                if (allSeats[x,y] == seat.unoccupied)
                {
                    foreach (seat adjacent in adjacentSeats)
                        if (adjacent == seat.occupied)
                            return seat.unoccupied;

                    return seat.occupied;
                }

                //else if seat is occupied
                int count = 0;
                foreach (seat adjacent in adjacentSeats)
                {
                    if (adjacent == seat.occupied)
                    {
                        count++;
                        if (count == visibleOccuped)
                            return seat.unoccupied;
                    }
                }

                return seat.occupied;
            }

            private seat[] getLineOfSightSeats(int x, int y)
            {
                List<seat> lineOfSightSeats = new List<seat>();

                for(int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if(i != 0 || j != 0)
                        {
                            lineOfSightSeats.Add(getSeatInVectorDirection(x, y, i, j));
                        }
                    }
                }

                return lineOfSightSeats.ToArray();
            }

            private seat getSeatInVectorDirection(int x, int y, int xChange, int yChange)
            {
                while (true)
                {
                    x += xChange;
                    y += yChange;

                    if (x < 0 || y < 0 || x >= allSeats.GetLength(0) || y >= allSeats.GetLength(1))
                        return seat.floor;

                    if (allSeats[x, y] != seat.floor)
                        return allSeats[x, y];
                }
            }

            enum seat{ floor, unoccupied, occupied }
        }
    }
}