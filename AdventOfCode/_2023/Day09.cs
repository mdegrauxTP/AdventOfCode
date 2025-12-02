using MoreLinq;

namespace AdventOfCode2023;

public class Day09 : BaseDay
{
    private readonly List<int[]> _input;

    public Day09()
    {
        _input = File.ReadAllLines(InputFilePath).Select(l => l.Split(" ").Select(int.Parse).ToArray()).ToList();
    }
    public override ValueTask<string> Solve_1()
    {
        var result = 0;
        foreach(var line in _input)
        {
            List<int[]> sets = new() { line };
            int[] current = line;            
            do
            {
                List<int> subset = new();
                for (int i = 0; i < current.Length - 1; i++)
                {
                    subset.Add(current[i+1] - current[i]);
                }

                current = subset.ToArray();
                sets.Add(subset.ToArray());
            }
            while (current.Any(x => x != 0));

            result += sets.Select(s => s.Last()).Sum();
        }
        return new(result.ToString());
    }    

    public override ValueTask<string> Solve_2()
    {
        var result = 0;
        foreach (var line in _input)
        {
            List<int[]> sets = new() { line };
            int[] current = line;
            do
            {
                List<int> subset = new();
                for (int i = 0; i < current.Length - 1; i++)
                {
                    subset.Add(current[i + 1] - current[i]);
                }

                current = subset.ToArray();
                sets.Add(subset.ToArray());
            }
            while (current.Any(x => x != 0));

            sets.Reverse();

            for(var i = 0; i < sets.Count; i++)
            {
                var updated = sets[i].ToList();
                if (i == 0)
                {
                    
                    updated.Insert(0, 0);
                    
                } else
                {                    
                    updated.Insert(0,sets[i][0] - sets[i - 1][0]);                   
                }
                sets[i] = updated.ToArray();
            }
            result += sets.Last().First();
        }
        return new(result.ToString());
    }
    
}