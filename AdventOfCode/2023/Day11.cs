using MoreLinq;
using System.Drawing;
using static AdventOfCode2023.Day10;

namespace AdventOfCode2023;

public class Day11 : BaseDay
{
    private IEnumerable<IEnumerable<char>> _input;
    private const int multiplicateur = 1000000;

    public Day11()
    {
        _input = File.ReadAllLines(InputFilePath).Select(l => l.ToCharArray()).ToArray();
    }
    public override ValueTask<string> Solve_1()
    {
        _input = ExpandUnivers(_input, 1);
        var reverse = _input.Transpose();
        _input = ExpandUnivers(reverse, 1);
        reverse = _input.Transpose();

        var galaxies = GetHashPositions(reverse);

        var x = GenerateCombinations(galaxies).Sum(x => (Math.Abs(x.a.X - x.b.X) + Math.Abs(x.a.Y - x.b.Y)));
        return new(x.ToString());
    }

    static IEnumerable<(Point a, Point b)> GenerateCombinations(IEnumerable<Point> input)
    {
        return input
            .SelectMany((point1, index1) =>
                input.Skip(index1 + 1)
                    .Select(point2 => (point1, point2)))
            .ToList();
    }

    private IEnumerable<IEnumerable<char>> ExpandUnivers(IEnumerable<IEnumerable<char>> univers, int multiplicator)
    {
        var inputList = univers.ToList();
        var spaceToExpand = univers.Select((l, index) => (l, index)).Where(l => l.l.All(c => c == '.')).ToList();
        int correction = 0;
        foreach (var space in spaceToExpand)
        {
            for (var i = 1; i < multiplicator; i++)
            {
                inputList.Insert(space.index + ++correction, space.l);
            }
        }
        return inputList;
    }

    static IEnumerable<Point> GetHashPositions(IEnumerable<IEnumerable<char>> grid)
    {
        var positions = new List<Point>();

        var rowIndex = 0;
        foreach (var row in grid)
        {
            var columnIndex = 0;
            foreach (var cell in row)
            {
                if (cell == '#')
                {
                    positions.Add(new Point(columnIndex, rowIndex));
                }
                columnIndex++;
            }
            rowIndex++;
        }

        return positions;
    }


    public override ValueTask<string> Solve_2()
    {
        _input = File.ReadAllLines(InputFilePath).Select(l => l.ToCharArray()).ToArray();
        var rowToExpand = _input.Select((l, index) => (l, index)).Where(l => l.l.All(c => c == '.')).ToList();
        var columnToExpand = _input.Transpose().Select((l, index) => (l, index)).Where(l => l.l.All(c => c == '.')).ToList();

        var galaxies = GetHashPositions(_input);

        var x = GenerateCombinations(galaxies).Sum(x => (Math.Abs(x.a.X - x.b.X) + Math.Abs(x.a.Y - x.b.Y)) + Expansion(x, rowToExpand, columnToExpand));
        return new(x.ToString());
    }

    private long Expansion((Point a, Point b) x, List<(IEnumerable<char> l, int index)> rowToExpand, List<(IEnumerable<char> l, int index)> columnToExpand)
    {
        long expansionCount = 0;
        foreach(var row in rowToExpand)
        {
            if(x.a.Y < row.index && x.b.Y > row.index
                || x.a.Y > row.index && x.b.Y < row.index)
            {
                expansionCount += multiplicateur - 1;
            }          
        }
        foreach(var column in columnToExpand)
        {
            if(x.a.X < column.index && x.b.X > column.index
                || x.a.X > column.index && x.b.X < column.index)
            {
                expansionCount += multiplicateur - 1;
            }
        }

        return expansionCount;
    }
}