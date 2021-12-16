using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dijkstra.NET.Graph;
using Dijkstra.NET.Graph.Simple;
using Dijkstra.NET.ShortestPath;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace AdventOfCode2021.Excercises
{
    internal class Day15Part1 : ExcerciseBase
    {
        public Day15Part1()
        {
            Day = 15;
            Part = 1;
        }

        private Dictionary<string, uint> indexes = new Dictionary<string, uint>();

        protected virtual Matrix<double> PrepareMatrix(Matrix<double> matrix)
        {
            return matrix;
        }
        public override void Run()
        {
            Matrix<double> input = ReadMatrix("data\\day15.txt");
            input = PrepareMatrix(input);
            var graph = new Graph<string, string>();

            for (int r = 0; r < input.RowCount; r++)
            {
                for (int c = 0; c < input.ColumnCount; c++)
                {
                    string key = $"{r},{c}";
                    uint idx = graph.AddNode(key);
                    indexes[key] = idx;
                }
            }
            for (int r = 0; r < input.RowCount; r++)
            {
                for (int c = 0; c < input.ColumnCount; c++)
                {
                    if (IsValid(r - 1, c, input))
                    {

                        string keyFrom = $"{r},{c}";
                        string keyTo = $"{r - 1},{c}";

                        graph.Connect(indexes[keyFrom], indexes[keyTo], Convert.ToInt32(input[r - 1, c]), "");
                    }
                    if (IsValid(r + 1, c, input))
                    {

                        string keyFrom = $"{r},{c}";
                        string keyTo = $"{r + 1},{c}";

                        graph.Connect(indexes[keyFrom], indexes[keyTo], Convert.ToInt32(input[r + 1, c]), "");
                    }
                    if (IsValid(r, c - 1, input))
                    {

                        string keyFrom = $"{r},{c}";
                        string keyTo = $"{r},{c-1}";

                        graph.Connect(indexes[keyFrom], indexes[keyTo], Convert.ToInt32(input[r, c - 1]), "");
                    }
                    if (IsValid(r, c + 1, input))
                    {

                        string keyFrom = $"{r},{c}";
                        string keyTo = $"{r},{c + 1}";

                        graph.Connect(indexes[keyFrom], indexes[keyTo], Convert.ToInt32(input[r, c + 1]), "");
                    }
                }
            }

            uint startNode = indexes["0,0"];
            uint endNode = indexes[$"{input.RowCount - 1},{input.ColumnCount - 1}"];

            var result = graph.Dijkstra(startNode, endNode);
            Console.WriteLine(result.Distance);
        }

        private bool IsValid(int r, int c, Matrix<double> matrix)
        {
            if (r >= 0 && c >= 0 && r < matrix.RowCount && c < matrix.ColumnCount)
            {
                return true;
            }

            return false;
        }

        
        
    }
}
