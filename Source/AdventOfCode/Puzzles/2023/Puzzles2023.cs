using AdventOfCode.Infrastructure;
using AdventOfCode.Puzzles._2023.Puzzle01.Part01;
using AdventOfCode.Puzzles._2023.Puzzle01.Part02;
using AdventOfCode.Puzzles._2023.Puzzle02.Part01;
using AdventOfCode.Puzzles._2023.Puzzle02.Part02;

namespace AdventOfCode.Puzzles._2023;

public class Puzzles2023 : BasePuzzleYear
{
    /// <inheritdoc />
    protected override int Year => 2023;
    
    /// <inheritdoc />
    public override BasePuzzle GetPuzzle(int day, int part) => (day, part) switch
    {
        (1, 1) => new Puzzle0101(Year), (1, 2) => new Puzzle0102(Year),
        (2, 1) => new Puzzle0201(Year), (2, 2) => new Puzzle0202(Year),
        _ => throw new InvalidDataException("Invalid Day/Part combination.")
    };
}