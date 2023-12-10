using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode.Puzzles._2023.Puzzle04.Part02;

public class Puzzle0402(int year) : BasePuzzle(year)
{
    /// <inheritdoc />
    protected override int Day => 4;

    /// <inheritdoc />
    protected override int Part => 2;

    /// <inheritdoc />
    public override void Solve()
    {
        List<int> cardEntitlements = base.GetPuzzleInput().Select(fileLine =>
        {
            fileLine = fileLine.Replace("  ", " ");
            string[] numbers = fileLine.Split(':').Last().Split('|');
            
            string winningNumbersRegexPattern = $@"(\b{numbers.First().Trim().Replace(" ", @"\b|\b")}\b)";
            Regex winningNumbersPattern = new(winningNumbersRegexPattern);
            
            List<Match> matches = winningNumbersPattern.Matches(numbers.Last().Trim()).ToList();
            return matches.Count;
        }).ToList();

        List<int> cardsWon = cardEntitlements.Select(_ => 1).ToList();
        
        for (int i = 0; i < cardEntitlements.Count; i++)
        {
            int amtOfTimesToDistribute = cardsWon[i]; 
            for (int ni = 0; ni < amtOfTimesToDistribute; ni++)
            {
                int distributionAmt = cardEntitlements[i];
                for (int nni = 1; nni <= distributionAmt; nni++)
                {
                    int cardIdxToInc = i + nni;
                    cardsWon[cardIdxToInc]++;
                }
            }
        }

        Console.WriteLine(base.GetFormattedSolutionString(cardsWon.Sum()));
    }
}