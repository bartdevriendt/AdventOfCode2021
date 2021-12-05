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