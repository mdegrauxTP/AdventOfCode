using MoreLinq;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using static AdventOfCode.Day10;

namespace AdventOfCode;

public class Day12 : BaseDay
{
    private List<(string Springs, List<int> Occurences)> _input;
    private List<(string Springs, List<int> Occurences)> _inputP2;

    public Day12()
    {
        _input = File.ReadAllLines(InputFilePath)
                     .Select(line =>
                     {
                         var split = line.Split(" ");
                         var springs = split[0];
                         var occurences = split[1].Split(",").Select(int.Parse).ToList();
                         return (springs, occurences);
                     }).ToList();

        _inputP2 = File.ReadAllLines(InputFilePath)
                     .Select(line =>
                     {
                         var split = line.Split(" ");
                         var springs = string.Join("?", Enumerable.Repeat(split[0], 5));
                         var occurences = string.Join(",", Enumerable.Repeat(split[1], 5)).Split(",").Select(int.Parse).ToList();
                         return (springs, occurences);
                     }).ToList();

    }
    public override ValueTask<string> Solve_1()
    {
        int count = 0;
        foreach(var line in _input)
        {
            var indexes = line.Springs.Select((c, i) => new { Char = c, Index = i })
                                .Where(ci => ci.Char == '?')
                                .Select(ci => ci.Index)
                                .ToList();
            var possibilities = indexes.Subsets();                        
            foreach(var possibility in possibilities)
            {
                StringBuilder workingString = new(line.Springs);
                foreach (var index in possibility)
                {
                    ReplaceCharAtIndex(workingString, '#', index);
                }

                var lengths = string.Join("", workingString.Replace('?', '.')).Split('.').Where(s => !string.IsNullOrEmpty(s)).Select(s => s.Length);
                if(lengths.SequenceEqual(line.Occurences))
                    count++;
            }
        }
        return new("".ToString());
    }
    static void ReplaceCharAtIndex(StringBuilder stringBuilder, char replacementChar, int index)
    {
        if (index >= 0 && index < stringBuilder.Length)
        {
            stringBuilder[index] = replacementChar;
        }
    }

    public override ValueTask<string> Solve_2()
    {
        int count = 0;
        foreach (var line in _inputP2)
        {
            var indexes = line.Springs.Select((c, i) => new { Char = c, Index = i })
                                .Where(ci => ci.Char == '?')
                                .Select(ci => ci.Index)
                                .ToList();
            var possibilities = indexes.Subsets();
            foreach (var possibility in possibilities)
            {
                StringBuilder workingString = new(line.Springs);
                foreach (var index in possibility)
                {
                    ReplaceCharAtIndex(workingString, '#', index);
                }

                var lengths = string.Join("", workingString.Replace('?', '.')).Split('.').Where(s => !string.IsNullOrEmpty(s)).Select(s => s.Length);
                if (lengths.SequenceEqual(line.Occurences))
                    count++;
            }
        }
        return new("".ToString());
    }    
}