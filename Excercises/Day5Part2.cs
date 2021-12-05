using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace AdventOfCode2021.Excercises
{
    internal class Day5Part2 : ExcerciseBase
    {
        public Day5Part2()
        {
            Day = 5;
            Part = 2;
        }

        public override void Run()
        {
            Matrix<double> matrix = Matrix<double>.Build.Dense(1, 1, 0);

            ReadFileLineByLine("data\\day5.txt", line =>
            {
                string origin = line.Substring(0, line.IndexOf(" "));
                string target = line.Substring(line.IndexOf("->") + 3);
                Console.WriteLine($"Origin: {origin} - Target: {target}");

                string[] x1y1 = origin.Split(",".ToCharArray());
                string[] x2y2 = target.Split(",".ToCharArray());

                int x1 = Convert.ToInt32(x1y1[0]);
                int y1 = Convert.ToInt32(x1y1[1]);
                int x2 = Convert.ToInt32(x2y2[0]);
                int y2 = Convert.ToInt32(x2y2[1]);
                
                if (x1 >= matrix.ColumnCount) matrix = matrix.Resize(matrix.RowCount, x1 + 1);
                if (x2 >= matrix.ColumnCount) matrix = matrix.Resize(matrix.RowCount, x2 + 1);
                if (y1 >= matrix.RowCount) matrix = matrix.Resize(y1 + 1, matrix.ColumnCount);
                if (y2 >= matrix.RowCount) matrix = matrix.Resize(y2 + 1, matrix.ColumnCount);

                if (x1 == x2)
                {
                    if (y1 > y2)
                    {
                        (y2, y1) = (y1, y2);
                    }
                    for (int y = y1; y <= y2; y++)
                    {
                        matrix[y, x1]++;
                    }
                }
                else if (y1 == y2)
                {
                    if (x1 > x2)
                    {
                        (x2, x1) = (x1, x2);
                    }
                    for (int x = x1; x <= x2; x++)
                    {
                        matrix[y1, x]++;
                    }

                }
                else
                {
                    int x = x1;
                    int y = y1;
                    int xinc = 1;
                    int yinc = 1;
                    if (x2 < x1) xinc = -1;
                    if (y2 < y1) yinc = -1;

                    while(x != x2) 
                    {

                        matrix[y, x]++;
                        x+=xinc;
                        y+=yinc;
                    }
                    matrix[y, x]++;
                }
                
            });

            PrintMatrix(matrix);


            int overlap = matrix.Enumerate().Where(x => x > 1).Count();
            Console.WriteLine($"Number of overlaps: {overlap}");
        }
    }
}
