using MoreLinq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2025;

public class Day01
{
    public class Dial
    {
        public int Index;
        public Dial? Previous;
        public Dial? Next;

        public Dial(int index)
        {
            Index = index;
        }
    }

    private readonly List<string> _input;

    public Day01()
    {
        _input = File.ReadAllLines("C:\\Users\\mdegraux\\source\\repos\\mdegrauxTP\\AdventOfCode\\AdventOfCode\\Inputs\\2025\\Day1.txt").Select(l => l).ToList();
    }

    public ValueTask<string> Solve_1()
    {
        var position = 50;
        var password = 0;

        var dials = SetupDials();

        var currentDial = dials[50];

        var instructions = _input.Select(x => (x[0], int.Parse(x.Substring(1))));

        foreach (var instruction in instructions)
        {
            var order = instruction.Item1 == 'L' ? -1 : 1;

            for(int i = 0; i < instruction.Item2; i++)
            {
                if (order == -1)
                {
                    currentDial = currentDial.Previous;
                }
                else 
                {
                    currentDial = currentDial.Next;
                }                
            }

            if (currentDial.Index == 0)
                password++;
        }


        return new(password.ToString());
    }

    private static List<Dial> SetupDials()
    {
        List<Dial> dials = new();

        Dial? previous = null;

        for (int i = 0; i < 100; i++)
        {
            Dial dial = new Dial(i);

            dial.Previous = previous;

            if (previous != null)
                previous.Next = dial;

            dials.Add(dial);
            previous = dial;
        }

        dials[0].Previous = dials[99];
        dials[99].Next = dials[0];

        return dials;
    }

    public ValueTask<string> Solve_2()
    {
        var position = 50;
        var password = 0;

        var dials = SetupDials();

        var currentDial = dials[50];

        var instructions = _input.Select(x => (x[0], int.Parse(x.Substring(1))));

        foreach (var instruction in instructions)
        {
            var order = instruction.Item1 == 'L' ? -1 : 1;

            for (int i = 0; i < instruction.Item2; i++)
            {
                if (order == -1)
                {
                    currentDial = currentDial.Previous;
                }
                else
                {
                    currentDial = currentDial.Next;
                }

                if (currentDial.Index == 0)
                    password++;
            }

            
        }


        return new(password.ToString());
    }
 }
