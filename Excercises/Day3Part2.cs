namespace AdventOfCode2021.Excercises;

public class Day3Part2 : ExcerciseBase
{
    public Day3Part2()
    {
        Day = 3;
        Part = 2;
    }

    public override void Run()
    {

        List<string> lines = new List<string>();

        ReadFileLineByLine("data\\day3.txt", line =>
        {
            lines.Add(line);
        });

        List<string> oxygen = new List<string>(lines);
        List<string> co2 = new List<string>(lines);

        int digits = lines[0].Length;

        for (int j = 0; j < digits; j++)
        {
            string mostCommon = CalculateMostSignificantBits(oxygen);

            char mostCommonBit = mostCommon[j];

            if (oxygen.Count > 1)
            {
                oxygen = oxygen.Where(x => x[j] == mostCommonBit).ToList();    
            }

            mostCommon = CalculateMostSignificantBits(co2);
            mostCommonBit = mostCommon[j];
            char leastCommonBit = mostCommonBit == '1' ? '0' : '1';
            if (co2.Count > 1)
            {
                co2 = co2.Where(x => x[j] == leastCommonBit).ToList();    
            }
            
        }

        int oxyRate = Convert.ToInt32(oxygen[0], 2);
        int co2Rate = Convert.ToInt32(co2[0], 2);

        Console.WriteLine($"Oxygen: {oxyRate} - CO2: {co2Rate} - Result: {oxyRate * co2Rate}");
    }

    private string CalculateMostSignificantBits(List<string> lines)
    {
        Dictionary<int, int> ones = new Dictionary<int, int>();
        foreach (string line in lines)
        {
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
        }
        int digits = ones.Keys.Max();

        string mostCommon = "";
        
        for (int j = 0; j <= digits; j++)
        {
            if (ones[j] >= lines.Count / 2)
            {
                mostCommon += "1";
            }
            else
            {
                mostCommon += "0";
            }
        }

        return mostCommon;
    }
}