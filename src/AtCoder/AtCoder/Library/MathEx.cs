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
    }
}
