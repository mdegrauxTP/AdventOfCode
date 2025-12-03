using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode._2025;

public class Day03
{
   
    private readonly IEnumerable<string> _input;

    public Day03()
    {
        _input = File.ReadAllLines("C:\\Users\\mdegraux\\source\\repos\\mdegrauxTP\\AdventOfCode\\AdventOfCode\\Inputs\\2025\\Day3.txt").Select(l => l);
    }

    public class Bank
    {
        public List<Battery> Batteries { get; set; }
    }

    public class Battery
    {
        public Battery(int number, int index)
        {
            Number = number;
            Index = index;
        }

        public int Number { get; set; }
        public int Index { get; set; }      
    }

    public ValueTask<string> Solve_1()
    {
        List<Bank> banks = _input.Select(i => new Bank() 
                                            { 
                                                Batteries = i.ToArray().Index().Select(a => new Battery(int.Parse(a.Item.ToString()), a.Index)).ToList() 
                                            }
                            ).ToList();
        var result = 0;
        foreach(var bank in banks)
        {
            var maxDizaine = bank.Batteries[..^1].Max(x => x.Number);
            var batterieDizaine = bank.Batteries.OrderBy(x => x.Index).First(x => x.Number == maxDizaine);
            var maxUnite = bank.Batteries[1..].Where(x => x.Index > batterieDizaine.Index).Max(x => x.Number);
            result += ((maxDizaine * 10) + maxUnite);
        }

        return new(result.ToString());
    }

 
    public ValueTask<string> Solve_2()
    {

        List<Bank> banks = _input.Select(i => new Bank()
        {
            Batteries = i.ToArray().Index().Select(a => new Battery(int.Parse(a.Item.ToString()), a.Index)).ToList()
        }
                            ).ToList();


        BigInteger result = 0;
        foreach (var bank in banks)
        {
            List<int> bankResult = new(); 
            var diff = bank.Batteries.Count() - 12;
            int position = 0;
            for(int i = 0; i < 12; i++)
            {
               
                var batteringInRange = bank.Batteries.Where(x => x.Index >= position && x.Index <= diff);
                var valeurBattery = batteringInRange.Max(x => x.Number);
                var matchingBattery = batteringInRange.OrderBy(x => x.Index).FirstOrDefault(x => x.Number == valeurBattery);
                bankResult.Add(matchingBattery.Number);
                position = matchingBattery.Index + 1;
                diff = bank.Batteries.Count() - 12 + bankResult.Count();
            }

            result += BigInteger.Parse(string.Concat(bankResult));
        }



        return new(result.ToString());
    }
 }
