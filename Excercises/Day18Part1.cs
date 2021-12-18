using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class PairTree
    {
        public int Left { get; set; }
        public int Right { get; set; }

        public PairTree PairLeft { get; set; }
        public PairTree PairRight { get; set; }

        public PairTree Parent { get; set; }


        public PairTree(string input, PairTree parent)
        {
            Parent = parent;
            int brackets = 0;
            int commaPosition = -1;
            for (int j = 1; j < input.Length - 1; j++)
            {
                if (input[j] == '[')
                {
                    brackets++;
                }
                else if (input[j] == ']')
                {
                    brackets--;
                }
                else if (input[j] == ',')
                {
                    if (brackets == 0)
                    {
                        commaPosition = j;
                        break;
                    }
                }
            }


            if (commaPosition > 0)
            {
                string leftPart = input.Substring(1, commaPosition - 1);
                string rightPart = input.Substring(commaPosition + 1, input.Length - commaPosition - 2);

                if (!leftPart.Contains("[") && !leftPart.Contains("]"))
                {
                    Left = Convert.ToInt32(leftPart);
                }
                else
                {
                    PairLeft = new PairTree(leftPart, this);
                }
                if (!rightPart.Contains("[") && !rightPart.Contains("]"))
                {
                    Right = Convert.ToInt32(rightPart);
                }
                else
                {
                    PairRight = new PairTree(rightPart, this);
                }
            }
        }

        public int Magnitude()
        {
            int result = 0;

            if (PairLeft != null)
            {
                result += PairLeft.Magnitude() * 3;
            }
            else
            {
                result += 3 * Left;
            }

            if (PairRight != null)
            {
                result += PairRight.Magnitude() * 2;
            }
            else
            {
                result += 2 * Right;
            }

            return result;
        }
    }

    public class Pair
    {
        public string Number { get; set; }

        
        public Pair(string number)
        {
            Number = number;
        }

        public Pair AddPair(Pair other)
        {
            return new Pair("[" + Number + "," + other.Number + "]");
        }

        private bool IsNumeric(char c)
        {
            return c >= '0' && c <= '9';
        }

        public bool Reduce()
        {

            int openBrackets = 0;

            int reducePosition = -1;

            for (int j = 0; j < Number.Length; j++)
            {
                if (Number[j] == '[')
                {
                    openBrackets++;
                }
                else if (Number[j] == ']')
                {
                    openBrackets--;
                }

                if (openBrackets >= 5)
                {

                    
                    for (int k = j + 1; j < Number.Length; k++)
                    {
                        if (Number[k] == '[')
                        {
                            break;
                        }
                        else if (Number[k] == ']')
                        {
                            reducePosition = j;
                            break;
                        }
                    }

                    


                }

                if (reducePosition > 0) break;
            }

            if (reducePosition > -1)
            {

                //while (Number[reducePosition + 1] == '[')
                //{
                //    reducePosition++;
                //}

                int closeIdx = Number.IndexOf(']', reducePosition);

                string pair = Number.Substring(reducePosition + 1, closeIdx - reducePosition - 1);
                string[] parts=  pair.Split(',');


                int leftPart = Convert.ToInt32(parts[0]);
                int rightPart = Convert.ToInt32(parts[1]);

                Number = Number.Remove(reducePosition, pair.Length + 2);
                Number = Number.Insert(reducePosition, "0");

                for (int j = reducePosition - 1; j >= 0; j--)
                {
                    if (Number[j] != '[' && Number[j] != ']' && Number[j] != ',')
                    {
                        int toAdd = Convert.ToInt32(Number[j].ToString());
                        int length = 1;

                        while (IsNumeric(Number[j - length]))
                        {
                            
                            toAdd += Convert.ToInt32(Number[j - length].ToString()) * 10;
                            length++;
                        }
                        Number = Number.Remove(j - length + 1, length);
                        Number = Number.Insert(j - length + 1, (toAdd + leftPart).ToString());
                        break;
                    }
                }

                for (int j = reducePosition + 2; j < Number.Length; j++)
                {
                    if (Number[j] != '[' && Number[j] != ']' && Number[j] != ',')
                    {
                        int toAdd = Convert.ToInt32(Number[j].ToString());
                        int length = 2;

                        while (IsNumeric(Number[j + length - 1]))
                        {
                            toAdd *= 10;
                            toAdd += Convert.ToInt32(Number[j + length - 1].ToString());
                            length++;
                        }
                        Number = Number.Remove(j, length - 1);
                        Number = Number.Insert(j, (toAdd + rightPart).ToString());
                        break;
                    }
                }

                return true;
            }

            return false;
        }

        public bool Split()
        {

            for (int j = 0; j < Number.Length; j++)
            {
                if (IsNumeric(Number[j]))
                {
                    int k = j + 1;
                    while (IsNumeric(Number[k]))
                    {
                        k++;
                    }

                    if (k > j + 1)
                    {
                        int toReplace = Convert.ToInt32(Number.Substring(j, k - j));
                        int left = Convert.ToInt32(Math.Floor(toReplace / 2.0));
                        int right = Convert.ToInt32(Math.Ceiling(toReplace / 2.0));

                        Number = Number.Remove(j, k - j);
                        Number = Number.Insert(j, $"[{left},{right}]");
                        return true;
                    }
                }
            }

            return false;
        }

        public override string ToString()
        {
            return Number;
        }

        public string BracketCount()
        {
            int brackets = 0;

            string result = "";
            for (int j = 0; j < Number.Length; j++)
            {
                if (Number[j] == '[')
                {
                    brackets++;
                    result += brackets.ToString();
                }
                else if (Number[j] == ']')
                {
                    brackets--;
                    result += brackets.ToString();
                }
                else
                {
                    result += ".";
                }

            }

            return result;
        }
    }

    internal class Day18Part1 : ExcerciseBase
    {
        public Day18Part1()
        {
            Day = 18;
            Part = 1;
        }

        public override void Run()
        {
            List<string> numbers = new List<string>();

            ReadFileLineByLine("data\\day18.txt", line =>
            {
                numbers.Add(line);
            });

            ProcessResult(numbers);
        }

        private Pair ParseLine(string line)
        {
            Pair p = new Pair(line);

            return p;
        }


        protected Pair ProcessList(List<string> lines)
        {
            Pair number = null;

            int i = 0;

            foreach (string line in lines)
            {
                Pair newPair = ParseLine(line);
                if (number == null)
                {
                    number = newPair;
                    Console.WriteLine($"   After start: {number}");

                    bool didAction = true;

                    while (didAction)
                    {
                        didAction = false;
                        if (number.Reduce())
                        {
                            didAction = true;
                            Console.WriteLine($"   After reduce: {number}");
                            //Console.WriteLine($"   Brackets:     {number.BracketCount()}");
                            continue;

                        }

                        if (number.Split())
                        {
                            didAction = true;
                            Console.WriteLine($"   After split:  {number}");
                            //Console.WriteLine($"   Brackets:     {number.BracketCount()}");
                            continue;
                        }
                    }


                }
                else
                {
                    number = number.AddPair(newPair);
                    Console.WriteLine($"   After add: {number}");

                    bool didAction = true;

                    while (didAction)
                    {
                        didAction = false;
                        if (number.Reduce())
                        {
                            didAction = true;
                            Console.WriteLine($"   After reduce: {number}");
                            //Console.WriteLine($"   Brackets:     {number.BracketCount()}");
                            continue;

                        }

                        if (number.Split())
                        {
                            didAction = true;
                            Console.WriteLine($"   After split:  {number}");
                            //Console.WriteLine($"   Brackets:     {number.BracketCount()}");
                            continue;
                        }
                    }

                }

                i++;

                Console.WriteLine($"After step {i}: {number}");
            }

            return number;
        }

        protected virtual void ProcessResult(List<string> lines)
        {

            Pair number = ProcessList(lines);

            PairTree pt = new PairTree(number.Number, null);
            Console.WriteLine($"Magnitude = {pt.Magnitude()}");
        }
    }
}
