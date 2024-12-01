using AdventOfCode.Extension;
using MoreLinq;
using System.Linq;
using System.Text;

namespace AdventOfCode2023;

public class Day14 : BaseDay
{
    private readonly string _input;
    public Day14()
    {
        _input = File.ReadAllText(InputFilePath);
    }
    public override ValueTask<string> Solve_1()
    {
        var count = 0;
        var columns = _input.SplitIntoColumns();
        foreach(var column in columns)
        {
            var splits = column.Split('#');
            var length = 0;
            foreach(var split in splits)
            {                            
                count += Enumerable.Range(0, split.Count(x => x == 'O')).Select(x => columns.Length - x - length).Sum();
                length += split.Length + 1; //1 for the square rock
            }
        }
        return new(count.ToString());
    }    
    
    public override ValueTask<string> Solve_2()
    {       
        return new("");
    }   
}