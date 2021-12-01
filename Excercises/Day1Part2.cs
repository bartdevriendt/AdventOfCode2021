using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Excercises
{
    public class Day1Part2 : ExcerciseBase
    {
        public Day1Part2()
        {
            Day = 1;
            Part = 2;
        }

        public override void Run()
        {

            List<int> values = new();
            
            int previousSum = 0;
            int increments = 0;
            ReadFileLineByLine("data\\day1.txt", line =>
            {
                
                values.Add(Convert.ToInt32(line));

                if (values.Count == 3)
                {
                    previousSum = values.Sum();
                }
                else if (values.Count == 4)
                {

                    values.RemoveAt(0);
                    
                    if (previousSum < values.Sum())
                    {
                        increments++;
                    }
                }

                previousSum = values.Sum();
            });

            Console.WriteLine($"There are {increments} increments");
        }
    }
}