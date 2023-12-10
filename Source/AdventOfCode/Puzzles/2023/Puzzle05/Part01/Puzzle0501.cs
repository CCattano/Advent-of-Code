using AdventOfCode.Infrastructure;

namespace AdventOfCode.Puzzles._2023.Puzzle05.Part01;

public class Puzzle0501 : BasePuzzle
{
    /// <inheritdoc />
    protected override int Day => 5;

    /// <inheritdoc />
    protected override int Part => 1;

    private readonly List<string> _fileInput;
    private readonly List<Func<long, long>> _maps;

    public Puzzle0501(int year) : base(year)
    {
        _fileInput = base.GetPuzzleInput();
        _maps =
        [
            new Map(_fileInput, "seed-to-soil map").MapEntry,
            new Map(_fileInput, "soil-to-fertilizer map").MapEntry,
            new Map(_fileInput, "fertilizer-to-water map").MapEntry,
            new Map(_fileInput, "water-to-light map").MapEntry,
            new Map(_fileInput, "light-to-temperature map").MapEntry,
            new Map(_fileInput, "temperature-to-humidity map").MapEntry,
            new Map(_fileInput, "humidity-to-location map").MapEntry
        ];
    }

    /// <inheritdoc />
    public override void Solve()
    {
        IEnumerable<long> seeds = _fileInput.First().Split(':').Last().Trim().Split(' ').Select(long.Parse);
        IEnumerable<long> locations = seeds.Select(seed => _maps.Aggregate(seed, (item, map) => map(item)));
        Console.WriteLine(base.GetFormattedSolutionString(locations.Min()));
    }
}

internal class MapBoundary(long lower, long upper)
{
    public readonly long Lower = lower;
    public bool IsInRange(long input) => input >= Lower && input <= upper;
}

internal class Map
{
    private readonly Dictionary<MapBoundary, MapBoundary> _maps = new();

    public Map(List<string> fileInput, string mapKey)
    {
        int mapIdx = fileInput.FindIndex(line => line.StartsWith(mapKey));
        string mapLine = fileInput[++mapIdx];
        while (!string.IsNullOrWhiteSpace(mapLine))
        {
            string[] mapParts = mapLine.Split(' ');
            long destinationRangeStart = long.Parse(mapParts.First());
            long sourceRangeStart = long.Parse(mapParts[1]);
            long range = long.Parse(mapParts.Last());

            MapBoundary srcBoundaries = new(sourceRangeStart, sourceRangeStart + range);
            MapBoundary destBoundaries = new(destinationRangeStart, destinationRangeStart + range);
            _maps.Add(srcBoundaries, destBoundaries);
            
            mapLine = ++mapIdx <= fileInput.Count - 1 ? fileInput[mapIdx] : null;
        }
    }

    public long MapEntry(long input)
    {
        int mapIdx = _maps.Keys.ToList().FindIndex(src => src.IsInRange(input));

        if (mapIdx == -1)
            return input;

        (MapBoundary srcBoundary, MapBoundary destBoundary) = _maps.ElementAt(mapIdx);

        long idxOfSrc = input - srcBoundary.Lower;
        long destVal = destBoundary.Lower + idxOfSrc;
        
        return destVal;
    }
}