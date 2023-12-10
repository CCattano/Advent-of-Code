using AdventOfCode.Infrastructure;

namespace AdventOfCode.Puzzles._2023.Puzzle02.Part01;

public class Puzzle0201(int year) : BasePuzzle(year)
{
    /// <inheritdoc />
    protected override int Day => 2;
    
    /// <inheritdoc />
    protected override int Part => 1;

    private const int RedLimit = 12;
    private const int GreenLimit = 13;
    private const int BlueLimit = 14;

    /// <inheritdoc />
    public override void Solve()
    {
        List<string> fileLines = base.GetPuzzleInput();

        int sumOfGameNumbers = fileLines.Select(line =>
        {
            string[] gameAndSubsets = line.Split(':');
            int gameNumber = int.Parse(gameAndSubsets.First().Split(" ").Last());
            return gameAndSubsets
                .Last()
                .Trim()
                .Replace(';', ',')
                .Split(',')
                .Select(cubeData => new CubeData(cubeData))
                .FirstOrDefault(cube => cube.Color switch
                {
                    Colors.Red => cube.Quantity > RedLimit,
                    Colors.Green => cube.Quantity > GreenLimit,
                    Colors.Blue => cube.Quantity > BlueLimit,
                    _ => throw new ArgumentOutOfRangeException()
                }) is not null 
                ? 0
                : gameNumber;
        }).Sum();

        Console.WriteLine(base.GetFormattedSolutionString(sumOfGameNumbers));
    }
}

internal enum Colors
{
    Red,
    Green,
    Blue
}

internal class CubeData
{
    public readonly int Quantity;
    public readonly Colors Color;

    public CubeData(string data)
    {
        string[] qtyAndColor = data.Trim().Split(' ');
        Quantity = int.Parse(qtyAndColor.First());
        Color = qtyAndColor.Last() switch
        {
            "red" => Colors.Red,
            "green" => Colors.Green,
            "blue" => Colors.Blue,
            _ => throw new NotImplementedException()
        };
    }
}