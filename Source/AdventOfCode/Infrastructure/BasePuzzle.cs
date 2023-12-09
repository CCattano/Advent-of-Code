using System.Runtime.CompilerServices;

namespace AdventOfCode.Infrastructure;

public abstract class BasePuzzle(int year)
{
    /// <summary>
    /// The day the puzzle was released
    /// </summary>
    protected abstract int Day { get; }
    /// <summary>
    /// What part of the daily puzzle the puzzle being run represents
    /// </summary>
    protected abstract int Part { get; }
    
    /// <summary>
    /// The year the puzzle is associated with
    /// </summary>
    private readonly int _year = year;

    /// <summary>
    /// The function to run to solve and output the solution of the puzzle
    /// </summary>
    public abstract void Solve();

    /// <summary>
    /// Reads the PuzzleInput.txt file associated with the puzzle part
    /// </summary>
    /// <returns></returns>
    protected List<string> GetPuzzleInput()
    {
        const string inputName = "PuzzleInput.txt";
        string currDir = Directory.GetCurrentDirectory();
        string fileLoc = Path.Join("Puzzles", $"{_year}", GetPaddedStr(Day, true), GetPaddedStr(Part, false));
        string fullFilePath = Path.Join(currDir, fileLoc, inputName);
        List<string> fileLines = File.ReadAllLines(fullFilePath).ToList();
        return fileLines;
    }

    /// <summary>
    /// Generate string to write out when solution has been determined
    /// </summary>
    /// <param name="solution"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected string GetFormattedSolutionString<T>(T solution) => 
        $"Year {_year}, {GetPaddedStr(Day, true, true)}, {GetPaddedStr(Part, false, true)} - Solution: {solution}";

    private static string GetPaddedStr(int num, bool isDay, bool delimit = false) =>
        (isDay ? "Puzzle" : "Part") + (delimit ? " " : string.Empty) + $"{num}".PadLeft(2, '0');
}