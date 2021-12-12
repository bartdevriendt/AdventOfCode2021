using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Excercises
{
    internal class Day12Part1 : ExcerciseBase
    {
        public Day12Part1()
        {
            Day = 12;
            Part = 1;
        }

        public override void Run()
        {

            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

            ReadFileLineByLine("data\\day12.txt", line =>
            {

                string[] nodes = line.Trim().Split('-');

                if (!graph.ContainsKey(nodes[0]))
                {
                    graph[nodes[0]] = new List<string>();
                }

                graph[nodes[0]].Add(nodes[1]);

                if (!graph.ContainsKey(nodes[1]))
                {
                    graph[nodes[1]] = new List<string>();
                }

                graph[nodes[1]].Add(nodes[0]);

            });

            var result = FindAllPaths(graph, "start", "end", new List<string>());


            foreach (var path in result)
            {
                Console.WriteLine(String.Join(",", path));
            }

            Console.WriteLine($"Total number of paths: {result.Count}");

        }

        protected List<List<string>> FindAllPaths(Dictionary<string, List<string>> graph, string start, string end, List<string> path)
        {
            path.Add(start);

            if(start == end)
                return new List<List<string>> {path};
            if (!graph.ContainsKey(start))
                return new List<List<string>>();
            List<List<string>> paths = new List<List<string>>();

            foreach (string node in graph[start])
            {
                if (CanVisitAgain(node, path))
                {
                    List<List<string>> new_paths = FindAllPaths(graph, node, end, new List<string>(path));
                    foreach (List<string> newpath in new_paths)
                    {
                        paths.Add(newpath);
                    }
                }
            }

            return paths;
        }

        protected virtual bool CanVisitAgain(string node, List<string> path)
        {
            if ((node.ToLower() == node && !path.Contains(node)) || node.ToUpper() == node)
            {
                return true;
            }

            return false;
        }
    }
}
