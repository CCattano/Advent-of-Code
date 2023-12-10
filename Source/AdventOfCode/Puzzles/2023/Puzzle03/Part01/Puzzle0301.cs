using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode.Puzzles._2023.Puzzle03.Part01;

public partial class Puzzle0301 : BasePuzzle
{
    /// <inheritdoc />
    protected override int Day => 3;

    /// <inheritdoc />
    protected override int Part => 1;

    [GeneratedRegex("[^\\.0-9]")]
    private static partial Regex _notNumOrPeriodPattern();

    private readonly List<string> _fileInput;
    private readonly int _bottomBoundary;
    private readonly int _rightBoundary;
    private readonly HashSet<char> _symbolList = [];

    public Puzzle0301(int year) : base(year)
    {
        _fileInput = base.GetPuzzleInput();
        _bottomBoundary = _fileInput.Count - 1;
        _rightBoundary = _fileInput.First().Length - 1;
        Regex notNumOrPeriodPattern = _notNumOrPeriodPattern();
        _fileInput.ForEach(line =>
            notNumOrPeriodPattern.Matches(line).ToList().ForEach(match =>
                _symbolList.Add(match.Value[0])));
    }

    /// <inheritdoc />
    public override void Solve()
    {
        int solutionSum = 0;

        int row = -1;
        while (++row <= _bottomBoundary)
        {
            int col = -1;
            while (++col <= _rightBoundary)
            {
                char posVal = _fileInput[row][col];

                if (!char.IsNumber(posVal))
                    continue;

                string number = GetNumber(row, col);

                int i = -1;
                if (number.Any(_ => DigitAdjacentToSpecChar(row, col + ++i)))
                    solutionSum += int.Parse(number);

                col += number.Length - 1;
            }
        }

        Console.WriteLine(base.GetFormattedSolutionString(solutionSum));
    }

    private string GetNumber(int row, int startingCol)
    {
        StringBuilder strBuilder = new();
        strBuilder.Append(_fileInput[row][startingCol]);

        int col = startingCol;
        while (++col <= _rightBoundary)
        {
            char nextChar = _fileInput[row][col];
            if (!char.IsNumber(nextChar))
                break;
            strBuilder.Append(nextChar);
        }

        return strBuilder.ToString();
    }

    private static bool NoOp(int _, int __) => false;
    private bool UpAndLeftIsSpecChar(int row, int col) => _symbolList.Contains(_fileInput[row - 1][col - 1]);
    private bool UpIsSpecChar(int row, int col) => _symbolList.Contains(_fileInput[row - 1][col]);
    private bool UpAndRightIsSpecChar(int row, int col) => _symbolList.Contains(_fileInput[row - 1][col + 1]);
    private bool LeftIsSpecChar(int row, int col) => _symbolList.Contains(_fileInput[row][col - 1]);
    private bool RightIsSpecChar(int row, int col) => _symbolList.Contains(_fileInput[row][col + 1]);
    private bool DownAndLeftIsSpecChar(int row, int col) => _symbolList.Contains(_fileInput[row + 1][col - 1]);
    private bool DownIsSpecChar(int row, int col) => _symbolList.Contains(_fileInput[row + 1][col]);
    private bool DownAndRightIsSpecChar(int row, int col) => _symbolList.Contains(_fileInput[row + 1][col + 1]);

    private bool DigitAdjacentToSpecChar(int row, int col) => ((List<Func<int, int, bool>>)
    [
        row - 1 >= 0 && col - 1 >= 0 ? UpAndLeftIsSpecChar : NoOp,
        row - 1 >= 0 ? UpIsSpecChar : NoOp,
        row - 1 >= 0 && col + 1 <= _rightBoundary ? UpAndRightIsSpecChar : NoOp,
        col - 1 >= 0 ? LeftIsSpecChar : NoOp,
        col + 1 <= _rightBoundary ? RightIsSpecChar : NoOp,
        row + 1 <= _bottomBoundary && col - 1 >= 0 ? DownAndLeftIsSpecChar : NoOp,
        row + 1 <= _bottomBoundary ? DownIsSpecChar : NoOp,
        row + 1 <= _bottomBoundary && col + 1 <= _rightBoundary ? DownAndRightIsSpecChar : NoOp
    ]).Any(func => func(row, col));
}