using MoreLinq;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode._2025;

public class Day02
{
   
    private readonly string _input;

    public Day02()
    {
        _input = File.ReadAllLines("C:\\Users\\mdegraux\\source\\repos\\mdegrauxTP\\AdventOfCode\\AdventOfCode\\Inputs\\2025\\Day2.txt").Select(l => l).First();
    }

    public class Range
    {
        public BigInteger Min { get; set; }
        public BigInteger Max { get; set; }
        public Range(BigInteger min, BigInteger max)
        {
            Min = min;
            Max = max;
        }
    }

    public static bool SplitInXMatch(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;
       
        for (int i = 2; i <= input.Length; i++)
        {
            if (input.Length % i != 0)
                continue;

            List<string> values = new();
            var partLenght = input.Length / i;

            for(int j = 0; j < input.Length; j += partLenght)
            {
                int end = Math.Min(j + partLenght, input.Length);
                values.Add(input[j..end]);
            }

            if(values.Distinct().Count() == 1) return true;
        }

        return false;
    }
    public static bool SplitInTwoMatch(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        if (input.Length % 2 != 0)
            return false;

        int mid = input.Length / 2;

        string part1 = input[..mid];
        string part2 = input[mid..];

        return part1 == part2;
    }



    public ValueTask<string> Solve_1()
    {
        List<Range> ranges = _input.Split(',').Select(r =>
        {
            var bounds = r.Split('-').Select(BigInteger.Parse).ToArray();
            return new Range(bounds[0], bounds[1]);
        }).ToList();

        BigInteger total = 0;
        foreach(var range in ranges)
        {
            BigInteger subtotal = 0;

            foreach (BigInteger x in BigIntegerExtensions.Range(range.Min, range.Max - range.Min + 1))
            {
                if (SplitInTwoMatch(x.ToString()))
                    subtotal += x;
            }

            total += subtotal;
        }


        return new(total.ToString());
    }

    public static class BigIntegerExtensions
    {
        public static IEnumerable<BigInteger> Range(BigInteger start, BigInteger count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            for (BigInteger i = 0; i < count; i++)
                yield return start + i;
        }
    }


    public ValueTask<string> Solve_2()
    {

        List<Range> ranges = _input.Split(',').Select(r =>
        {
            var bounds = r.Split('-').Select(BigInteger.Parse).ToArray();
            return new Range(bounds[0], bounds[1]);
        }).ToList();

        BigInteger total = 0;
        foreach (var range in ranges)
        {
            BigInteger subtotal = 0;

            foreach (BigInteger x in BigIntegerExtensions.Range(range.Min, range.Max - range.Min + 1))
            {
                if (SplitInXMatch(x.ToString()))
                    subtotal += x;
            }

            total += subtotal;
        }


        return new(total.ToString());
    }
 }
