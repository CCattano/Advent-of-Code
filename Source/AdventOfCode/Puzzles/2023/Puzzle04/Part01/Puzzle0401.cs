using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode.Puzzles._2023.Puzzle04.Part01;

public class Puzzle0401(int year) : BasePuzzle(year)
{
    /// <inheritdoc />
    protected override int Day => 4;

    /// <inheritdoc />
    protected override int Part => 1;

    /// <inheritdoc />
    public override void Solve()
    {
        int sumOfCardScores = base.GetPuzzleInput().Select(fileLine =>
        {
            fileLine = fileLine.Replace("  ", " ");
            string[] numbers = fileLine.Split(':').Last().Split('|');
            
            string winningNumbersRegexPattern = $@"(\b{numbers.First().Trim().Replace(" ", @"\b|\b")}\b)";
            Regex winningNumbersPattern = new(winningNumbersRegexPattern);
            List<Match> matches = winningNumbersPattern.Matches(numbers.Last().Trim()).ToList();

            int cardScore = matches.Count >= 1 ? 1 : 0;
            if (matches.Count > 1) 
                matches.Skip(1).ToList().ForEach(_ => cardScore *= 2);
            return cardScore;
        }).Sum();
        
        Console.WriteLine(base.GetFormattedSolutionString(sumOfCardScores));
    }
}