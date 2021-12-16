using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace AdventOfCode2021.Excercises
{
    internal class Day15Part2 : Day15Part1
    {
        public Day15Part2()
        {
            Day = 15;
            Part = 2;
        }

        protected override Matrix<double> PrepareMatrix(Matrix<double> matrix)
        {
            Matrix<double> right = AddOne(matrix);
            for (int j = 0; j < 4; j++)
            {
                matrix = matrix.Append(right);
                
                right = AddOne(right);
            }

            Matrix<double> bottom = AddOne(matrix);
            for (int j = 0; j < 4; j++)
            {
                matrix = matrix.Stack(bottom);
                bottom = AddOne(bottom);
            }


            return matrix;
        }

        private Matrix<double> AddOne(Matrix<double> input)
        {
            Matrix<double> result = input.Add(1.0);

            result.MapInplace(x => x > 9 ? 1 : x);

            return result;
        }
    }
}
