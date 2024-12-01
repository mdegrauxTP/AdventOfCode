using AdventOfCode.Extension;
using MoreLinq;
using System.Linq;
using System.Text;

namespace AdventOfCode2023;

public class Day13 : BaseDay
{
    private readonly List<string> _input;
    private readonly HashSet<(int, int)> initialMiroir = new();
    Dictionary<int, (int num, bool isRow)> initialMirrors = new();
    public Day13()
    {
        _input = File.ReadAllText(InputFilePath).Split(new[] { "\r\n\r\n", "\r\r", "\n\n" }, StringSplitOptions.None)
                                                .Where(s => !string.IsNullOrWhiteSpace(s))
                                                .Select(s => s.Trim())
                                                .ToList();
    }
    public override ValueTask<string> Solve_1()
    {
        var result = 0;
        foreach(var (pattern, index) in _input.Select((p, i) => (p,i)))
        {
            result += FindMirror(pattern, index);
        }
        return new("");
    }

    private int FindMirror(string pattern, int index)
    {
        var rows = pattern.Split("\r\n");
        for (int i = 1; i < rows.Count(); i++)
        {
            bool isMatch = rows.Take(i).Reverse().Zip(rows.Skip(i)).All(c => c.First == c.Second);
            if (isMatch)
            {
                if (initialMiroir.Contains((index, i * 100)))
                {
                    continue;
                }

                initialMiroir.Add((index, i * 100));
                return i * 100;
            }               
        }

        var columns = pattern.SplitIntoColumns();
        for (int i = 1; i < columns.Count(); i++)
        {
            bool isMatch = columns.Take(i).Reverse().Zip(columns.Skip(i)).All(c => c.First == c.Second);
            if (isMatch)
            {
                if (initialMiroir.Contains((index, i)))
                {
                    continue;
                }

                initialMiroir.Add((index, i));
                return i;
            }
                
        }

        return 0;
    }
    
    public override ValueTask<string> Solve_2()
    {
        var result = 0;
        foreach (var (pattern, index) in _input.Select((p, i) => (p, i)))
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                if (!".#".Contains(pattern[i])) continue;
                StringBuilder sb = new StringBuilder(pattern);
                sb[i] = sb[i] == '.' ? '#' : '.';

                int reflectionLine = FindMirror(sb.ToString(), index);
                if (reflectionLine != 0)
                {
                    result += reflectionLine;
                    break;
                }
            }
        }
        return new("");

        
    }

    private bool TryFindReflection(string block, int Id, out int result)
    {
        var asRows = block.SplitByNewline();
        var asColumns = block.SplitIntoColumns().ToList();
        //Check rows
        for (int i = 1; i < asRows.Count; i++)
        {
            if (asRows.Take(i).Reverse().Zip(asRows.Skip(i)).All(x => x.First == x.Second))
            {
                if (initialMirrors.TryGetValue(Id, out (int num, bool isRow) x))
                {
                    if (x.isRow && x.num == i) continue;
                }
                initialMirrors[Id] = (i, true);
                result = i * 100;
                return true;
            }
        }

        for (int i = 1; i < asColumns.Count; i++)
        {
            if (asColumns.Take(i).Reverse().Zip(asColumns.Skip(i)).All(x => x.First == x.Second))
            {
                if (initialMirrors.TryGetValue(Id, out (int num, bool isRow) x))
                {
                    if (!x.isRow && x.num == i) 
                        continue;

                }
                initialMirrors[Id] = (i, false);
                result = i;
                return true;
            }
        }

        result = 0;
        return false;
    }
}