using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode.Puzzles._2023.Puzzle01.Part01;

public partial class Puzzle0101(int year) : BasePuzzle(year)
{
    /// <inheritdoc />
    protected override int Day => 1;
    
    /// <inheritdoc />
    protected override int Part => 1;
    
    /// <summary>
    /// Regex pattern that matches on any alphabet character case-insensitively
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("[a-z]", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex _lettersOnlyPattern();

    /// <inheritdoc />
    public override void Solve()
    {
        List<string> fileInput = base.GetPuzzleInput();

        Regex lettersOnlyPattern = _lettersOnlyPattern();
        int calibrationsSum = fileInput.Select(fileLine =>
        {
            string numbersOnly = lettersOnlyPattern.Replace(fileLine, string.Empty);
            return int.Parse($"{numbersOnly.First()}{numbersOnly.Last()}");
        }).Sum();
        Console.WriteLine(base.GetFormattedSolutionString(calibrationsSum));
    }
}