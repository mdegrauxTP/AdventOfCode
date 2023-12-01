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
        //string pattern = @"([0-9])";
        //var matches = _input.Select(l => Regex.Matches(l, pattern));
        //var result = matches.Select(m => int.Parse(m.First().ToString() + m.Last().ToString())).Sum();
        //return new(result.ToString());
        return new("");
    }

    public override ValueTask<string> Solve_2()
    {
        string pattern = @"([0-9]|one|two|three|four|five|six|seven|eight|nine)";
        string patternBack = @"([0-9]|eno|owt|eerht|ruof|evif|xis|neves|thgie|enin)";
        //var matches = _input.Select(l => Regex.Matches(l, pattern).Cast<Match>().Select(x => Convert(x.Value)));
//        var result = matches.Select(m => int.Parse(m.First().ToString() + m.Last().ToString())).Sum();

        int count = 0;
        foreach(var line in _input)
        {
            var first = Regex.Match(line, pattern);
            var last = Regex.Match(new string(line.Reverse().ToArray()), patternBack);
            count += int.Parse(Convert(first.ToString()) + ConvertBackward(last.ToString()));
        }
        return new(count.ToString());
    }

    private string Convert(string digitAsString)
    {
        return digitAsString switch
        {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
            _ => digitAsString
        };
    }

    private string ConvertBackward(string digitAsString)
    {
        return digitAsString switch
        {
            "eno" => "1",
            "owt" => "2",
            "eerht" => "3",
            "ruof" => "4",
            "evif" => "5",
            "xis" => "6",
            "neves" => "7",
            "thgie" => "8",
            "enin" => "9",
            _ => digitAsString
        };
    }
}
