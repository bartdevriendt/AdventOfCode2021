using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AdventOfCode2021.Excercises;

public class Day11Part1 : ExcerciseBase
{
    public Day11Part1()
    {
        Day = 11;
        Part = 1;
    }

    public override void Run()
    {
        var matrix = ReadMatrix("data\\day11.txt");

        int flashes = 0;
        for (int j = 1; j <= 1000; j++)
        {
            flashes += CalculateStep(matrix);

            // if ((j % 10) == 0)
            // {
            //     Console.WriteLine($"After step {j} there have been {flashes} flashes");
            //     PrintMatrix(matrix);
            // }
            
            
            //PrintMatrix(matrix);
            
            
            
            AfterStep(matrix, j);
            
        }
        
        Console.WriteLine($"Total flashes: {flashes}");
    }


    protected virtual void AfterStep(Matrix<double> matrix, int step)
    {
        
    }
    
    protected int CalculateStep(Matrix<double> matrix)
    {

        for (int row = 0; row < matrix.RowCount; row++)
        {
            for (int col = 0; col < matrix.ColumnCount; col++)
            {
                matrix[row, col] += 1;
            }
        }

        Matrix<double> flashed = Matrix<double>.Build.Dense(matrix.RowCount, matrix.ColumnCount, 0);
        while (true)
        {
            bool hasFlashed = false;
            for (int row = 0; row < matrix.RowCount; row++)
            {
                for (int col = 0; col < matrix.ColumnCount; col++)
                {
                    double energy = matrix[row, col];
                    if (energy > 9 && flashed[row, col] == 0)
                    {
                        IncreaseEnergyAround(row, col, matrix);
                        flashed[row, col] = 1;
                        hasFlashed = true;
                    }
                }
            }

            if (!hasFlashed) break;
        }
        
        
        
            
        int flashes = matrix.Enumerate().Where(x => x > 9).Count();

        for (int row = 0; row < matrix.RowCount; row++)
        {
            for (int col = 0; col < matrix.ColumnCount; col++)
            {
                double energy = matrix[row, col];
                if(energy > 9)
                    matrix[row, col] = 0;
            }
        }

        
        return flashes;
    }

    private void IncreaseEnergyAround(int row, int col, Matrix<double> matrix)
    {
        for (int offsetx = -1; offsetx <= 1; offsetx++)
        {
            for (int offsety = -1; offsety <= 1; offsety++)
            {
                IncreaseEnergy(row + offsety, col + offsetx, matrix);
            }    
        }
    }

    private void IncreaseEnergy(int row, int col, Matrix<double> matrix)
    {
        if (row >= matrix.RowCount || row < 0) return;
        if (col >= matrix.ColumnCount || col < 0) return;

        matrix[row, col] += 1;
    }
}