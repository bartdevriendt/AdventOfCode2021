using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCode2021.Excercises
{

    internal class Day22Part2 : ExcerciseBase
    {

        private List<(bool b, (int lo, int hi) x, (int lo, int hi) y, (int lo, int hi) z)> boxes =
            new List<(bool b, (int lo, int hi) x, (int lo, int hi) y, (int lo, int hi) z)>();

        public Day22Part2()
        {
            Day = 22;
            Part = 2;
        }

        

        public override void Run()
        {


            ReadFileLineByLine("data\\day22.txt", line =>
            {
                ProcessLine(line);
            });

            var result = boxes.Sum(b => BoxSize(b.x, b.y, b.z) * (b.b ? 1 : -1));

            Console.WriteLine($"Number of boxes turned on: {result}");
        }

        private void ProcessLine(string line)
        {
            bool turnOn = line.StartsWith("on");
            if (turnOn) line = line.Substring(3);
            else line = line.Substring(4);

            string[] parts = line.Split(',');
            string[] xRange = parts[0].Substring(2).Split('.', StringSplitOptions.RemoveEmptyEntries);
            string[] yRange = parts[1].Substring(2).Split('.', StringSplitOptions.RemoveEmptyEntries);
            string[] zRange = parts[2].Substring(2).Split('.', StringSplitOptions.RemoveEmptyEntries);


            int xFrom = Convert.ToInt32(xRange[0]);
            int xTo = Convert.ToInt32(xRange[1]);
            int yFrom = Convert.ToInt32(yRange[0]);
            int yTo = Convert.ToInt32(yRange[1]);
            int zFrom = Convert.ToInt32(zRange[0]);
            int zTo = Convert.ToInt32(zRange[1]);

            (bool b, (int lo, int hi) x, (int lo, int hi) y, (int lo, int hi) z) newBox = (turnOn, (xFrom, xTo),
                (yFrom, yTo), (zFrom, zTo));
            

            boxes.AddRange(
                boxes.Select(sub => Overlap(newBox, sub))
                    .Where(o => o.x.lo <= o.x.hi
                                && o.y.lo <= o.y.hi
                                && o.z.lo <= o.z.hi)
                    .ToList()
                );

            if (turnOn)
            {
                boxes.Add(newBox);
            }

        }

        private static long BoxSize(
            (int lo, int hi) x,
            (int lo, int hi) y,
            (int lo, int hi) z) =>
            (x.hi - x.lo + 1L)
            * (y.hi - y.lo + 1)
            * (z.hi - z.lo + 1);

        private static (bool b, (int lo, int hi) x, (int lo, int hi) y, (int lo, int hi) z) Overlap(
            (bool b, (int lo, int hi) x, (int lo, int hi) y, (int lo, int hi) z) b1,
            (bool b, (int lo, int hi) x, (int lo, int hi) y, (int lo, int hi) z) b2) =>
        (
            !b2.b,
            (lo: Math.Max(b1.x.lo, b2.x.lo), hi: Math.Min(b1.x.hi, b2.x.hi)),
            (lo: Math.Max(b1.y.lo, b2.y.lo), hi: Math.Min(b1.y.hi, b2.y.hi)),
            (lo: Math.Max(b1.z.lo, b2.z.lo), hi: Math.Min(b1.z.hi, b2.z.hi)));
    }
}
