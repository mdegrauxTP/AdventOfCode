using Spectre.Console.Rendering;
using System.Text.RegularExpressions;
using MoreLinq;
using System.Drawing;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly List<string> _input;


    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath).Select(l => l).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        string pattern = @"(\d+)";
        var result = 0;
        foreach(var line in _input)
        {
            var winningNumber = Regex.Matches(line.Split(": ")[1].Split("|")[0], pattern).Cast<Match>().Select(x => int.Parse(x.Value));
            var numberYouHave = Regex.Matches(line.Split(": ")[1].Split("|")[1], pattern).Cast<Match>().Select(x => int.Parse(x.Value));
            var matchingNumber = winningNumber.Intersect(numberYouHave).ToList();
            result += matchingNumber.Count < 0 ? 0 : (int)Math.Pow(2, (matchingNumber.Count - 1));
        }
        
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        string pattern = @"(\d+)";
        string cardPattern = @"Card\s+(\d+):";        
        Dictionary<int, int> scratchcards = new Dictionary<int, int>();
        foreach (var n in Enumerable.Range(1, _input.Count))
        {
            scratchcards.Add(n, 1);
        }
        foreach (var line in _input)
        {
            var card = int.Parse(Regex.Match(line, cardPattern).Groups[1].Value);
            var winningNumber = Regex.Matches(line.Split(": ")[1].Split("|")[0], pattern).Cast<Match>().Select(x => int.Parse(x.Value));
            var numberYouHave = Regex.Matches(line.Split(": ")[1].Split("|")[1], pattern).Cast<Match>().Select(x => int.Parse(x.Value));
            var matchingNumber = winningNumber.Intersect(numberYouHave).ToList();
            for(int i = 1; i < matchingNumber.Count + 1; i++)
            {                
                if(card + i <= _input.Count)
                    scratchcards[card + i] += 1 * scratchcards[card];                           
            }

            if (matchingNumber.Count == 0 && !scratchcards.Any(x => x.Key > card && x.Value > 0))
                break;
        }

        var result = scratchcards.Values.Sum();
        return new(result.ToString());
    }  
 }
