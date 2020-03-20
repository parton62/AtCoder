using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Library
{
    class GCD
    {
        public static int Solve(int x, int y)
        {
            var a = Math.Max(x, y);
            var b = x + y - a;

            while (b != 0)
            {
                var c = b;
                b = a % b;
                a = c;
            }

            return a;
        }
        public static long Solve(long x, long y)
        {
            var a = Math.Max(x, y);
            var b = x + y - a;

            while (b != 0)
            {
                var c = b;
                b = a % b;
                a = c;
            }

            return a;
        }
    }
}
