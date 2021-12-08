using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day8Part1 : ExcerciseBase
    {
        public Day8Part1()
        {
            Day = 8;
            Part = 1;
        }

        public override void Run()
        {

            int appearances = 0;

            ReadFileLineByLine("data\\day8.txt", line =>
            {

                string[] input = line.Split('|');
                string[] digits = input[1].Split(' ');

                foreach (string digit in digits)
                {
                    if (digit.Length == 2 || digit.Length == 4 || digit.Length == 3 || digit.Length == 7)
                    {
                        appearances++;
                    }
                }

            });

            Console.WriteLine($"Number of appearances of 1, 4, 7 or 8: {appearances}");
        }
    }
}
