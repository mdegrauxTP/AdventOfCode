using MoreLinq;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly List<string> _input;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath).Select(l => l).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        int reportSafe = 0;
        var reports = _input.Select(line => line.Split(" ").Select(x => int.Parse(x)).ToArray());
        foreach (var report in reports)
        {
            bool isSafe = true;
            var order = report[0] < report[1] ? -1 : 1; //+1 = le rapport est descendant, -1 il est montant
            for(int i = 0; i < report.Count() -1; i++)
            {
                int diff = report[i] - report[i + 1];
                if (Math.Sign(diff) != Math.Sign(order)) isSafe = false;
                if (Math.Abs(diff) > 3) isSafe = false;

                if (!isSafe) break;
            }

            if (isSafe) reportSafe++;
        }

        return new(reportSafe.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int reportSafe = 0;
        var reports = _input.Select(line => line.Split(" ").Select(x => int.Parse(x)).ToList());
        List<List<int>> okOnes = new();
        foreach (var report in reports)
        {
            bool isSafe = true;
            var order = report[0] < report[1] ? -1 : 1; //+1 = le rapport est descendant, -1 il est montant
            for (int i = 0; i < report.Count() - 1; i++)
            {
                int diff = report[i] - report[i + 1];
                if (Math.Sign(diff) != Math.Sign(order)) isSafe = false;
                if (Math.Abs(diff) > 3) isSafe = false;

                if (!isSafe)
                {
                    List<int> copy = new List<int>(report);
                    List<int> copy2 = new List<int>(report);
                    List<int> copy3 = new List<int>(report);
                    isSafe = Remove(copy, i) || Remove(copy2, i + 1) || (i != 0 && Remove(copy3, i - 1));
                    break;
                };
            }

            if (isSafe) reportSafe++;
        }

        return new(reportSafe.ToString());    
    }       

    private bool Remove(List<int> report, int i)
    {
        bool isSafe = true;
        report.RemoveAt(i);
        var possiblyNewOrder = report[0] < report[1] ? -1 : 1; //+1 = le rapport est descendant, -1 il est montant
        for (int j = 0; j < report.Count() - 1; j++)
        {
            int diff = report[j] - report[j + 1];
            if (Math.Sign(diff) != Math.Sign(possiblyNewOrder)) isSafe = false;
            if (Math.Abs(diff) > 3) isSafe = false;

            if (!isSafe) break;
        }
        
        return isSafe;
    }
}
