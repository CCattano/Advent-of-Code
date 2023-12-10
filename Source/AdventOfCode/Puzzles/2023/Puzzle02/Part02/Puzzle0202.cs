using AdventOfCode.Infrastructure;

namespace AdventOfCode.Puzzles._2023.Puzzle02.Part02;

public class Puzzle0202(int year) : BasePuzzle(year)
{
    /// <inheritdoc />
    protected override int Day => 2;

    /// <inheritdoc />
    protected override int Part => 2;

    /// <inheritdoc />
    public override void Solve()
    {
        int sumOfGameNumbers = base.GetPuzzleInput()
            .Select(line => line
                .Split(':')
                .Last()
                .Trim()
                .Replace(';', ',')
                .Split(',')
                .Select(cubeData => new CubeData(cubeData))
                .GroupBy(cubeData => cubeData.Color)
                .Select(group => group.OrderByDescending(cube => cube.Quantity).First().Quantity)
                .Aggregate(1, (acc, curr) => acc * curr))
            .Sum();

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