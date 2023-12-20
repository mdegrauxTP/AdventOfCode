using AdventOfCode.Extension;
using MoreLinq;
using System.Drawing;
using System.Text.RegularExpressions;
using static AdventOfCode.Day10;

namespace AdventOfCode;

public class Day16 : BaseDay
{
    private static Dictionary<(int, int), char> _input;
    private static List<Beam> beams = new();
    private static List<Beam> tmp = new();
    private static HashSet<(int, int)> marked = new();

    class Beam
    {
        public Beam((int, int) position, char direction)
        {
            Position = position;
            Direction = direction;
        }

        public (int x, int y) Position;
        public char Direction;

        public void Move()
        {
            MoveToNextPosition(Direction);
            bool isStillInMap = _input.TryGetValue(Position, out char value);
            if (isStillInMap)
            {
                if(!marked.Add(Position)) beams.Remove(this);
                React(value);
            }                
            else
                beams.Remove(this);
        }

        private void React(char v)
        {
            switch (v)
            {
                case '.': break;
                case '-':
                    if (Direction == 'R' || Direction == 'L') break;
                    else Split(v); break;
                case '|':
                    if (Direction == 'U' || Direction == 'D') break;
                    else Split(v); break;
                case '\\':
                case '/':
                    Redirect(v); break;
                default: throw new Exception();
            }
        }

        private void Redirect(char v)
        {
            if(v == '/')
            {
                if (Direction == 'U') { Direction = 'R'; return; }     
                if (Direction == 'D') {Direction = 'L'; return;}
                if (Direction == 'R') {Direction = 'U'; return;}
                if (Direction == 'L') {Direction = 'D'; return;}
            }
            if (v == '\\')
            {
                if (Direction == 'U') { Direction = 'L'; return; }
                if (Direction == 'D') { Direction = 'R'; return; }
                if (Direction == 'R') {Direction = 'D'; return;}
                if (Direction == 'L') {Direction = 'U'; return;}
            }
        }

        private void Split(char v)
        {
            if (v == '-')
            {
                if (Direction == 'U' || Direction == 'D')
                {
                    Direction = 'R'; //arbitraire
                    beams.Add(new Beam(Position, 'L'));
                }
            }
            if (v == '|')
            {
                if (Direction == 'L' || Direction == 'R')
                {
                    Direction = 'U'; //arbitraire
                    beams.Add(new Beam(Position, 'D'));
                }
            }
        }

        private void MoveToNextPosition(char direction)
        {
            switch (direction)
            {
                case 'U': Position.y--; break;
                case 'D': Position.y++; break;
                case 'R': Position.x++; break;
                case 'L': Position.x--; break;
                default: throw new Exception();
            };           
        }
    }

    public Day16()
    {
        _input = File.ReadAllText(InputFilePath).ConvertToMap();
    }
    public override ValueTask<string> Solve_1()
    {        
        beams.Add(new Beam((0, 0), 'R'));
        tmp = new List<Beam>(beams);
        while (beams.Any())
        {
           
            for (int i = 0; i < tmp.Count; i++)
            {
                tmp[i].Move();
            }

            tmp = new List<Beam>(beams);
        }

        return new(marked.Count.ToString());
    }



    public override ValueTask<string> Solve_2()
    {
        return new("");
    }

   
}