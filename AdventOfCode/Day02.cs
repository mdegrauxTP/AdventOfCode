using Spectre.Console.Rendering;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly List<string> _input;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath).Select(l => l).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        int result = 0;
        foreach(var line in _input)
        {
            bool impossible = false;
            var gameAndSet = line.Split(":");
            var game = int.Parse(gameAndSet[0].Split(" ")[1]);
            var sets = gameAndSet[1].Split(";");
            foreach(var set in sets)
            {
                var tirages = set.Split(',');
                foreach(var tirage in tirages)
                {
                    var number = int.Parse(tirage.Split(' ')[1]);
                    var color = tirage.Split(' ')[2];
                    if (!isPossible(number, color))
                    {
                        impossible = true;
                        break;
                    }

                    if (impossible) break;
                }
            }

            if(!impossible) result += game;
        }
        return new(result.ToString());
    }

    private bool isPossible(int number, string color)
    {
        return (color == "red" && number <= 12) ||
               (color == "green" && number <= 13) ||
               (color == "blue" && number <= 14);
    }

    public override ValueTask<string> Solve_2()
    {
        int result = 0;
        foreach (var line in _input)
        {
            Dictionary<string, int> minValueByColor = new();
            var gameAndSet = line.Split(":");
            var game = int.Parse(gameAndSet[0].Split(" ")[1]);
            var sets = gameAndSet[1].Split(";");
            foreach (var set in sets)
            {
                var tirages = set.Split(',');
                foreach (var tirage in tirages)
                {
                    var number = int.Parse(tirage.Split(' ')[1]);
                    var color = tirage.Split(' ')[2];
                    if (minValueByColor.ContainsKey(color))
                    {
                        minValueByColor[color] = Math.Max(minValueByColor[color], number);
                    }
                    else
                    {
                        minValueByColor.Add(color, number);
                    }
                }
            }

            result += (minValueByColor["green"] * minValueByColor["blue"] * minValueByColor["red"]);
        }
        return new(result.ToString());
    }  
 }
