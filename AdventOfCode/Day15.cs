using MoreLinq;
using System.Drawing;

namespace AdventOfCode;

public class Day15 : BaseDay
{
    private readonly List<string> _input;
    Dictionary<int, Box> boxes = new Dictionary<int, Box>();

    public Day15()
    {
        _input = File.ReadAllText(InputFilePath).Split(",").ToList();
    }
    public override ValueTask<string> Solve_1()
    {
        var result = _input.Select(s => s.Aggregate(0, (acc, x) => (acc + Convert.ToInt32(x)) * 17 % 256)).Sum();
        return new(result.ToString());
    }
      
    public override ValueTask<string> Solve_2()
    {
        _input.ForEach(x => Step(x, boxes));
        var result = boxes.SelectMany((b, ib) => b.Value.Lenses.Select((l, il) => (b.Key + 1) * (il+1) * l.FocalLength)).Sum();
        
        return new(result.ToString());
    }

    static void Step(string step, Dictionary<int, Box> boxes)
    {
        // Parse the step
        string[] split = step.Split(new[] { '=', '-' });
        string label = split[0];
        char operation = step[split[0].Length];
        int value = operation == '=' ? int.Parse(split[1]) : 0;

        int hash = label.Aggregate(0, (acc, x) => (acc + Convert.ToInt32(x)) * 17 % 256);

        if (!boxes.ContainsKey(hash))
            boxes[hash] = new Box();

        Box box = boxes[hash];
        if (operation == '=')
        {
            // Add or replace lens
            Lens lens = box.Lenses.Find(l => l.Label == label);
            if (lens is not null)
                lens.FocalLength = value;
            else
                box.Lenses.Add(new Lens(label, value));
        }
        else if (operation == '-')
        {
            box.Lenses.RemoveAll(l => l.Label == label);
        }
    }

    class Lens
    {
        public string Label { get; set; }
        public int FocalLength { get; set; }

        public Lens(string label, int focalLength)
        {
            Label = label;
            FocalLength = focalLength;
        }
    }

    class Box
    {
        public List<Lens> Lenses { get; } = new List<Lens>();
    }
}