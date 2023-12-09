namespace AdventOfCode.Infrastructure;

public abstract class BasePuzzleYear
{
    /// <summary>
    /// The year the puzzles are associated with
    /// </summary>
    protected abstract int Year { get; }
    
    /// <summary>
    /// The function to run to fetch the puzzle specified for the year
    /// </summary>
    public abstract BasePuzzle GetPuzzle(int day, int part);
}