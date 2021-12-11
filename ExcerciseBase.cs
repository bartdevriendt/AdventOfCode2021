using System;
using System.IO;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace AdventOfCode2021
{
    public class ExcerciseBase
    {

        public int Day { get; set; }
        public int Part { get; set; }
        
        protected void ReadFileLineByLine(string file, Action<string> action)
        {

            if (!File.Exists(file))
            {
                throw new FileNotFoundException("File not found");
            }

            StreamReader rdr = new StreamReader(file);

            string line;

            while ((line = rdr.ReadLine()) != null)
            {
                action.Invoke(line);    
            }
            
            rdr.Close();
        }

        protected string ReadFullFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("File not found");
            }

            StreamReader rdr = new StreamReader(file);

            string content = rdr.ReadToEnd().Trim();
            rdr.Close();
            return content;

        }

        protected Matrix<double> ReadMatrix(string file)
        {
            string input = ReadFullFile(file);
            string[] lines = input.Split("\r\n");
            int rows = lines.Length;
            int cols = lines[0].Trim().Length;

            Matrix<double> m = Matrix<double>.Build.Sparse(rows, cols, -1);


            for (int j = 0; j < rows; j++)
            {
                for (int k = 0; k < cols; k++)
                {
                    m[j, k] = Convert.ToInt32(lines[j][k].ToString());
                }
            }

            return m;
        }
        
        protected void PrintMatrix<T>(Matrix<T> matrix)
            where T: struct, IFormattable, IEquatable<T>
        {

            Console.WriteLine(matrix);
            
            //for (int x = 0; x < matrix.ColumnCount; x++)
            //{

            //}
        }

        public virtual void Run()
        {
            
        }
    }
}