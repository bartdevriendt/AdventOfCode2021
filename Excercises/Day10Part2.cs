using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day10Part2 : ExcerciseBase
    {
        public Day10Part2()
        {
            Day = 10;
            Part = 2;
        }


        public override void Run()
        {
            Dictionary<char, char> mapping = new Dictionary<char, char>();
            mapping[')'] = '(';
            mapping['}'] = '{';
            mapping['>'] = '<';
            mapping[']'] = '[';

            Dictionary<char, char> reverseMapping = new Dictionary<char, char>();
            reverseMapping['('] = ')';
            reverseMapping['{'] = '}';
            reverseMapping['<'] = '>';
            reverseMapping['['] = ']';

            Dictionary<char, int> scores = new Dictionary<char, int>();
            scores[')'] = 1;
            scores[']'] = 2;
            scores['}'] = 3;
            scores['>'] = 4;

            List<long> lineScores = new List<long>();

            ReadFileLineByLine("data\\day10.txt", line =>
            {
                Stack<char> parseStack = new Stack<char>();


                Console.WriteLine(line);

                bool corrupted = false;

                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];

                    if (c == '{' || c == '<' || c == '[' || c == '(')
                    {

                        parseStack.Push(c);
                    }
                    else
                    {

                        char m = mapping[c];

                        if (parseStack.Peek() == m)
                        {
                            parseStack.Pop();
                        }
                        else
                        {
                            Console.WriteLine("Line is corrupt");
                            corrupted = true;
                            break;
                        }


                    }


                }

                if (!corrupted)
                {
                    long lineScore = 0;

                    Console.WriteLine("   Closing with " + String.Join("", parseStack.ToArray()));

                    while (parseStack.Count > 0)
                    {
                        char open = parseStack.Pop();
                        char close = reverseMapping[open];
                        Console.WriteLine($"         Closing char {close}");
                        lineScore *= 5;
                        lineScore += scores[close];
                    }

                    lineScores.Add(lineScore);
                }

            });

            lineScores.Sort();

            int middleIndex = Convert.ToInt32(Math.Round(lineScores.Count / 2.0)) - 1;

            long result = lineScores[middleIndex];
            Console.WriteLine($"Middle score at index {middleIndex} is {result}");
        }
    }
}
