using MoreLinq;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly List<string> _input;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath).Select(l => l).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var firstList = _input.Select(l => int.Parse(l.Split(" ")[0])).OrderBy(x => x);
        var secondList = _input.Select(l => int.Parse(l.Split("   ")[1])).OrderBy(x => x);

        var combinedList = firstList.Zip(secondList);
        var totalDistance = combinedList.Sum(x => Math.Abs(x.First - x.Second));

        return new(totalDistance.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var firstList = _input.Select(l => int.Parse(l.Split(" ")[0])).OrderBy(x => x);
        var secondList = _input.Select(l => int.Parse(l.Split("   ")[1])).OrderBy(x => x);

        var count = firstList.Sum(number => number * secondList.Count(n2 => n2 == number));
        return new(count.ToString());
    }
 }
