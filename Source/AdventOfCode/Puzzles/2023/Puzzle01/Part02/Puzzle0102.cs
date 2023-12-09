using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode.Puzzles._2023.Puzzle01.Part02;

public partial class Puzzle0102(int year) : BasePuzzle(year)
{
    /// <inheritdoc />
    protected override int Day => 1;
    
    /// <inheritdoc />
    protected override int Part => 2;

    /// <summary>Matches on the word "one" or the number "1"</summary>
    [GeneratedRegex("(one|1)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex _oneWordPattern();
    
    /// <summary>Matches on the word "two" or the number "2"</summary>
    [GeneratedRegex("(two|2)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex _twoWordPattern();
    
    /// <summary>Matches on the word "three" or the number "3"</summary>
    [GeneratedRegex("(three|3)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex _threeWordPattern();
    
    /// <summary>Matches on the word "four" or the number "4"</summary>
    [GeneratedRegex("(four|4)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex _fourWordPattern();
    
    /// <summary>Matches on the word "five" or the number "5"</summary>
    [GeneratedRegex("(five|5)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex _fiveWordPattern();
    
    /// <summary>Matches on the word "six" or the number "6"</summary>
    [GeneratedRegex("(six|6)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex _sixWordPattern();
    
    /// <summary>Matches on the word "seven" or the number "7"</summary>
    [GeneratedRegex("(seven|7)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex _sevenWordPattern();
    
    /// <summary>Matches on the word "eight" or the number "8"</summary>
    [GeneratedRegex("(eight|8)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex _eightWordPattern();

    /// <summary>Matches on the word "nine" or the number "9"</summary>
    [GeneratedRegex("(nine|9)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex _nineWordPattern();

    private static readonly Dictionary<string, string> IntByName = new()
    {
        { "one", "1" }, { "two", "2" }, { "three", "3" },
        { "four", "4" }, { "five", "5" }, { "six", "6" },
        { "seven", "7" }, { "eight", "8" }, { "nine", "9" }
    };
    
    private static readonly Dictionary<string, Regex> RegexByName = new()
    {
        { "one", _oneWordPattern() }, { "two", _twoWordPattern() }, { "three", _threeWordPattern() },
        { "four", _fourWordPattern() }, { "five", _fiveWordPattern() }, { "six", _sixWordPattern() },
        { "seven", _sevenWordPattern() }, { "eight", _eightWordPattern() }, { "nine", _nineWordPattern() }
    };

    /// <inheritdoc />
    public override void Solve()
    {
        List<string> fileInput = base.GetPuzzleInput();

        int calibrationsSum = fileInput.Select(fileLine =>
        {
            IEnumerable<string> validNumbers = IntByName.Keys
                .SelectMany(word =>
                {
                    Regex regexToUse = RegexByName[word];
                    MatchCollection matches = regexToUse.Matches(fileLine);

                    return matches.Count > 0
                        ? matches.Select(match => (match.Index, word))
                        : Enumerable.Empty<(int, string)>();
                })
                .OrderBy(matchData => matchData.Item1)
                .Select(matchData => IntByName[matchData.Item2]);
            
            string numbersOnly =  string.Join(string.Empty, validNumbers);

            return int.Parse($"{numbersOnly.First()}{numbersOnly.Last()}");
        }).Sum();
        
        Console.WriteLine(base.GetFormattedSolutionString(calibrationsSum));
    }
}