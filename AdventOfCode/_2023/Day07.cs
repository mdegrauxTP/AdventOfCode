using MoreLinq;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode2023;

public class Day07 : BaseDay
{
    private readonly List<(char[] hand, int bit)> _input;    

    public Day07()
    {
       _input = File.ReadAllLines(InputFilePath).Select(l => (l.Split(" ")[0].ToArray(), int.Parse(l.Split(" ")[1]))).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var sorted = _input.OrderBy(x => HandType(x.hand)).ThenBy(x => StrongestOrder(x.hand));
        var result = sorted.Select((s, index) => (s, index)).Sum(s => s.s.bit * (s.index + 1));
        return new(result.ToString());  
    }

    private long StrongestOrder(char[] hand, bool p1 = true)
    {
        StringBuilder powerSB = new StringBuilder(); ;
        foreach(char c in hand)
        {
            powerSB.Append(ConvertCardToPower(c, p1));
        }

        return long.Parse(powerSB.ToString());
    }

    private string ConvertCardToPower(char c, bool p1)
    {
        return c switch {
            'T' => "10",
            'J' => p1 ? "11" : "01",
            'Q' => "12",
            'K' => "13",
            'A' => "14",
            _ => $"0{c}"
        };
    }

    private int HandType(char[] hand)
    {
        var grouped = hand.GroupBy(c => c);
        if(grouped.Any(g => g.Key == 'J'))
        {
            int max = 0;
            for(int i = 0; i < 5; i++)
            {
                if (hand[i] == 'J') continue;
                var joker = new string(hand).Replace('J', hand[i]);
                max = Math.Max(max, HandType(joker.ToArray()));
            }
            return max == 0 ? 7 : max; // cas JJJJJ
        }
        else
        {
            if (grouped.Count() == 1) return 7; // five of a kind
            if (grouped.Count() == 2 && grouped.Any(g => g.Count() == 4)) return 6; ; //four of a kind
            if (grouped.Count() == 2 && grouped.Any(g => g.Count() == 3)) return 5; //full house 
            if (grouped.Count() == 3 && grouped.Any(g => g.Count() == 3)) return 4; //three of a kind
            if (grouped.Count() == 3 && grouped.Count(g => g.Count() == 2) == 2) return 3; //two pair
            if (grouped.Count() == 4) return 2; //one pair
            return 1; //high card        
        }
    }

    public override ValueTask<string> Solve_2()
    {
        var sorted = _input.OrderBy(x => HandType(x.hand)).ThenBy(x => StrongestOrder(x.hand, p1: false));
        var result = sorted.Select((s, index) => (s, index)).Sum(s => s.s.bit * (s.index + 1));
        return new(result.ToString());
    }  
 }
