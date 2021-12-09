using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AdventOfCode2021.Excercises;

public class Day9Part2 : ExcerciseBase
{
    public Day9Part2()
    {
        Day = 9;
        Part = 2;
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

        Matrix<double> result = FindBasins(m);

        int y = 1;

        List<int> sizes = new List<int>();
        
        while (true)
        {
            int size = result.Enumerate().Count(x => Convert.ToInt32(x) == y);

            if (size == 0)
            {
                break;
            }
            else
            {
                sizes.Add(size);
            }

            y++;
        }

        sizes.Sort();
        sizes.Reverse();

        int mult = sizes[0] * sizes[1] * sizes[2];

        Console.WriteLine($"Result={mult}");
    }
    
    private Matrix<double> FindBasins(Matrix<double> matrix)
    {

        Matrix<double> result = Matrix<double>.Build.Sparse(matrix.RowCount, matrix.ColumnCount, -1);

        int basinCount = 1;
        
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
                    result = MarkBasinFromLowPoint(matrix, result, j, k, basinCount);
                    basinCount++;
                }
            }
        }

        return result;
    }

    private Matrix<double> MarkBasinFromLowPoint(Matrix<double> input, Matrix<double> basins, int j, int k, int basinCount)
    {

        Stack<Tuple<int, int>> coordinates = new Stack<Tuple<int, int>>();
        
        coordinates.Push(new Tuple<int, int>(j, k));

        while (coordinates.Count > 0)
        {
            Tuple<int, int> coordinate = coordinates.Pop();
            j = coordinate.Item1;
            k = coordinate.Item2;
            if (Convert.ToInt32(basins[j, k]) == -1)
            {
                basins[j, k] = basinCount;
            }

            if (j > 0)
            {
                if (Convert.ToInt32(basins[j - 1, k]) == -1)
                {
                    if (Convert.ToInt32(input[j - 1, k]) == 9)
                    {
                        basins[j - 1, k] = 0;
                    }
                    else
                    {
                        coordinates.Push(new Tuple<int, int>(j - 1, k));
                    }
                }
            }

            if (j < input.RowCount - 1)
            {
                if (Convert.ToInt32(basins[j + 1, k]) == -1)
                {
                    if (Convert.ToInt32(input[j + 1, k]) == 9)
                    {
                        basins[j + 1, k] = 0;
                    }
                    else
                    {
                        coordinates.Push(new Tuple<int, int>(j + 1, k));
                    }
                }
            }
            
            if (k > 0)
            {
                if (Convert.ToInt32(basins[j, k - 1]) == -1)
                {
                    if (Convert.ToInt32(input[j, k - 1]) == 9)
                    {
                        basins[j, k - 1] = 0;
                    }
                    else
                    {
                        coordinates.Push(new Tuple<int, int>(j, k - 1));
                    }
                }
            }
            
            if (k < input.ColumnCount - 1)
            {
                if (Convert.ToInt32(basins[j, k + 1]) == -1)
                {
                    if (Convert.ToInt32(input[j, k + 1]) == 9)
                    {
                        basins[j, k + 1] = 0;
                    }
                    else
                    {
                        coordinates.Push(new Tuple<int, int>(j, k + 1));
                    }
                }
            }
        }
        
        return basins;
    }
}