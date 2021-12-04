using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace AdventOfCode2021.Excercises
{
    internal class Day4Part2 : ExcerciseBase
    {
        public Day4Part2()
        {
            Day = 4;
            Part = 2;
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
                            currentBoard[lineCount, j] = Convert.ToInt32(parts[j].Trim());
                        }

                        lineCount++;

                    }
                }
            });

            List<int> numbers = randomNumbers.Split(",".ToCharArray()).Select(x => Convert.ToInt32(x)).ToList();


            int lastScore = 0;

            while (true)
            {
                var result = PlayBingoAndReturnWinningBoard(numbers, boards);

                if (result != null)
                {

                    int sum = 0;

                    var board = boards[result.Item1];

                    for (int row = 0; row < board.Item1.RowCount; row++)
                    {
                        for (int col = 0; col < board.Item1.ColumnCount; col++)
                        {
                            if (board.Item2[row, col] == 0)
                            {
                                sum += Convert.ToInt32(board.Item1[row, col]);
                            }
                        }
                    }


                    lastScore = sum * result.Item2;

                    boards.RemoveAt(result.Item1);
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine($"Result : {lastScore}");

        }

        private Tuple<int, int> PlayBingoAndReturnWinningBoard(List<int> numbers, List<Tuple<Matrix<double>, Matrix<double>>> boards)
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
                            return new Tuple<int, int>(boards.IndexOf(board), number);
                        }

                        if (!board.Item2.Column(idx).Contains(0))
                        {
                            return new Tuple<int, int>(boards.IndexOf(board), number);
                        }
                    }
                }



            }
            return null;

        }
    }
}
