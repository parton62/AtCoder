using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoder.Library
{
    static class Bits
    {
        public static IEnumerable<int> Enumerate(int bitDigits)
        {
            return Enumerable.Range(0, (int)Math.Pow(2, bitDigits));
        }

        public static bool HasFlag(this int bits, int index)
        {
            return (bits >> index & 1) == 1;
        }
    }
}
