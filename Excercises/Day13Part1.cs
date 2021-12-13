using System.Security.Cryptography;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AdventOfCode2021.Excercises;

public class Day13Part1 : ExcerciseBase
{
    public Day13Part1()
    {
        Day = 13;
        Part = 1;
    }

    protected List<string> foldActions = new List<string>();
    
    public override void Run()
    {
        Matrix<double> input = ReadInput();
        
        //PrintMatrix(input);

        RunAnalysis(input);
        
    }

    protected virtual void RunAnalysis(Matrix<double> input)
    {
        var result = FoldAlong(input, foldActions[0]);
        //result = FoldAlong(result, foldActions[1]);
        //PrintMatrix(result);

        var dots = result.Enumerate().Count(x => x > 0);
        Console.WriteLine($"Number of dots = {dots}");
    }
    private Matrix<double> ReadInput()
    {
        Matrix<double> input = Matrix<double>.Build.Dense(1, 1, 0);

        bool readDots = true;

        ReadFileLineByLine("data\\day13.txt", line =>
        {
            if (line.Trim() == "")
            {
                readDots = false;
            }
            else
            {
                if (readDots)
                {
                    string[] parts = line.Split(',');
                    int x = Convert.ToInt32(parts[0]);
                    int y = Convert.ToInt32(parts[1]);
                    if (x + 1 > input.ColumnCount)
                    {
                        input = input.Resize(input.RowCount, x + 1);
                    }

                    if (y + 1 > input.RowCount)
                    {
                        input = input.Resize(y + 1, input.ColumnCount);
                    }

                    input[y, x] = 1;
                }
                else
                {
                    foldActions.Add(line.Substring(11));
                }
            }
        });

        return input;
    }

    protected Matrix<double> FoldAlong(Matrix<double> input, string foldLine)
    {
        
        
        if (foldLine.StartsWith("x="))
        {
            int col = Convert.ToInt32(foldLine.Substring(2));
            int leftCols = col;
            int rightCols = input.ColumnCount - col - 1;
            int newSize = leftCols > rightCols ? leftCols : rightCols;
            Matrix<double> result =
                Matrix<double>.Build.Dense(input.RowCount, newSize);

            for (int r = 0; r < input.RowCount; r++)
            {
                int countLeft = col - 1;
                int countRight = col + 1;
            
                for (int c = newSize - 1; c >= 0; c--)
                {
                    double valLeft = countLeft >= 0 ? input[r, countLeft] : 0;
                    double valRight = countRight < input.ColumnCount ? input[r, countRight] : 0;
                    result[r, c] = valLeft + valRight > 1 ? 1 : valLeft + valRight;
                    countLeft--;
                    countRight++;
                }    
            }

            return result;


        }
        else
        {
            int row = Convert.ToInt32(foldLine.Substring(2));
            int upRows = row;
            int downRows = input.RowCount - row - 1;
            int newSize = upRows > downRows ? upRows : downRows;
            Matrix<double> result =
                Matrix<double>.Build.Dense(newSize, input.ColumnCount);

            for (int c = 0; c < input.ColumnCount; c++)
            {
                int countUp = row - 1;
                int countDown = row + 1;
            
                for (int r = newSize - 1; r >= 0; r--)
                {
                    double valUp = countUp >= 0 ? input[countUp, c] : 0;
                    double valDown = countDown < input.RowCount ? input[countDown, c] : 0;
                    result[r, c] = valUp + valDown > 1 ? 1 : valUp + valDown;
                    countUp--;
                    countDown++;
                }    
            }

            return result;
        }
    }
}