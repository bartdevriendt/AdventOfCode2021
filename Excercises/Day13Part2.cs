using MathNet.Numerics.LinearAlgebra;

namespace AdventOfCode2021.Excercises;

public class Day13Part2 : Day13Part1
{
    public Day13Part2()
    {
        Day = 13;
        Part = 2;
    }

    protected override void RunAnalysis(Matrix<double> input)
    {
        
        foreach (string fold in foldActions)
        {
            input = FoldAlong(input, fold);
        }
        
        PrintMatrix(input);
        
    }
}