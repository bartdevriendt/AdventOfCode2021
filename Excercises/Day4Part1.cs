using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace AdventOfCode2021.Excercises
{
    internal class Day4Part1 : ExcerciseBase
    {


        public Day4Part1()
        {
            Day = 4;
            Part = 1;
        }




        public override void Run()
        {

            string randomNumbers = null;

            List<Tuple<Matrix<double>, Matrix<double>>> boards = new List<Tuple<Matrix<double>, Matrix<double>>>();

            Matrix<double> currentBoard = null;

            int lineCount = 0;

            ReadFileLineByLine("data\\day4.txt", line =>
            {
                if (randomNumbers is null)
                {
                    randomNumbers = line;
                }
                else
                {
                    if (line.Trim() == "")
                    {
                        var board = new Tuple<Matrix<double>, Matrix<double>>(Matrix<double>.Build.Dense(5, 5, -1),
                            Matrix<double>.Build.Dense(5, 5, 0));
                        boards.Add(board);
                        currentBoard = board.Item1;
                        lineCount = 0;
                    }
                    else
                    {
                        string[] parts = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        for (int j = 0; j < parts.Length; j++)
                        {
                            currentBoard[lineCount,j] = Convert.ToInt32(parts[j].Trim());
                        }

                        lineCount++;

                    }
                }
            });

            List<int> numbers = randomNumbers.Split(",".ToCharArray()).Select(x => Convert.ToInt32(x)).ToList();

            var result = PlayBingoAndReturnWinningBoard(numbers, boards);

            if (result != null)
            {

                int sum = 0;

                for (int row = 0; row < result.Item1.RowCount; row++)
                {
                    for (int col = 0; col < result.Item1.ColumnCount; col++)
                    {
                        if (result.Item2[row, col] == 0)
                        {
                            sum += Convert.ToInt32(result.Item1[row, col]);
                        }
                    }
                }

                Console.WriteLine($"Result : {sum * result.Item3}");
            }

        }

        private Tuple<Matrix<double>, Matrix<double>, int> PlayBingoAndReturnWinningBoard(List<int> numbers, List<Tuple<Matrix<double>, Matrix<double>>> boards)
        {


            foreach (int number in numbers)
            {

                foreach (Tuple<Matrix<double>, Matrix<double>> board in boards)
                {
                    for (int row = 0; row < board.Item1.RowCount; row++)
                    {
                        for (int col = 0; col < board.Item1.ColumnCount; col++)
                        {
                            if (board.Item1[row, col] == number)
                            {
                                board.Item2[row, col] = 1;
                            }

                        }
                    }

                    // check winner
                    for (int idx = 0; idx < board.Item1.RowCount; idx++)
                    {
                        if (!board.Item2.Row(idx).Contains(0))
                        {
                            return new Tuple<Matrix<double>, Matrix<double>, int>(board.Item1, board.Item2, number);
                        }

                        if (!board.Item2.Column(idx).Contains(0))
                        {
                            return new Tuple<Matrix<double>, Matrix<double>, int>(board.Item1, board.Item2, number);
                        }
                    }
                }

                

            }
            return null;

        }
    }
}
