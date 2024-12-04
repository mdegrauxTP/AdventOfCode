using MoreLinq;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        string pattern = @"mul\((\d+),(\d+)\)";
        var result = Regex.Matches(_input, pattern).Cast<Match>()
                                                   .Select(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value))
                                                   .Sum();
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        string patternMult = @"mul\((\d+),(\d+)\)";
        var mulMatches = Regex.Matches(_input, patternMult).Cast<Match>()
                                                   .Select(match => (match.Index, Value: int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value)));

        string patternInstructionDont = @"don\'t\(\)";
        var instructionMatchDont = Regex.Matches(_input, patternInstructionDont).Cast<Match>()
                                                                                .Select(match => match.Index)
                                                                                .OrderBy(index => index)
                                                                                .ToList();

        string patternInstructionDo = @"do\(\)";
        List<int> instructionMatchDo = [-1];
        instructionMatchDo.AddRange(Regex.Matches(_input, patternInstructionDo).Cast<Match>()
                                                                            .Select(match => match.Index)
                                                                            .OrderBy(index => index)
                                                                            .ToList());

        int result = default;
       
        foreach(var mulMatch in mulMatches)
        {
            var index = mulMatch.Index;
            var closestDo = instructionMatchDo.Where(value => value < index)?.LastOrDefault(int.MinValue);
            var closestDont = instructionMatchDont.Where(value => value < index)?.LastOrDefault(int.MinValue);

            if (closestDo > closestDont) 
                result += mulMatch.Value;
        }

        return new(result.ToString());
    }       
}
