using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day21Part2 : ExcerciseBase
    {
        public int EndScore { get; set; } = 21;
        
        private long[] playerWins = new long[2] { 0, 0 };

        private Dictionary<int, int> oddsThrow = new();

        public Day21Part2()
        {
            Day = 21;
            Part = 2;
        }

        public override void Run()
        {
            // number of times each combination of three throws happens
            oddsThrow.Add(3, 1);
            oddsThrow.Add(4, 3);
            oddsThrow.Add(5, 6);
            oddsThrow.Add(6, 7);
            oddsThrow.Add(7, 6);
            oddsThrow.Add(8, 3);
            oddsThrow.Add(9, 1);

            NextPlayer(0, 0, 6, 3, 1, 1);
            Console.WriteLine($"Player wins: {playerWins[0]} - {playerWins[1]}");

            var answer = playerWins[0] > playerWins[1] ? playerWins[0] : playerWins[1];

            Console.WriteLine($"Most wins: {answer}");
        }

        

        protected void NextPlayer(int player1score, int player2score, int player1position, int player2position, long occurences, int currentPlayer)
        {

            foreach (int step in oddsThrow.Keys)
            {
                if (currentPlayer == 1)
                {
                    int nextPos = MovePlayer(player1position, step);
                    int score = player1score + nextPos;
                    long probability = occurences * oddsThrow[step];
                    if (score >= 21)
                    {
                        playerWins[0] += probability;
                    }
                    else
                    {
                        NextPlayer(score, player2score, nextPos, player2position, probability, 2);
                    }
                }
                else
                {
                    int nextPos = MovePlayer(player2position, step);

                    int score = player2score + nextPos;
                    long probability = occurences * oddsThrow[step];
                    if (score >= 21)
                    {
                        playerWins[1] += probability;
                    }
                    else
                    {
                        NextPlayer(player1score, score, player1position, nextPos, probability, 1);
                    }

                }
            }
            
        }

        private int MovePlayer(int position, int steps)
        {
            int result = position + steps;
            while (result > 10) result -= 10;
            return result;

        }
        
        
    }
}
