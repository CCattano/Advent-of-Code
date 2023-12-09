using AdventOfCode.Infrastructure;
using Puzzle0101 = AdventOfCode.Puzzles._2023.Puzzle01.Part01.Puzzle0101;
using Puzzle0102 = AdventOfCode.Puzzles._2023.Puzzle01.Part02.Puzzle0102;

namespace AdventOfCode.Puzzles._2023;

public class Puzzles2023 : BasePuzzleYear
{
    /// <inheritdoc />
    protected override int Year => 2023;
    
    /// <inheritdoc />
    public override BasePuzzle GetPuzzle(int day, int part) => (day, part) switch
    {
        (1, 1) => new Puzzle0101(Year),
        (1, 2) => new Puzzle0102(Year),
        _ => throw new InvalidDataException("Invalid Day/Part combination.")
    };
}