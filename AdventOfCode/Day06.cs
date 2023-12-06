using MoreLinq;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly List<(long time, long record)> _input;    

    public Day06()
    {
        //_input = new() { (7, 9), (15, 40), (30,200) };
        //_input = new() { (38, 241), (94, 1549), (79, 1074), (70, 1091) };
        //_input = new() { (71530, 940200) };
        _input = new() { (38947970, 241154910741091) };
       
    }

    public override ValueTask<string> Solve_1()
    {
        long result = 1;
        foreach(var race in _input)
        {
            long nbWay = Race(race.time).Count(r => r > race.record);
            result *=  nbWay > 0 ? nbWay : 1;
        }
        return new(result.ToString());
    }    

    private IEnumerable<long> Race(long time)
    {
        for (var i = 0; i <= time; i++)
        {
            yield return (time - i) * i;
        }
    }

    public override ValueTask<string> Solve_2()
    {
        long result = 1;
        foreach (var race in _input)
        {
            long nbWay = Race(race.time).Count(r => r > race.record);
            result *= nbWay > 0 ? nbWay : 1;
        }
        return new(result.ToString());
    }  
 }
