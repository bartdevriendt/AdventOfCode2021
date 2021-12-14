using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day14Part1 : ExcerciseBase
    {

        protected int NumberOfSteps
        {
            get;set;
        }

        private Dictionary<int, long> pairCounts = new Dictionary<int, long>();

        public Day14Part1()
        {
            Day = 14;
            Part = 1;
            NumberOfSteps = 10;
        }

        public override void Run()
        {

            bool firstLine = true;

            string template = "";
            Dictionary<string, string> insertions = new Dictionary<string, string>();

            ReadFileLineByLine("data\\day14.txt", line =>
            {
                if(firstLine)
                {
                    template = line.Trim();
                    firstLine = false;
                }
                else if(line.Trim() != "")
                {
                    string ruleInput = line.Substring(0, 2);
                    string ruleOutput = line.Substring(6, 1);

                    insertions[ruleInput] = ruleOutput;
                }
            });

            RunSteps(template, insertions);

        }

        protected virtual void RunSteps(string template, Dictionary<string, string> rules)
        {


            Dictionary<string, int> mapping = new Dictionary<string, int>();

            int j = 0;
            foreach(string key in rules.Keys)
            {
                mapping[key] = j++;
            }

            Dictionary<int, Tuple<int, int>> pairMapping = new Dictionary<int, Tuple<int, int>>();

            foreach(string key in rules.Keys)
            {
                int from = mapping[key];
                string to1 = key[0] + rules[key];
                string to2 = rules[key] + key[1];

                Tuple<int, int> map = new Tuple<int, int>(mapping[to1], mapping[to2]);
                pairMapping.Add(from, map);
            }
            
            List<int> pairs = new List<int>();
            for(int k=0; k<template.Length - 1; k++)
            {
                pairs.Add(mapping[template.Substring(k, 2)]);
            }

            foreach(int pair in pairs)
            {
                if(!pairCounts.ContainsKey(pair))
                {
                    pairCounts[pair] = 0;   
                }
                pairCounts[pair]++;
            }

            int lastPair = pairs.Last();

            for(int k=0; k < NumberOfSteps; k++)
            {
                Console.WriteLine($"Calculate step {k+1}");
                CalculateStep(pairMapping);
                Console.WriteLine($"Number of pairs: {pairCounts.Values.Sum()}");
                lastPair = pairMapping[lastPair].Item2;
            }

            

            
            Dictionary<char, long> counts = new Dictionary<char, long>();

            

            foreach(int pair in pairCounts.Keys)
            {

                var m = mapping.Where(m => m.Value == pair).First();

                if(!counts.ContainsKey(m.Key[0]))
                {
                    counts[m.Key[0]] = 0;
                }
                counts[m.Key[0]] += pairCounts[pair];

            }
            var lm = mapping.Where(m => m.Value == lastPair).First();
            if (!counts.ContainsKey(lm.Key[1]))
            {
                counts[lm.Key[1]] = 0;
            }
            counts[lm.Key[1]] += 1;

            

            long maxValue = counts.Values.Max();
            long minValue = counts.Values.Min();

            Console.WriteLine($"Result = {maxValue - minValue}");
        }

        protected void CalculateStep(Dictionary<int, Tuple<int, int>> pairMapping)
        {
            Dictionary<int, long> copy = new Dictionary<int, long>(pairCounts);
                        
            foreach(int key in pairCounts.Keys)
            {
                Tuple<int, int> mapping = pairMapping[key];
                if (!copy.ContainsKey(mapping.Item1))
                {
                    copy[mapping.Item1] = 0;
                }
                if (!copy.ContainsKey(mapping.Item2))
                {
                    copy[mapping.Item2] = 0;
                }

                copy[key] -= pairCounts[key];
                copy[mapping.Item1] += pairCounts[key];
                copy[mapping.Item2] += pairCounts[key];

            }

            pairCounts = copy;
            
        }
    }
}
