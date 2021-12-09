using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AdventOfCode2021.Excercises;

public class Day9Part1 : ExcerciseBase
{
    public Day9Part1()
    {
        Day = 9;
        Part = 1;
    }

    public override void Run()
    {
        string input = ReadFullFile("data\\day9.txt");
        string[] lines = input.Split("\r\n");
        int rows = lines.Length;
        int cols = lines[0].Trim().Length;

        Matrix<double> m = Matrix<double>.Build.Sparse(rows, cols, -1);


        for (int j = 0; j < rows; j++)
        {
            for (int k = 0; k < cols; k++)
            {
                m[j, k] = Convert.ToInt32(lines[j][k].ToString());
            }
        }

        int lowPoints = FindLowPoints(m);
        
        Console.WriteLine($"Total number of low points: {lowPoints}");
    }

    private int FindLowPoints(Matrix<double> matrix)
    {
        int result = 0;
        for (int j = 0; j < matrix.RowCount; j++)
        {
            for (int k = 0; k < matrix.ColumnCount; k++)
            {
                bool isLowPoint = true;

                if (j > 0)
                {
                    if (matrix[j - 1, k] <= matrix[j, k])
                    {
                        isLowPoint = false;
                    }
                }

                if (j < matrix.RowCount - 1)
                {
                    if (matrix[j + 1, k] <= matrix[j, k])
                    {
                        isLowPoint = false;
                    }
                }

                if (k > 0)
                {
                    if (matrix[j, k-1] <= matrix[j, k])
                    {
                        isLowPoint = false;
                    }
                }
                
                if (k < matrix.ColumnCount - 1)
                {
                    if (matrix[j, k+1] <= matrix[j, k])
                    {
                        isLowPoint = false;
                    }
                }

                if (isLowPoint)
                {
                    result+=(1+Convert.ToInt32(matrix[j,k]));
                }
            }
        }

        return result;
    }
}