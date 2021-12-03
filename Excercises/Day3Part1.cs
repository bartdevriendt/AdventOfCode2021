namespace AdventOfCode2021.Excercises;

public class Day3Part1 : ExcerciseBase
{
    public Day3Part1()
    {
        Day = 3;
        Part = 1;
    }

    public override void Run()
    {

        int lines = 0;
        Dictionary<int, int> ones = new Dictionary<int, int>();

        ReadFileLineByLine("data\\day3.txt", line =>
        {
            lines++;
            for (int j = 0; j < line.Length; j++)
            {
                if (!ones.ContainsKey(j))
                {
                    ones[j] = 0;
                }

                if (line[j] == '1')
                {
                    ones[j]++;
                }
            }  
        });

        int digits = ones.Keys.Max();

        string gammaRate = "";
        string epsilonRate = "";
        for (int j = 0; j <= digits; j++)
        {
            if (ones[j] > lines / 2)
            {
                gammaRate += "1";
                epsilonRate += "0";
            }
            else
            {
                gammaRate += "0";
                epsilonRate += "1";
            }

            
        }

        int gamma = Convert.ToInt32(gammaRate, 2);
        int epsilon = Convert.ToInt32(epsilonRate, 2);


        Console.WriteLine($"Gamma: {gamma} - Epsilon: {epsilon} - Result: {gamma * epsilon}");
    }
}