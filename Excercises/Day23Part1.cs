namespace AdventOfCode2021.Excercises;

public class Board
{
    public Stack<int>[] Chambers { get; set; }
    public int Cost { get; set; }
    public int[] Hallway { get; set; }
    
    public Board Clone()
    {
        return new Board
        {
            Cost = Cost,
            Hallway = Hallway.ToArray(),
            Chambers = Chambers.Select(x => new Stack<int>(x.Reverse().ToArray())).ToArray()
        };
    }

    public string Id()
    {
        var corridor = string.Join(string.Empty, Hallway);
        var room = string.Join("_", Chambers.Select(x =>
            string.Join(string.Empty, x).PadLeft(4, '.')
        ));

        return $"{corridor}_{room}";
    }
}

public class Day23Part1 : ExcerciseBase
{
    public Day23Part1()
    {
        Day = 23;
        Part = 1;
    }
    
    protected static readonly int[] _cost =
    {
        1,
        10,
        100,
        1000
    };

    public override void Run()
    {
        var board = new Board
        {
            Cost = 0,
            Hallway = Enumerable.Repeat(4, 11).ToArray(),
            Chambers = new[]
            {
                new Stack<int>(new [] { 3, 3 }),
                new Stack<int>(new [] { 0, 1 }),
                new Stack<int>(new [] { 1, 2 }),
                new Stack<int>(new [] { 0, 2 })
            }
        };

        board.Hallway[2] = board.Hallway[4] = board.Hallway[6] = board.Hallway[8] = 5;
        
        var energy = SolveBoard(board, 2);

        Console.WriteLine($"Least energy required: {energy}");
    }

    protected int SolveBoard(Board initalBoard, int chamberSize)
    {
        var states = new PriorityQueue<Board, int>();
        states.Enqueue(initalBoard, initalBoard.Cost);

        var visited = new HashSet<string>();

        while (states.Count > 0)
        {
            var board = states.Dequeue();

            var id = board.Id();

            if (visited.Contains(id))
            {
                continue;
            }

            visited.Add(id);

            //Did we win?
            if (
                board.Chambers[0].Count == chamberSize && board.Chambers[0].All(x => x == 0) &&
                board.Chambers[1].Count == chamberSize && board.Chambers[1].All(x => x == 1) &&
                board.Chambers[2].Count == chamberSize && board.Chambers[2].All(x => x == 2) &&
                board.Chambers[3].Count == chamberSize && board.Chambers[3].All(x => x == 3)
            )
            {
                return board.Cost;
            }

            for (var c = 0; c < board.Hallway.Length; c++)
            {
                var corridor = board.Hallway[c];

                if (corridor >= 4)
                {
                    continue;
                }

                var possibleRoomIndex = corridor;
                var possibleRoom = board.Chambers[possibleRoomIndex];

                if (possibleRoom.Any(x => x != corridor))
                {
                    continue;
                }

                var beginIndex = c;
                var targetIndex = (possibleRoomIndex + 1) * 2;

                if (beginIndex < targetIndex)
                {
                    for (var cc = beginIndex + 1; cc < targetIndex; cc++)
                    {
                        var currentCorridor = board.Hallway[cc];

                        if (currentCorridor < 4)
                        {
                            goto skip;
                        }
                    }
                }
                else if (beginIndex > targetIndex)
                {
                    for (var cc = beginIndex - 1; cc > targetIndex; cc--)
                    {
                        var currentCorridor = board.Hallway[cc];

                        if (currentCorridor < 4)
                        {
                            goto skip;
                        }
                    }
                }

                var clone = board.Clone();

                var cost = ((chamberSize - clone.Chambers[possibleRoomIndex].Count) + Math.Abs(beginIndex - targetIndex)) * _cost[corridor];

                clone.Cost += cost;
                clone.Hallway[c] = 4;
                clone.Chambers[possibleRoomIndex].Push(corridor);
                states.Enqueue(clone, clone.Cost);

                skip:;
            }

            for (var s = 0; s < board.Chambers.Length; s++)
            {
                var room = board.Chambers[s];
                var amphipod = room.Any()
                    ? room.Peek()
                    : 4;

                if (amphipod == 4)
                {
                    continue;
                }

                var corridorIndex = s * 2 + 2;
                int corridor;

                
                var leftCorridor = corridorIndex - 1;
                do
                {
                    corridor = board.Hallway[leftCorridor];

                    if (corridor == 4)
                    {
                        var clone = board.Clone();

                        var cost = ((chamberSize + 1 - clone.Chambers[s].Count) + (corridorIndex - leftCorridor)) * _cost[amphipod];

                        clone.Cost += cost;
                        clone.Chambers[s].Pop();
                        clone.Hallway[leftCorridor] = amphipod;
                        states.Enqueue(clone, clone.Cost);
                    }

                    leftCorridor--;
                } while (leftCorridor >= 0 && corridor >= 4);

                
                var rightCorridor = corridorIndex + 1;
                do
                {
                    corridor = board.Hallway[rightCorridor];

                    if (corridor == 4)
                    {
                        var clone = board.Clone();

                        var cost = ((chamberSize + 1 - clone.Chambers[s].Count) + (rightCorridor - corridorIndex)) * _cost[amphipod];

                        clone.Cost += cost;
                        clone.Chambers[s].Pop();
                        clone.Hallway[rightCorridor] = amphipod;
                        states.Enqueue(clone, clone.Cost);
                    }

                    rightCorridor++;
                } while (rightCorridor < board.Hallway.Length && corridor >= 4);
            }
        }

        return 0;
    }

}