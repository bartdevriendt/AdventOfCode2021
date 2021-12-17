using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day17Part2 : Day17Part1
    {
        public Day17Part2()
        {
            Day = 17;
            Part = 2;
        }

        private int count=0;
        private List<Tuple<int, int>> bestShots = new List<Tuple<int, int>>();


        protected override void ProcessTrajectory(List<Tuple<int, int, int, int>> trajectory)
        {
            var start = trajectory[0];
            bestShots.Add(new Tuple<int, int>(start.Item3, start.Item4));
            

            
        }

        protected override void ProcessResult()
        {
            Console.WriteLine($"Number of good shots: {bestShots.Count}");
        }
    }
}
