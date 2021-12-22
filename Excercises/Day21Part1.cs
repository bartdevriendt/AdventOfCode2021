using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day21Part1 : ExcerciseBase
    {

        private int nextValue = 1;
        private int[] playerPositions = new int[2] { 6, 3 };
        private long[] playerScores = new long[2] { 0, 0 };
        private int currentPlayer = 0;
        private int dieRolls = 0;
        public int EndScore { get; set; } = 1000;

        public Day21Part1()
        {
            Day = 21;
            Part = 1;
        }

        public override void Run()
        {
            while (true)
            {
                int movePosition = GetNextDieValue() + GetNextDieValue() + GetNextDieValue();
                MovePlayer(currentPlayer, movePosition);

                Console.WriteLine($"Player {currentPlayer} on position {playerPositions[currentPlayer]}");

                playerScores[currentPlayer] += playerPositions[currentPlayer];

                if (playerScores[currentPlayer] >= EndScore)
                {
                    NextPlayer();
                    Console.WriteLine($"Player {currentPlayer + 1} lost.  Total value = {dieRolls * playerScores[currentPlayer]}");
                    break;
                }

                NextPlayer();
            }
        }

        protected void NextPlayer()
        {
            currentPlayer++;
            if (currentPlayer == playerPositions.Length)
            {
                currentPlayer = 0;
            }
        }

        protected void MovePlayer(int player, int positions)
        {
            playerPositions[player] += positions;
            while (playerPositions[player] > 10) playerPositions[player] -= 10;
        }

        protected virtual int GetNextDieValue()
        {
            dieRolls++;
            return nextValue++;
        }
    }
}
