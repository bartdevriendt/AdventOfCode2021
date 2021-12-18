using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day18Part2 : Day18Part1
    {
        public Day18Part2()
        {
            Day = 18;
            Part = 2;
        }

        protected override void ProcessResult(List<string> lines)
        {

            int magnitude = 0;
            for (int j = 0; j < lines.Count; j++)
            {
                for (int k = 0; k < lines.Count; k++)
                {
                    if (j != k)
                    {
                        Pair p = ProcessList(new List<string> { lines[j], lines[k] });
                        int mag = new PairTree(p.Number, null).Magnitude();
                        if (mag > magnitude) magnitude = mag;
                    }
                }
            }


            Console.WriteLine($"Largest magnitude: {magnitude}");
        }
    }
}
