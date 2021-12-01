using System;
using System.IO;

namespace AdventOfCode2021
{
    public class ExcerciseBase
    {

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

        public virtual void Run()
        {
            
        }
    }
}