using MathNet.Numerics.LinearAlgebra;

namespace AdventOfCode2021.Excercises;

public class Day11Part2 : Day11Part1
{
    public Day11Part2()
    {
        Day = 11;
        Part = 2;
    }

    protected override void AfterStep(Matrix<double> matrix, int step)
    {
        int count = matrix.Enumerate().Where(x => x > 0).Count();
        if (count == 0)
        {
            Console.WriteLine($"At step {step} all octopusses are flashing");
        }
    }
}