using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using static AdventOfCode._2025.Day02;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode._2025;

public class Day05
{

    private readonly string[] _input;
    public record Range(BigInteger Start, BigInteger End);
    public List<Range> ranges = new();
    public List<BigInteger> ingredients = new();


    public Day05()
    {
        _input = File.ReadAllLines("C:\\Users\\mdegraux\\source\\repos\\mdegrauxTP\\AdventOfCode\\AdventOfCode\\Inputs\\2025\\Day5.txt");
        ranges = _input.Where(x => x.Contains('-')).Select(x =>
            {
                var p = x.Split('-');
                return new Range(Start: BigInteger.Parse(p[0]), End: BigInteger.Parse(p[1]));
            })
        .ToList();

        ingredients = _input.Where(x => !x.Contains('-') && x != "")
                                .Select(BigInteger.Parse)
                                .ToList();
    }

    public ValueTask<string> Solve_1()
    {
        var result = 0;
        foreach (var ingredient in ingredients)
        {
            foreach (var range in ranges)
            {
                if (ingredient >= range.Start && ingredient <= range.End)
                {
                    result++;
                    break;
                }
            }
        }
        return new(result.ToString());
    }


    public ValueTask<string> Solve_2()
    {
        var merged = MergeOverlappingRange(ranges);

        BigInteger result = 0;
        foreach(var range in merged)
        {
            result += range.End - range.Start + 1;
        }

        return new(result.ToString());
    }

    public static List<Range> MergeOverlappingRange(List<Range> ranges)
    {
        var output = new List<Range>();

        var sortedRange = ranges.OrderBy(x => x.Start).ToList();
        var current = sortedRange[0];

        for (int i = 1; i < sortedRange.Count; i++)
        {
            var next = sortedRange[i];

            if(next.Start <= current.End)
            {
                current = current with { End = Max(current.End, next.End) };
            }
            else
            {
                output.Add(current);
                current = next;
            }
        }

        output.Add(current);

        return output;
    }

    public static BigInteger Max(BigInteger x, BigInteger y)
    {
        return x > y ? x : y;
    }

}