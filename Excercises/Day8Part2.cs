using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day8Part2 : ExcerciseBase
    {

        Dictionary<int, List<int>> numberIndexes = new Dictionary<int, List<int>>();

        public Day8Part2()
        {
            Day = 8;
            Part = 2;
        }

        public override void Run()
        {
            int appearances = 0;

            numberIndexes[0] = new List<int> { 1, 2, 3, 5, 6, 7 };
            numberIndexes[1] = new List<int> { 3, 6 };
            numberIndexes[2] = new List<int> { 1, 3, 4, 5, 7 };
            numberIndexes[3] = new List<int> { 1, 3, 4, 6, 7 };
            numberIndexes[4] = new List<int> { 2, 3, 4, 6 };
            numberIndexes[5] = new List<int> { 1, 2, 4, 6, 7 };
            numberIndexes[6] = new List<int> { 1, 2, 4, 5, 6, 7 };
            numberIndexes[7] = new List<int> { 1, 3, 6 };
            numberIndexes[8] = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            numberIndexes[9] = new List<int> { 1, 2, 3, 4, 6, 7 };


            int totalOutput = 0;

            ReadFileLineByLine("data\\day8.txt", line =>
            {

                string[] input = line.Split('|');
                string[] digits = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] signals = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                List<string> combination = FindSolution(signals);


                if (combination == null)
                {
                    throw new Exception("Found no solution");
                }
                else
                {
                    totalOutput += CalcOutputValue(digits, combination);
                }

            });

            Console.WriteLine($"Total output value: {totalOutput}");
        }


        private int CalcOutputValue(string[] digits, List<string> combination)
        {
            int result = 0;
            foreach (string digit in digits)
            {

                List<int> positions = new List<int>();

                for (int j = 0; j < digit.Length; j++)
                {
                    string letter = digit[j].ToString();
                    positions.Add(combination.IndexOf(letter) + 1);
                }

                bool found = false;

                foreach (int k in numberIndexes.Keys)
                {
                    List<int> resultPos = numberIndexes[k].FindAll(x => positions.Contains(x));
                    if (positions.Count == resultPos.Count && numberIndexes[k].Count == positions.Count)
                    {
                        result *= 10;
                        result += k;
                        found = true;
                    }
                }

                if (!found)
                {
                    throw new Exception("Something went wrong");
                }
            }

            return result;
        }

        private List<string> FindSolution(string[] wireConnections)
        {
            /*
             *         1
             *       2   3
             *         4
             *       5   6
             *         7
             *       
             */
            string[] wires = new[] { "a", "b", "c", "d", "e", "f", "g" };


            var permutations = GetPermutations(wires, 7);



            foreach (var perm in permutations)
            {
                List<string> combi = perm.ToList();

                string toTest = String.Join(',', combi);

                //Console.WriteLine($"Trying combination {toTest}");

                bool matchAll = true;

                

                foreach(string wireConnection in wireConnections)
                {

                    List<int> toMatch = new List<int>();
                    for (int j = 0; j < wireConnection.Length; j++)
                    {
                        string wire = wireConnection[j].ToString();
                        toMatch.Add(combi.IndexOf(wire) + 1);
                    }

                    bool found = false;

                    foreach (List<int> positions in numberIndexes.Values)
                    {
                        List<int> result = positions.FindAll(x => toMatch.Contains(x));
                        if (result.Count == positions.Count && result.Count == toMatch.Count)
                        {
                            found = true;
                        }
                    }


                    if (!found)
                    {
                        matchAll = false;
                        break;
                    }

                }


                if (matchAll)
                {
                    return combi;
                }
              
            }

            return null;

        }

        static IEnumerable<IEnumerable<T>>
            GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
