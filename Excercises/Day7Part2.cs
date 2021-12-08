using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day7Part2 : Day7Part1
    {
        public Day7Part2()
        {
            Day = 7;
            Part = 2;
        }

        protected override int CalculateFuel(List<int> crabPositions, int targetPosition)
        {

            int totalFuel = 0;
            foreach (int crabPosition in crabPositions)
            {
                int n = Math.Abs(crabPosition - targetPosition);
                totalFuel += n * (n + 1) / 2;
            }

            return totalFuel;
        }
    }
}
