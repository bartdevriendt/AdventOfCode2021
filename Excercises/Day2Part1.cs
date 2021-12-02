namespace AdventOfCode2021.Excercises
{
    public class Day2Part1 : ExcerciseBase
    {
        public Day2Part1()
        {
            Day = 2;
            Part = 1;
        }

        public override void Run()
        {

            int position = 0;
            int depth = 0;
            
            
            ReadFileLineByLine("data\\day2.txt", line =>
            {
                string[] parts = line.Split(" ".ToCharArray());
                switch(parts[0])
                {
                    case "forward":
                        position += Convert.ToInt32(parts[1]);
                        break;
                    case "up":
                        depth -= Convert.ToInt32(parts[1]);
                        break;
                    case "down":
                        depth += Convert.ToInt32(parts[1]);
                        break;
                }
            });
            
            Console.WriteLine($"Position: {position} - Depth: {depth} - Result: {position * depth}");
        }
    }
}