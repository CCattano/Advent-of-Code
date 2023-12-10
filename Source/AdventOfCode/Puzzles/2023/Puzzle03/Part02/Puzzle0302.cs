using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Infrastructure;

namespace AdventOfCode.Puzzles._2023.Puzzle03.Part02;

internal enum Direction
{
    UpAndLeft,
    Up,
    UpAndRight,
    Left,
    Right,
    DownAndLeft,
    Down,
    DownAndRight
}

public partial class Puzzle0302 : BasePuzzle
{
    /// <inheritdoc />
    protected override int Day => 3;

    /// <inheritdoc />
    protected override int Part => 2;
    
    [GeneratedRegex("\\*+")]
    private static partial Regex _asteriskPattern();

    private readonly List<string> _fileInput;
    private readonly int _bottomBoundary;
    private readonly int _rightBoundary;

    public Puzzle0302(int year) : base(year)
    {
        _fileInput = base.GetPuzzleInput();
        _bottomBoundary = _fileInput.Count - 1;
        _rightBoundary = _fileInput.First().Length - 1;
    }

    /// <inheritdoc />
    public override void Solve()
    {
        Regex asteriskPattern = _asteriskPattern();
        
        int solutionSum = 0;
        
        int row = -1;
        while (++row <= _bottomBoundary)
        {
            List<Match> matches = asteriskPattern.Matches(_fileInput[row]).ToList();
            if (matches.Count == 0)
                continue;

            int col;
            foreach (Match asteriskInRow in matches)
            {
                col = asteriskInRow.Index;
                List<Direction> dirOfDigits = GetDirectionsOfAdjacentDigits(row, col);
                if (dirOfDigits.Count < 2)
                    continue;

                HashSet<int> distinctAdjacentNums = dirOfDigits.Select(dir =>
                {
                    (int startingRow, int startingCol) = GetStartingCoordsForNumSearch(dir, row, col);
                    return GetNumber(startingRow, startingCol);
                }).ToHashSet();
                
                if (distinctAdjacentNums.Count < 2)
                    continue;

                int productOfNums = distinctAdjacentNums.Aggregate(1, (acc, curr) => acc * curr);

                solutionSum += productOfNums;
            }
        }
        
        Console.WriteLine(base.GetFormattedSolutionString(solutionSum));
    }

    private int GetNumber(int row, int startingCol)
    {
        StringBuilder strBuilder = new();
        strBuilder.Append(_fileInput[row][startingCol]);

        int col = startingCol;
        while (--col >= 0)
        {
            char precedingChar = _fileInput[row][col];
            if (!char.IsNumber(precedingChar))
                break;
            strBuilder.Insert(0, precedingChar);
        }
        
        col = startingCol;
        while (++col <= _rightBoundary)
        {
            char nextChar = _fileInput[row][col];
            if (!char.IsNumber(nextChar))
                break;
            strBuilder.Append(nextChar);
        }

        string numStr = strBuilder.ToString();
        return int.Parse(numStr);
    }

    private static Direction? NoOp(int _, int __) => null;
    private Direction? UpAndLeftIsSpecChar(int row, int col) => char.IsNumber(_fileInput[row - 1][col - 1]) ? Direction.UpAndLeft : null;
    private Direction? UpIsSpecChar(int row, int col) => char.IsNumber(_fileInput[row - 1][col]) ? Direction.Up : null;
    private Direction? UpAndRightIsSpecChar(int row, int col) => char.IsNumber(_fileInput[row - 1][col + 1]) ? Direction.UpAndRight : null;
    private Direction? LeftIsSpecChar(int row, int col) => char.IsNumber(_fileInput[row][col - 1]) ? Direction.Left : null;
    private Direction? RightIsSpecChar(int row, int col) => char.IsNumber(_fileInput[row][col + 1]) ? Direction.Right : null;
    private Direction? DownAndLeftIsSpecChar(int row, int col) => char.IsNumber(_fileInput[row + 1][col - 1]) ? Direction.DownAndLeft : null;
    private Direction? DownIsSpecChar(int row, int col) => char.IsNumber(_fileInput[row + 1][col]) ? Direction.Down : null;
    private Direction? DownAndRightIsSpecChar(int row, int col) => char.IsNumber(_fileInput[row + 1][col + 1]) ? Direction.DownAndRight : null;

    private List<Direction> GetDirectionsOfAdjacentDigits(int row, int col) => ((List<Func<int, int, Direction?>>)
    [
        row - 1 >= 0 && col - 1 >= 0 ? UpAndLeftIsSpecChar : NoOp,
        row - 1 >= 0 ? UpIsSpecChar : NoOp,
        row - 1 >= 0 && col + 1 <= _rightBoundary ? UpAndRightIsSpecChar : NoOp,
        col - 1 >= 0 ? LeftIsSpecChar : NoOp,
        col + 1 <= _rightBoundary ? RightIsSpecChar : NoOp,
        row + 1 <= _bottomBoundary && col - 1 >= 0 ? DownAndLeftIsSpecChar : NoOp,
        row + 1 <= _bottomBoundary ? DownIsSpecChar : NoOp,
        row + 1 <= _bottomBoundary && col + 1 <= _rightBoundary ? DownAndRightIsSpecChar : NoOp
    ])
        .Select(func => func(row, col))
        .Where(resp => resp.HasValue)
        .Select(resp => resp.Value)
        .ToList();

    private static (int, int) GetStartingCoordsForNumSearch(Direction direction, int startingRow, int startingCol) => direction switch
    {
        Direction.UpAndLeft => (startingRow - 1, startingCol - 1),
        Direction.Up => (startingRow - 1, startingCol),
        Direction.UpAndRight => (startingRow - 1, startingCol + 1),
        Direction.Left => (startingRow, startingCol - 1),
        Direction.Right => (startingRow, startingCol + 1),
        Direction.DownAndLeft => (startingRow + 1, startingCol - 1),
        Direction.Down => (startingRow + 1, startingCol),
        Direction.DownAndRight => (startingRow + 1, startingCol + 1),
        _ => throw new NotImplementedException()
    };
}