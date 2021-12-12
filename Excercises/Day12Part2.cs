using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day12Part2 : Day12Part1
    {
        public Day12Part2()
        {
            Day = 12;
            Part = 2;
        }

        protected override bool CanVisitAgain(string node, List<string> path)
        {

            if (node == "start") return false;


            if (node == "end")
            {
                int count = path.Count(x => x == node);
                return count < 1;
            }


            if (node.ToLower() == node)
            {

                if (!path.Contains(node)) return true;

                List<string> smallCaves = path.Where(y => y.ToLower() == y).Distinct().ToList();

                foreach (string cave in smallCaves)
                {
                    int count = path.Count(x => x == cave);
                    if (count == 2)
                    {
                        return false;
                    }
                }

                return true;

            }

            if (node.ToUpper() == node)
            {
                return true;
            }

            return false;

        }
    }
}
