using System.Numerics;

namespace AdventOfCode._2025;

public partial class Day02
{
    public static class BigIntegerExtensions
    {
        public static IEnumerable<BigInteger> Range(BigInteger start, BigInteger count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            for (BigInteger i = 0; i < count; i++)
                yield return start + i;
        }
    }
 }
