using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{

    

    internal class Day17Part1 : ExcerciseBase
    {

        private int AreaXMin = 175;
        private int AreaXMax = 227;
        private int AreaYMin = -134;
        private int AreaYMax = -79;

        //private int AreaXMin = 20;
        //private int AreaXMax = 30;
        //private int AreaYMin = -10;
        //private int AreaYMax = -5;

        private int HighestPoint = Int32.MinValue;
        private Tuple<int, int> bestInitialValue;

        public Day17Part1()
        {
            Day = 17;
            Part = 1;
        }

        public override void Run()
        {

            for (int x = 0; x <= AreaXMax; x++)
            {
                for (int y = AreaYMin; y < 1000; y++)
                {
                    var result = CalculateTrajectory(x, y);
                    if (result != null) // if we reach target area result is not null
                    {
                        ProcessTrajectory(result);
                    }
                }
            }

            ProcessResult();
            


        }


        protected virtual void ProcessResult()
        {
            Console.WriteLine($"Highest point reached: {HighestPoint}");
            Console.WriteLine($"Initial velocity: {bestInitialValue.Item1} - {bestInitialValue.Item2}");
        }

        protected virtual void ProcessTrajectory(List<Tuple<int, int, int, int>> trajectory)
        {
            int localHigh = Int32.MinValue;

            foreach (var t in trajectory)
            {
                if (t.Item2 > localHigh)
                {
                    localHigh = t.Item2;
                }
            }

            if (localHigh > HighestPoint)
            {
                HighestPoint = localHigh;

                var startFrom = trajectory[0];

                bestInitialValue = new Tuple<int, int>(startFrom.Item3, startFrom.Item4);

            }
        }


        private List<Tuple<int, int, int, int>> CalculateTrajectory(int deltaX, int deltaY)
        {


            var result = new List<Tuple<int, int, int, int>>();

            Tuple<int, int, int, int> from = new Tuple<int, int, int, int>(0, 0, deltaX, deltaY);

            result.Add(from);

            while (!IsInArea(from.Item1, from.Item2) && !Overshot(from.Item1, from.Item2))
            {
                from = CalculateNextStep(from);
                result.Add(from);
            }

            if (IsInArea(from.Item1, from.Item2))
            {
                return result;
            }

            return null;
        }

        private bool Overshot(int x, int y)
        {
            return x > AreaXMax || y < AreaYMin;
        }

        private bool IsInArea(int x, int y)
        {
            if (x >= AreaXMin && x <= AreaXMax && y >= AreaYMin && y <= AreaYMax) return true;
            return false;
        }


        private Tuple<int, int, int, int> CalculateNextStep(Tuple<int, int, int, int> from)
        {
            int newX = from.Item1 + from.Item3;
            int newY = from.Item2 + from.Item4;

            int newDeltaX = 0;

            if (from.Item3 > 0)
            {
                newDeltaX = from.Item3 - 1;
            }

            if (from.Item3 < 0)
            {
                newDeltaX = from.Item3 + 1;
            }

            int newDeltaY = from.Item4 - 1;

            return new Tuple<int, int, int, int>(newX, newY, newDeltaX, newDeltaY);
        }
    }
}
