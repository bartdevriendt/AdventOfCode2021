using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AdventOfCode2021.Excercises;

public class Day25Part1 : ExcerciseBase
{
    public Day25Part1()
    {
        Day = 25;
        Part = 1;

    }

    public override void Run()
    {
        var matrix = ReadMatrix("data\\day25.txt");

        int j = 0;

        bool rightMoved = true;
        bool downMoved = true;

        Dictionary<double, string> mapping = new Dictionary<double, string>();
        mapping.Add(0.0, ".");
        mapping.Add(1.0, ">");
        mapping.Add(2.0, "v");

        while (rightMoved || downMoved)
        {
            j++;
            (matrix, rightMoved) = MoveRight(matrix);
            (matrix, downMoved) = MoveDown(matrix);
            
            Console.WriteLine($"After step {j}  ");
            //PrintMatrix(matrix, mapping);
        }
        
        Console.WriteLine($"Did {j} steps");
    }

    private (Matrix<double>, bool) MoveRight(Matrix<double> input)
    {

        List<(int, int)> toMove = new List<(int, int)>();
        
        for (int r = 0; r < input.RowCount; r++)
        {
            for (int c = 0; c < input.ColumnCount; c++)
            {
                if (Convert.ToInt32(input[r, c]) == 1 && IsFreeSpot(input, c + 1, r))
                {
                    toMove.Add((r, c));
                }
            }
        }

        foreach (var move in toMove)
        {
            var right = (move.Item1, move.Item2 + 1);
            if (right.Item2 == input.ColumnCount)
            {
                right.Item2 = 0;
            }

            input[right.Item1, right.Item2] = 1;
            input[move.Item1, move.Item2] = 0;
        }


        return (input, toMove.Count > 0);
    }
    
    private (Matrix<double>, bool) MoveDown(Matrix<double> input)
    {
        List<(int, int)> toMove = new List<(int, int)>();
        
        for (int r = 0; r < input.RowCount; r++)
        {
            for (int c = 0; c < input.ColumnCount; c++)
            {
                if (Convert.ToInt32(input[r, c]) == 2 && IsFreeSpot(input, c, r + 1))
                {
                    toMove.Add((r, c));
                }
            }
        }

        foreach (var move in toMove)
        {
            var down = (move.Item1 + 1, move.Item2);
            if (down.Item1 == input.RowCount)
            {
                down.Item1 = 0;
            }

            input[down.Item1, down.Item2] = 2;
            input[move.Item1, move.Item2] = 0;
        }


        return (input, toMove.Count > 0);
    }

    private bool IsFreeSpot(Matrix<double> input, int x, int y)
    {
        if (x == input.ColumnCount) x = 0;
        if (y == input.RowCount) y = 0;

        return input[y, x] == 0.0;
    }
}