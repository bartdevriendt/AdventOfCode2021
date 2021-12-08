using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace AdventOfCode2021.Excercises
{
    internal class Day7Part1 : ExcerciseBase
    {
        public Day7Part1()
        {
            Day = 7;
            Part = 1;
        }

        public override void Run()
        {
            string startSituation = ReadFullFile("data\\day7.txt");

            List<int> crabPositions = startSituation.Split(",".ToCharArray()).Select(x => Convert.ToInt32(x)).ToList();
            int maxPosition = crabPositions.Max();
            int minPosition = crabPositions.Min();

            //Dictionary<int, int> positionFuel = new Dictionary<int, int>();

            int cheapest = minPosition;
            int lowestFuel = Int32.MaxValue;
            

            for (int j = minPosition; j <= maxPosition; j++)
            {
                int positionFuel = CalculateFuel(crabPositions, j);
                if (positionFuel < lowestFuel)
                {
                    cheapest = j;
                    lowestFuel = positionFuel;
                }
            }


            Console.WriteLine($"Cheapest position is {cheapest} with {lowestFuel} fuel");

        }

        protected virtual int CalculateFuel(List<int> crabPositions, int targetPosition)
        {
            int totalFuel = 0;
            foreach (int crabPosition in crabPositions)
            {
                totalFuel += Math.Abs(crabPosition - targetPosition);
            }

            return totalFuel;

        }
    }
}
