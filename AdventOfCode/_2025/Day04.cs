using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode._2025;

public class Day04
{
   
    private readonly char[][] _input;

    public Day04()
    {
        _input = File.ReadLines("C:\\Users\\mdegraux\\source\\repos\\mdegrauxTP\\AdventOfCode\\AdventOfCode\\Inputs\\2025\\Day4.txt").Select(l => l.ToCharArray()).ToArray();
    }
    
    int[][] neighbors =
    {
            new[]{ -1, -1 }, new[]{ -1, 0 }, new[]{ -1, 1 },
            new[]{  0, -1 },                new[]{  0, 1 },
            new[]{  1, -1 }, new[]{  1, 0 }, new[]{  1, 1 },
        };

    public ValueTask<string> Solve_1()
    {
        var result = 0;
        for(int i = 0; i < _input.Length; i++)
        {
            StringBuilder lines = new();
            for(int j = 0; j < _input[i].Length; j++)
            {
                List<char> arround = new();
                foreach (var d in neighbors)
                {
                    int ni = i + d[0];
                    int nj = j + d[1];

                    // Boundary check
                    if (ni >= 0 && ni < _input.Length &&
                        nj >= 0 && nj < _input[ni].Length)
                    {
                        arround.Add(_input[ni][nj]);
                    }
                }

                result += _input[i][j] == '@' && arround.Where(x => x == '@').Count() < 4 ? 1 : 0;
            }
            Console.WriteLine(lines);
        }

        return new(result.ToString());
    }

 
    public ValueTask<string> Solve_2()
    {
        var result = 0;
        var lastResult = 0;
        do
        {
            lastResult = result;
            for (int i = 0; i < _input.Length; i++)
            {
                StringBuilder lines = new();
                for (int j = 0; j < _input[i].Length; j++)
                {
                    List<char> arround = new();
                    foreach (var d in neighbors)
                    {
                        int ni = i + d[0];
                        int nj = j + d[1];

                        // Boundary check
                        if (ni >= 0 && ni < _input.Length &&
                            nj >= 0 && nj < _input[ni].Length)
                        {
                            arround.Add(_input[ni][nj]);
                        }
                    }

                    if (_input[i][j] == '@' && arround.Where(x => x == '@' || x == '#').Count() < 4) 
                    {
                        result++;
                        _input[i][j] = '#';
                    }
                }
                Console.WriteLine(lines);
            }

            for (int i = 0; i < _input.Length; i++)
            {
                StringBuilder lines = new();
                for (int j = 0; j < _input[i].Length; j++)
                {
                    if (_input[i][j] == '#') _input[i][j] = 'x';
                }
            }

        } while (lastResult != result);

        return new(result.ToString());
    }
 }
