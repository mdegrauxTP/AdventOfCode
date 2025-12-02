using MoreLinq;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AdventOfCode2023;

public class Day10 : BaseDay
{
    private readonly char[][] _input;
    private readonly List<Position> path = new();
    private readonly Dictionary<string, List<char>> invalidVoisin = new();
    private readonly Dictionary<string, List<char>> invalidTravel = new();
    public Day10()
    {
        _input = File.ReadAllLines(InputFilePath).Select(l => l.ToCharArray()).ToArray();
        invalidVoisin.Add("down", new() { '.', '7', '-', 'F' });        
        invalidVoisin.Add("left", new() { '.', 'J', '|', '7' });
        invalidVoisin.Add("right", new() { '.', 'F', '|', 'L' });
        invalidVoisin.Add("up", new() { '.', '-', 'L', 'J' });
    }
    public override ValueTask<string> Solve_1()
    {
        var startingPoint = _input.SelectMany((row, rowIndex) => row.Select((ch, colIndex) => new Position(new Point(rowIndex, colIndex), ch)))
                               .FirstOrDefault(item => item.Char == 'S');

        var currentPosition = startingPoint;
        var comingFrom = "start";
        do
        {
            var result = Travel(currentPosition, comingFrom);
            currentPosition = result.Value.currentPosition;
            comingFrom = result.Value.comingFrom;
        } 
        while (currentPosition.Char != 'S');
        return new((path.Count / 2).ToString());
    }

    private (Position currentPosition, string comingFrom)? Travel(Position currentPosition, string comingFrom)
    {        
        path.Add(currentPosition);
        if (comingFrom != "up" && currentPosition.Char != '-' && currentPosition.Char != '7' && currentPosition.Char != 'F')
        {
            var result = CheckUp(currentPosition.point);
            if (result is not null)
                return result;
        }
        if (comingFrom != "right" && currentPosition.Char != '|' && currentPosition.Char != 'J' && currentPosition.Char != '7')
        {
            var result = CheckRight(currentPosition.point);
            if (result is not null)
                return result;
        }
        if (comingFrom != "down" && currentPosition.Char != '-' && currentPosition.Char != 'L' && currentPosition.Char != 'J')
        {
            var result = CheckDown(currentPosition.point);
            if (result is not null)
                return result;
        }
        if (comingFrom != "left" && currentPosition.Char != '|' && currentPosition.Char != 'L' && currentPosition.Char != 'F')
        {
            var result = CheckLeft(currentPosition.point);
            if (result is not null)
                return result;
        }

        return null;
    }

    private (Position?, string comingFrom)? CheckLeft(Point currentPosition)
    {
        if (currentPosition.Y - 1 < 0) return null;
        var charVoisin = _input[currentPosition.X][currentPosition.Y-1];
        if (!CanMove("left", charVoisin));
        return (new Position(new Point(currentPosition.X, currentPosition.Y-1), charVoisin), "right");
    }

    private (Position?, string comingFrom)? CheckDown(Point currentPosition)
    {
        if (currentPosition.X + 1 > _input.Length) return null;
        var charVoisin = _input[currentPosition.X+1][currentPosition.Y];
        if (!CanMove("down", charVoisin)) return null;
        return (new Position(new Point(currentPosition.X+1, currentPosition.Y), charVoisin), "up");
    }

    private bool CanMove(string direction, char charVoisin)
    {
        return !invalidVoisin[direction].Contains(charVoisin);
    }

    private (Position?, string comingFrom)? CheckRight(Point currentPosition)
    {
        if (currentPosition.Y + 1 > _input[currentPosition.X].Length) return null;
        var charVoisin = _input[currentPosition.X][currentPosition.Y+1];
        if (!CanMove("right", charVoisin));
        return (new Position(new Point(currentPosition.X, currentPosition.Y +1), charVoisin), "left");
    }

    private (Position, string comingFrom)? CheckUp(Point currentPosition)
    {
        if (currentPosition.X - 1 < 0) return null;
        var charVoisin = _input[currentPosition.X-1][currentPosition.Y];
        if (!CanMove("up", charVoisin)) ;
        return (new Position(new Point(currentPosition.X - 1, currentPosition.Y), charVoisin), "down");
    }

    public record Position(Point point, char Char);

    public override ValueTask<string> Solve_2()
    {
        path.Clear();
        Solve_1();
        int count = 0;
        var polygon = path.Select(x => new Point(x.point.Y, x.point.X)).ToArray();
        var points = _input.SelectMany((row, rowIndex) => row.Select((ch, colIndex) => new Position(new Point(colIndex, rowIndex), ch)));
        var graphicsPath = new GraphicsPath();
        graphicsPath.AddPolygon(polygon);
        var region = new Region(graphicsPath);
        foreach (var p in points.Where(p => !polygon.Contains(p.point)))
        {
            count += IsPointInsidePolygon(region, new Point(p.point.X, p.point.Y)) ? 1 : 0;
        }

        return new(count.ToString());
    }

    static bool IsPointInsidePolygon(Region region, Point point)
    {        
        return region.IsVisible(point);
    }

}