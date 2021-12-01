using System;

namespace AdventOfCode2021.Excercises
{
    public class Day1Part1 : ExcerciseBase
    {
        public Day1Part1()
        {
            Day = 1;
            Part = 1;
        }

        public override void Run()
        {

            string previousLine = null;
            int increments = 0;
            ReadFileLineByLine("data\\day1.txt", line =>
            {

                if (previousLine != null)
                {
                    if (Convert.ToInt32(previousLine) < Convert.ToInt32(line))
                    {
                        increments++;
                    }
                }

                previousLine = line;
            });

            Console.WriteLine($"There are {increments} increments");

        }
    }
}