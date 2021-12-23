using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{

    public class Cube
    {
        public int XMin { get; set; }
        public int XMax { get; set; }
        public int YMin { get; set; }


    }

    internal class Day22Part2 : ExcerciseBase
    {
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

            //Console.WriteLine($"Number of cubes turned on: {cubes.Count}");
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

            //if (xFrom < xMin) xFrom = xMin;
            //if (xTo > xMax) xTo = xMax;
            //if (yFrom < yMin) yFrom = yMin;
            //if (yTo > yMax) yTo = yMax;
            //if (zFrom < zMin) zFrom = zMax;
            //if (zTo > zMax) zTo = zMin;

            
        }
    }
}
