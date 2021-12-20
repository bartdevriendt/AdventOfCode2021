using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace AdventOfCode2021.Excercises
{
    internal class Day20Part1 : ExcerciseBase
    {
        public Day20Part1()
        {
            Day = 20;
            Part = 1;
        }

        protected int NumSteps { get; set; } = 2;

        private int _currentStep = 1;
        private int _outsideBit = 0;
        public override void Run()
        {

            string algo = ReadFullFile("data\\day20_algorithm.txt").Trim();
            Matrix<double> input = ReadMatrix("data\\day20_input.txt");

            _currentStep = 1;

            while (_currentStep <= NumSteps)
            {
                Console.WriteLine($"Enhancing image step {_currentStep}");
                input = EnhanceImage(input, algo);
                _currentStep++;
                if (algo[0] == '#')
                {
                    _outsideBit = _outsideBit == 0 ? 1 : 0;
                }
            }


            int result = input.Enumerate().Where(x => x >= 1.0).Count();
            
            Console.WriteLine($"Number of lit bits: {result}");

        }
        private Matrix<double> EnhanceImage(Matrix<double> input, string algo)
        {

            Matrix<double> result = Matrix<double>.Build.Dense(input.RowCount + 2, input.ColumnCount + 2 , 0);
            
            for (int r = -1; r < input.RowCount + 1; r++)
            {
                for (int c = -1; c < input.ColumnCount + 1; c++)
                {
                    int binary = GetBinaryNumber(input, r, c);
                    result[r + 1, c + 1] = GetEnhancedValue(binary, algo);
                }
            }

            return result;

        }

        private double GetEnhancedValue(int binary, string algo)
        {
            if (algo[binary] == '.') return 0.0;
            else return 1.0;
        }

        private int GetBinaryNumber(Matrix<double> input, int r, int c)
        {
            string binary = "";
            for (int i = r - 1; i <= r + 1; i++)
            {
                for (int j = c - 1; j <= c + 1; j++)
                {
                    double val = GetValueAt(input, i, j);
                    binary += (val == 0.0 ? "0" : "1");
                }
            }

            return Convert.ToInt32(binary, 2);
        }

        private double GetValueAt(Matrix<double> input, int r, int c)
        {
            if (r < 0 || r >= input.RowCount || c < 0 || c >= input.ColumnCount)
            {
                return _outsideBit;
            }
            else
            {
                return input[r, c];
            }
        }
    }
}
