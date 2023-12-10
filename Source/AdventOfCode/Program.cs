using AdventOfCode.Infrastructure;
using AdventOfCode.Puzzles._2023;

const int year = 2023;
const int day = 5;
const int part = 2;

BasePuzzleYear puzzleYear = year switch
{
    2023 => new Puzzles2023(),
    _ => throw new InvalidDataException("Invalid Year specified")
};

BasePuzzle puzzle = puzzleYear.GetPuzzle(day, part);
puzzle.Solve();