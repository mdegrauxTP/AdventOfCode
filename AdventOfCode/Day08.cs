using MoreLinq;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace AdventOfCode;

public class Day08 : BaseDay
{
    private readonly IEnumerable<string> _input;
    private Queue<char> _instructions;
    private readonly Dictionary<string, (string left, string right)> _map;

    public Day08()
    {
        _input = File.ReadAllLines(InputFilePath);
        _instructions = new Queue<char>(_input.First().ToString().ToArray());
        _map = ParseInput(_input);
    }

    public override ValueTask<string> Solve_1()
    {
        var current = "AAA";
        int step = 0;
        do
        {
            var currentInstruction = _instructions.Dequeue();
            if (_instructions.Count == 0)
                _instructions = new Queue<char>(_input.First().ToString().ToArray()); //refill
            if (currentInstruction == 'L')
            {
                current = _map[current].left;
            }
            else
            {
                current = _map[current].right;
            }
            step++;
        } while (current != "ZZZ");
        return new(step.ToString());
        return new("");
    }    

    public override ValueTask<string> Solve_2()
    {
        IEnumerable<string> startingPos = _map.Select(x => x.Key).Where(x => x.EndsWith('A'));
        Dictionary<string, int> minimumByPath = new();
        
        foreach (var pos in startingPos)
        {
            int step = 0;
            var current = pos;
            do
            {
                var instruction = new Queue<char>(_input.First().ToString().ToArray());
                if (_instructions.Count == 0)
                    _instructions = new Queue<char>(_input.First().ToString().ToArray()); //refill

                var currentInstruction = _instructions.Dequeue();
                
                if (currentInstruction == 'L')
                {
                    current = _map[current].left;
                }
                else
                {
                    current = _map[current].right;
                }
                step++;
            } 
            while (!current.EndsWith("Z"));

            minimumByPath.TryAdd(pos, step);
        }

        var result = CalculateLCM(minimumByPath.Select(x => x.Value).ToArray());

        return new(result.ToString());
    }

    static BigInteger CalculateLCM(int[] numbers)
    {
        BigInteger lcm = numbers[0];

        for (int i = 1; i < numbers.Length; i++)
        {
            lcm = CalculateLCM(lcm, numbers[i]);
        }

        return lcm;
    }

    static BigInteger CalculateLCM(BigInteger a, int b)
    {        
        return (a * b) / BigInteger.GreatestCommonDivisor(a, b);
    }


    static Dictionary<string, (string, string)> ParseInput(IEnumerable<string> input)
    {
        return input
            .Select(line => line.Split('='))
            .Where(parts => parts.Length == 2)
            .ToDictionary(
                parts => parts[0].Trim(),
                parts =>
                {
                    var valueParts = parts[1]
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(part => part.Trim('(', ')', ' '))
                        .ToArray();

                    return (valueParts[0], valueParts[1]);
                }
            );
    }
}