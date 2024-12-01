using Spectre.Console.Rendering;
using System.Text.RegularExpressions;
using MoreLinq;
using System.Drawing;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AdventOfCode2023;

public class Day03 : BaseDay
{
    private readonly List<string> _input;
    private readonly char[][] inputAsGrid;
    private Dictionary<Point, List<int>> gears = new();


    public Day03()
    {
        _input = File.ReadAllLines(InputFilePath).Select(l => l).ToList();
        inputAsGrid = _input.Select(l => l.ToArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        string pattern = @"(\d+)+";
        var result = _input.SelectMany((l, index) => Regex.Matches(l, pattern).Cast<Match>().Select(x => (index, x.Index, x.Value)))
                      .Where(n => IsPartNumber(n)).Select(x => int.Parse(x.Value)).Sum();
        
        return new("");
    }

    private bool IsPartNumber((int ligneIndex, int numberIndex, string Value) n)
    {
        bool touchSymbole = false;
        for (int i = 0; i < n.Value.Length; i++)
        {
            touchSymbole = TouchSymbole(n.ligneIndex, n.numberIndex + i, int.Parse(n.Value));
            if (touchSymbole) 
                return true;
        }
        return touchSymbole;
    }

    private bool TouchSymbole(int ligneIndex, int i, int number)
    {
        return IsSymbole(ligneIndex - 1,i - 1, number)
        || IsSymbole(ligneIndex - 1,i, number)
        || IsSymbole(ligneIndex - 1,i + 1, number)
        || IsSymbole(ligneIndex,i - 1, number)
        || IsSymbole(ligneIndex,i + 1, number)
        || IsSymbole(ligneIndex + 1,i - 1, number)
        || IsSymbole(ligneIndex + 1,i, number)
        || IsSymbole(ligneIndex + 1,i + 1, number);              
    }

    private bool IsSymbole(int indexLigne, int indexRow, int currentNumber)
    {
        if (indexLigne < 0 || indexLigne > inputAsGrid.Count() - 1 || indexRow < 0 || indexRow > inputAsGrid.Length - 1)
            return false;
        char c = inputAsGrid[indexLigne][indexRow];
        bool touch =  c != '.' && !int.TryParse(c.ToString(), out _);

        if (c == '*')
        {
            if (!gears.ContainsKey(new Point(indexLigne, indexRow)))
            {
                gears.Add(new Point(indexLigne, indexRow), new List<int>() { currentNumber });
            }
            else
            {
                gears[new Point(indexLigne, indexRow)].Add(currentNumber);
            }
        }
        return touch;
    }

    public override ValueTask<string> Solve_2()
    {
        string pattern = @"(\d+)+";
        _input.SelectMany((l, index) => Regex.Matches(l, pattern).Cast<Match>().Select(x => (index, x.Index, x.Value)))
                      .Where(n => IsPartNumber(n));

        var result = gears.Where(x => x.Value.Count == 2).Select(x => x.Value.First() * x.Value.Last()).Sum();

        return new("");
    }  
 }
