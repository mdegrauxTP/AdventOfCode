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
        string pattern = @"([0-9])";
        var matches = _input.Select(l => Regex.Matches(l, pattern));
        var result = matches.Select(m => int.Parse(m.First().ToString() + m.Last().ToString())).Sum();
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        string pattern = @"([0-9]|one|two|three|four|five|six|seven|eight|nine)";
        string patternBack = @"([0-9]|eno|owt|eerht|ruof|evif|xis|neves|thgie|enin)";

        int count = 0;
        foreach(var line in _input)
        {
            var first = Regex.Match(line, pattern);
            var last = Regex.Match(new string(line.Reverse().ToArray()), patternBack);
            count += int.Parse(Convert(first.ToString()) + Convert(last.ToString()));
        }
        return new(count.ToString());
    }

    private string Convert(string digitAsString)
    {
        return digitAsString switch
        {
            "one" or "eno" => "1",
            "two" or "owt" => "2",
            "three" or "eerht" => "3",
            "four" or "ruof" => "4",
            "five" or "evif" => "5",
            "six" or "xis" => "6",
            "seven" or "neves" => "7",
            "eight" or "thgie" => "8",
            "nine" or "enin" => "9",
            _ => digitAsString
        };
    }
 }
