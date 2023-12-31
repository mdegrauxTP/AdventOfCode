﻿using System.Text;


namespace AdventOfCode.Extension;

public static class StringExtension
{
    public static string[] SplitIntoColumns(this string input)
    {
        var rows = input.SplitByNewline(false, false);
        int numColumns = rows.Max(x => x.Length);

        var res = new string[numColumns];
        for (int i = 0; i < numColumns; i++)
        {
            StringBuilder sb = new();
            foreach (var row in rows)
            {
                try
                {
                    sb.Append(row[i]);
                }
                catch (IndexOutOfRangeException)
                {
                    sb.Append(' ');
                }
            }
            res[i] = sb.ToString();
        }
        return res;
    }
    public static List<string> SplitByNewline(this string input, bool blankLines = false, bool shouldTrim = true)
    {
        return input
           .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
           .Where(s => blankLines || !string.IsNullOrWhiteSpace(s))
           .Select(s => shouldTrim ? s.Trim() : s)
           .ToList();
    }

    public static Dictionary<(int x, int y), char> ConvertToMap(this string input)
    {
        Dictionary<(int x, int y), char> patternDict = new Dictionary<(int x, int y), char>();

        string[] rows = input.Split("\r\n");

        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].Length; x++)
            {         
                patternDict[(x, y)] = rows[y][x];
            }
        }

        return patternDict;
    }
}
