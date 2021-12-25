namespace AdventOfCode2021.Excercises;

public class Day23Part2 : Day23Part1
{
    public Day23Part2()
    {
        Day = 23;
        Part = 2;
    }

    public override void Run()
    {
        var board = new Board
        {
            Cost = 0,
            Hallway = Enumerable.Repeat(4, 11).ToArray(),
            Chambers = new[]
            {
                new Stack<int>(new [] { 3, 3, 3, 3 }),
                new Stack<int>(new [] { 0, 1, 2, 1 }),
                new Stack<int>(new [] { 1, 0, 1, 2 }),
                new Stack<int>(new [] { 0, 2, 0, 2 })
            }
        };

        board.Hallway[2] = board.Hallway[4] = board.Hallway[6] = board.Hallway[8] = 5;
        
        var energy = SolveBoard(board, 4);

        Console.WriteLine($"Least energy required: {energy}");
    }
}