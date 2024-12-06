using MoreLinq;
using Spectre.Console;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly List<string> _input;
    private readonly char[][] _grid;

    public Day06()
    {
        static char[][] ConvertToGrid(List<string> stringList)
        {
            return stringList.ConvertAll(str => str.ToCharArray()).ToArray();
        }

        _input = File.ReadAllLines(InputFilePath).Select(l => l).ToList();
        _grid = ConvertToGrid(_input);
    }

    public override ValueTask<string> Solve_1()
    {
        (int x, int y) guardPosition = _grid.FindCharIndex('^');
        (int x, int y) direction = (-1, 0); // on commence par monter
        try
        {
            do
            {
                _grid[guardPosition.x][guardPosition.y] = 'X';
                if (CheckIfNextPositionIsObstruction(guardPosition, direction))
                {
                    RotateRight(ref direction);
                }

                guardPosition.x += direction.x;
                guardPosition.y += direction.y;
            } while (guardPosition.x < _grid.Length -1 && guardPosition.y < _grid[0].Length -1
                  && guardPosition.x > 0 && guardPosition.y > 0);
        } catch (Exception e)
        {

        }

        int result = _grid.Sum(row => row.Count(ch => ch == 'X')) + 1; //+1 pour current pos
        return new(result.ToString());
    }

    private bool CheckIfNextPositionIsObstruction((int x, int y) guardPosition, (int x, int y) currentDirection)
    {
        (int x, int y) nextPotentialGuardPosition = (guardPosition.x + currentDirection.x, guardPosition.y + currentDirection.y);
        return _grid[nextPotentialGuardPosition.x][nextPotentialGuardPosition.y] == '#';
    }

    public override ValueTask<string> Solve_2()
    {
        return new();
    }

    private void RotateRight(ref (int x, int y) direction)
    {
        int tmpX = direction.y;
        int tmpY = -direction.x;

        direction.x = tmpX;
        direction.y = tmpY;
    }
}

public static class gridExtension
{
    public static (int x, int y) FindCharIndex(this char[][] grid, char target)
    {
        for (int row = 0; row < grid.Length; row++)
        {
            for (int col = 0; col < grid[row].Length; col++)
            {
                if (grid[row][col] == target)
                {
                    return (row, col);
                }
            }
        }

        return (0, 0);
    }
}
