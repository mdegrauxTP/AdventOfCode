using MoreLinq;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly List<(int first, int second)> _pageOrder;
    private readonly List<int[]> _pagesToUpdate;
    private readonly List<int> _pagesToUpdateIndexThatAreValid;
    private int indexInput = 0;
    public Day05()
    {
        _pageOrder = File.ReadAllLines(InputFilePath)
                         .TakeWhile(x => x != "")
                         .Select((l, index) => { 
                            indexInput = index;
                            return (int.Parse(l.Split("|")[0]), int.Parse(l.Split("|")[1]));
                          })
                         .ToList();
        _pagesToUpdate = File.ReadAllLines(InputFilePath)
                             .Skip(indexInput + 2)
                             .Select(l => l.Split(',').Select(x => int.Parse(x)).ToArray()).ToList();        
    }

    public override ValueTask<string> Solve_1()
    {
        int result = 0;
        foreach(var pages in _pagesToUpdate)
        {
            bool isValid = true;
            foreach(var pageNumber in pages)
            {
                int index = Array.IndexOf(pages, pageNumber);

                //CheckAfter
                var orderWherePageNumberShouldBePrintedAfter = _pageOrder.Where(p => p.second == pageNumber);                
                List<int> pageAfter = index != -1 ? pages.Skip(index + 1).ToArray().ToList() : new List<int>();
                isValid = !orderWherePageNumberShouldBePrintedAfter.Select(x => x.first).Any(p => pageAfter.Contains(p));
                if (!isValid) break;

                //CheckBefore
                var orderWherePageNumberShouldBePrintedBefore = _pageOrder.Where(p => p.first == pageNumber);
                List<int> pageBefore = index > 0 ? pages.Take(index).ToArray().ToList() : new List<int>();                
                isValid = !orderWherePageNumberShouldBePrintedBefore.Select(x => x.second).Any(p => pageBefore.Contains(p));
                if (!isValid) break;
            }

            if (isValid)
            {
                result += pages.ToArray()[(pages.Count() - 1) / 2]; //récupération de l'élement du milieu
            }
        }
        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int result = 0;
        foreach (var pages in _pagesToUpdate)
        {            
            if (CheckValidity(pages))
            {

                result += pages.ToArray()[(pages.Count() - 1) / 2]; //récupération de l'élement du milieu
            }
        }
        return new(result.ToString()); // et on retire la P1;
    }

    private bool CheckValidity(int[] pages)
    {
        foreach (var pageNumber in pages)
        {
            int index = Array.IndexOf(pages, pageNumber);

            //CheckAfter
            var orderWherePageNumberShouldBePrintedAfter = _pageOrder.Where(p => p.second == pageNumber);
            List<int> pageAfter = index != -1 ? pages.Skip(index + 1).ToArray().ToList() : new List<int>();
            int numberToSwap = orderWherePageNumberShouldBePrintedAfter.Select(x => x.first).FirstOrDefault(p => pageAfter.Contains(p));
            if (numberToSwap != 0)
            {
                var newArray = SwapNumber(pages, pageNumber, numberToSwap);
                return CheckValidity(newArray); //same array as pages, pass by ref
            }

            //CheckBefore
            var orderWherePageNumberShouldBePrintedBefore = _pageOrder.Where(p => p.first == pageNumber);
            List<int> pageBefore = index > 0 ? pages.Take(index).ToArray().ToList() : new List<int>();
            numberToSwap = orderWherePageNumberShouldBePrintedBefore.Select(x => x.second).FirstOrDefault(p => pageBefore.Contains(p));
            if (numberToSwap != 0)
            {
                var newArray = SwapNumber(pages, pageNumber, numberToSwap);
                return CheckValidity(newArray);
            }
        }

        return true;
    }

    private int[] SwapNumber(int[] array, int num1, int num2)
    {
        int index1 = Array.IndexOf(array, num1);
        int index2 = Array.IndexOf(array, num2);

        if (index1 != -1 && index2 != -1)
        {
            // Swap the numbers
            int temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        return array;
    }
}
