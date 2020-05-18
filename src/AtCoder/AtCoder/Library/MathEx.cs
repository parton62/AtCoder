using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Library
{
    class MathEx
    {
        public static bool chmax<T>(ref T x, T y) where T : IComparable<T>
        {
            if (x.CompareTo(y) < 0)
            {
                x = y;
                return true;
            }
            return false;
        }
        public static bool chmin<T>(ref T x, T y) where T : IComparable<T>
        {
            if(x.CompareTo(y) > 0)
            {
                x = y;
                return true;
            }
            return false;
        }
        public static HashSet<long> GetDivisors(long n)
        {
            var f = Math.Sqrt(n);
            var d = new HashSet<long>();
            for (int i = 2; i <= f; i++)
            {
                if (n % i != 0) continue;
                d.Add(i);
                d.Add(n / i);
            }
            return d;
        }
    }
}
