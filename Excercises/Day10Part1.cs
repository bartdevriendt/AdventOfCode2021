using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day10Part1 : ExcerciseBase
    {
        public Day10Part1()
        {
            Day = 10;
            Part = 1;
        }

        public override void Run()
        {

            int score = 0;

            Dictionary<char, int> scores = new Dictionary<char, int>();
            scores[')'] = 3;
            scores[']'] = 57;
            scores['}'] = 1197;
            scores['>'] = 25137;

            Dictionary<char, char> mapping = new Dictionary<char, char>();
            mapping[')'] = '(';
            mapping['}'] = '{';
            mapping['>'] = '<';
            mapping[']'] = '[';

            
            ReadFileLineByLine("data\\day10.txt", line =>
            {
                Stack<char> parseStack = new Stack<char>();


                Console.WriteLine(line);

                
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
                            Console.WriteLine($"Found {c} as wrong closing char");
                            score += scores[c];
                            break;
                        }

                            
                    }

                    
                }
            });

            Console.WriteLine($"Total score: {score}");
        }

    }
}
