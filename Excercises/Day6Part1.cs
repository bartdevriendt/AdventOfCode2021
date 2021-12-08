using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day6Part1 : ExcerciseBase
    {

        protected int _iterations = 80;

        public Day6Part1()
        {
            Day = 6;
            Part = 1;
        }

        public override void Run()
        {
            string startSituation = ReadFullFile("data\\day6.txt");

            List<int> internalTimers = startSituation.Split(",".ToCharArray()).Select(x => Convert.ToInt32(x)).ToList();

            List<int> distinctTimers = internalTimers.Distinct().ToList();


            Dictionary<int, double> fishCount = new Dictionary<int, double>();

            foreach (int i in distinctTimers)
            {
                fishCount[i] = CalculateFishCount(i, _iterations);
            }

            double totalFish = 0;

            foreach (int i in internalTimers)
            {
                totalFish += fishCount[i];
            }

            Console.WriteLine($"Total fish: {totalFish}");

        }

        private double CalculateFishCount(int startTimer, int iterations)
        {
            Dictionary<int, double> internalTimerState = GetStartDictionary();
            internalTimerState[startTimer] = 1;

            int j = 0;
            while (j < iterations)
            {

                Dictionary<int, double> newInternalTimerState = GetStartDictionary();

                for (int f = 0; f <= 8; f++)
                {
                    if (f == 0)
                    {
                        newInternalTimerState[6] += internalTimerState[f];
                        newInternalTimerState[8] += internalTimerState[f];
                    }
                    else
                    {
                        newInternalTimerState[f - 1] += internalTimerState[f];
                    }
                }

                internalTimerState = newInternalTimerState;
                j++;
            }

            return internalTimerState.Values.Sum();

        }

        private Dictionary<int, double> GetStartDictionary()
        {
            Dictionary<int, double> newInternalTimerState = new Dictionary<int, double>();
            for (int f = 0; f <= 8; f++)
            {
                newInternalTimerState[f] = 0;
            }

            return newInternalTimerState;
        }

    }
}
