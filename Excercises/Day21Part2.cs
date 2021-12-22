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

        public Day21Part2()
        {
            Day = 21;
            Part = 2;
        }

        public override void Run()
        {
            var task = Task.Factory.StartNew(()=> NextPlayer(0, 0, 4, 8, 1));
            task.Wait();
            Console.WriteLine($"Player wins: {playerWins[0]} - {playerWins[1]}");
        }

        

        protected async Task NextPlayer(int player1score, int player2score, int player1position, int player2position, int currentPlayer)
        {

            List<Task<Task>> tasks = new List<Task<Task>>();

            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    for (int k = 1; k <= 3; k++)
                    {
                        int totalMove = i + j + k;
                        if (currentPlayer == 1)
                        {
                            int nextPos = player1position + totalMove;
                            while(nextPos > 10) nextPos -= 10;
                            int nextScore = player1score + nextPos;
                            if (nextScore >= EndScore)
                            {
                                playerWins[0]++;
                            }
                            else
                            {
                                var t = Task.Factory.StartNew(() => NextPlayer(nextScore, player2score, nextPos, player2position, 2));
                                tasks.Add(t);
                            }
                            

                        }
                        else if (currentPlayer == 2)
                        {
                            int nextPos = player2position + totalMove;
                            while (nextPos > 10) nextPos -= 10;
                            int nextScore = player2score + nextPos;
                            if (nextScore >= EndScore)
                            {
                                playerWins[1]++;
                            }
                            else
                            {
                                var t = Task.Factory.StartNew(() => NextPlayer(player1score, nextScore, player1position, nextPos, 1));
                                tasks.Add(t);
                            }


                        }
                    }
                }
            }

            if (tasks.Count > 0)
            {
                Task.WaitAll(tasks.ToArray());
            }
            
        }

        
        
    }
}
