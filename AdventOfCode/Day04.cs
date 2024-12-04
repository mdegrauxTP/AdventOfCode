using MoreLinq;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly List<string> _input;
    private readonly char[][] _grid;

    public Day04()
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
        var x = _input.Select(l => CountOccurrences(l, "XMAS")).Sum();
        var y = _input.Select(l => CountOccurrences(ReverseString(l), "XMAS")).Sum();
        var b = TransposeGrid(_input).Select(l => CountOccurrences(l, "XMAS")).Sum();
        var p = TransposeGrid(_input).Select(l => CountOccurrences(ReverseString(l), "XMAS")).Sum();
        var l = FlattenDiagonals(_input).Select(l => CountOccurrences(l, "XMAS")).Sum();
        var m = FlattenDiagonals(_input).Select(l => CountOccurrences(ReverseString(l), "XMAS")).Sum();
        var u = FlattenAntiDiagonals(_input).Select(l => CountOccurrences(l, "XMAS")).Sum();
        var n = FlattenAntiDiagonals(_input).Select(l => CountOccurrences(ReverseString(l), "XMAS")).Sum();
        var result = x + y + b + p + l + m + u + n;
        return new();
    }

    public override ValueTask<string> Solve_2()
    {
        List<char[][]> patterns = new List<char[][]>(); ;
        char[][] basePattern = new char[][] {
            ['M', '#', 'S'],
            ['#', 'A', '#'],
            ['M', '#', 'S']
        };

        patterns.Add(basePattern);
        var rotatePattern = RotateMatrix90DegreesClockwise(basePattern);
        patterns.Add(rotatePattern);
        rotatePattern = RotateMatrix90DegreesClockwise(rotatePattern);
        patterns.Add(rotatePattern);
        rotatePattern = RotateMatrix90DegreesClockwise(rotatePattern);
        patterns.Add(rotatePattern);

        int count = 0;
        foreach(var pattern in patterns)
        {
            for (int i = 0; i < _grid.Length - 2; i++)
            {
                for (int j = 0; j < _grid.Length - 2; j++)
                {
                    if (_grid[i][j] == pattern[0][0]
                        && _grid[i][j + 2] == pattern[0][2]
                        && _grid[i + 1][j + 1] == pattern[1][1]
                        && _grid[i + 2][j] == pattern[2][0]
                        && _grid[i + 2][j + 2] == pattern[2][2])
                    {
                        count++;
                    }
                        
                }
            }
        }
       
        
        return new(count.ToString());
    }

    static string ReverseString(string text)
    {
        char[] charArray = text.ToCharArray(); // Convert string to char array
        Array.Reverse(charArray);             // Reverse the array
        return new string(charArray);         // Convert back to string
    }

    static char[][] RotateMatrix90DegreesClockwise(char[][] matrix)
    {
        int rows = matrix.Length;
        int cols = matrix[0].Length;

        // Create a new matrix for the rotated result
        char[][] rotatedMatrix = new char[cols][];
        for (int i = 0; i < cols; i++)
        {
            rotatedMatrix[i] = new char[rows];
        }

        // Perform the rotation
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                rotatedMatrix[j][rows - 1 - i] = matrix[i][j];
            }
        }

        return rotatedMatrix;
    }

    static int CountOccurrences(string text, string word)
    {
        int count = 0;
        for (int i = 0; i <= text.Length - word.Length; i++)
        {
            if (text.Substring(i, word.Length) == word)
            {
                count++;
            }
        }
        return count;
    }

    static List<string> TransposeGrid(List<string> grid)
    {
        int rowCount = grid.Count;
        int colCount = grid[0].Length;

        List<string> result = new List<string>();

        // Iterate over columns to create rows for the transposed grid
        for (int col = 0; col < colCount; col++)
        {
            char[] newRow = new char[rowCount];
            for (int row = 0; row < rowCount; row++)
            {
                newRow[row] = grid[row][col];
            }
            result.Add(new string(newRow));
        }

        return result;
    }

    static List<string> FlattenDiagonals(List<string> grid)
    {
        int rowCount = grid.Count;
        int colCount = grid[0].Length;

        // Dictionary to store characters for each diagonal
        Dictionary<int, List<char>> diagonalGroups = new Dictionary<int, List<char>>();

        // Iterate through the grid
        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                int diagonalIndex = row + col; // Determine diagonal index

                // Add character to the appropriate diagonal group
                if (!diagonalGroups.ContainsKey(diagonalIndex))
                {
                    diagonalGroups[diagonalIndex] = new List<char>();
                }
                diagonalGroups[diagonalIndex].Add(grid[row][col]);
            }
        }

        // Convert diagonal groups into a list of strings
        List<string> result = new List<string>();
        foreach (var group in diagonalGroups)
        {
            result.Add(new string(group.Value.ToArray()));
        }

        return result;
    }

    static List<string> FlattenAntiDiagonals(List<string> grid)
    {
        int rowCount = grid.Count;
        int colCount = grid[0].Length;

        // Dictionary to store characters for each anti-diagonal
        Dictionary<int, List<char>> antiDiagonalGroups = new Dictionary<int, List<char>>();

        // Iterate through the grid
        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                int antiDiagonalIndex = row - col; // Determine anti-diagonal index

                // Add character to the appropriate anti-diagonal group
                if (!antiDiagonalGroups.ContainsKey(antiDiagonalIndex))
                {
                    antiDiagonalGroups[antiDiagonalIndex] = new List<char>();
                }
                antiDiagonalGroups[antiDiagonalIndex].Add(grid[row][col]);
            }
        }

        // Convert anti-diagonal groups into a list of strings, sorted by index
        List<string> result = new List<string>();
        foreach (var group in antiDiagonalGroups)
        {
            result.Add(new string(group.Value.ToArray()));
        }

        return result;
    }
}
