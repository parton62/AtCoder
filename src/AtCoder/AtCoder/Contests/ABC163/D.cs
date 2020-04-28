using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC163
{
    /// <summary>
    /// 
    /// </summary>
    static class D
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var k = NextInt();

            mint result = 0;

            for (long i = k; i <= n + 1; i++)
            {
                result += i * (n - i + 1) + 1;
            }

            Console.WriteLine(result);
        }

        struct mint : IEquatable<mint>
        {
            //static uint mod = 998244353;
            public static uint mod = 1000000007;
            uint _v;

            public mint(long v) : this((uint)(v % mod + mod)) { }
            private mint(uint v)
            {
                _v = v < mod ? v : (v - mod);
            }

            public static void SetMod(uint p)
            {
                mod = p;
            }

            public static mint operator +(mint lhs, mint rhs)
            {
                return new mint(lhs._v + rhs._v);
            }
            public static mint operator -(mint lhs, mint rhs)
            {
                return new mint(lhs._v + mod - rhs._v);
            }
            public static mint operator *(mint lhs, mint rhs)
            {
                return new mint((uint)((ulong)lhs._v * rhs._v % mod));
            }
            public static mint operator /(mint lhs, mint rhs)
            {
                return lhs * rhs.Inv();
            }
            public static mint operator ^(mint x, int n)
            {
                return Pow(x, n);
            }
            public static bool operator ==(mint l, mint r)
            {
                return l._v == r._v;
            }
            public static bool operator !=(mint l, mint r)
            {
                return !(l == r);
            }
            public static bool operator <(mint l, mint r)
            {
                return l._v < r._v;
            }
            public static bool operator >(mint l, mint r)
            {
                return l._v > r._v;
            }

            public static mint Pow(mint x, int n)
            {
                var r = new mint(1);
                while (n > 0)
                {
                    if ((n & 1) > 0) r *= x;
                    x *= x;
                    n >>= 1;
                }
                return r;
            }

            public mint Pow(int n)
            {
                return Pow(this, n);
            }
            public mint Inv()
            {
                return Pow((int)mod - 2);
            }

            public bool Equals(mint o)
            {
                return this == o;
            }

            public override bool Equals(object obj)
            {
                if (obj is mint) return this == (mint)obj;
                else return false;
            }
            public override int GetHashCode()
            {
                return _v.GetHashCode();
            }
            public override string ToString()
            {
                return _v.ToString();
            }

            public static implicit operator mint(int i)
            {
                return new mint(i);
            }
            public static implicit operator mint(long i)
            {
                return new mint(i);
            }


            private static mint[] _fact;
            public static void SetFact(int n)
            {
                _fact = new mint[n + 1];
                _fact[0] = 1;
                for (int i = 1; i < n + 1; i++)
                {
                    _fact[i] = _fact[i - 1] * i;
                }
            }
            public static mint Fact(int n)
            {
                if (_fact == null || _fact.Length <= n)
                {
                    SetFact(n * 2);
                }
                return _fact[n];
            }

            public static mint Combination(int n, int r)
            {
                return Fact(n) / (Fact(n - r) * Fact(r));
            }
        }
        #region Console
        static int _index = 0;
        static string[] _buffer;

        static string Next()
        {
            if (_buffer == null || _index >= _buffer.Length)
            {
                _buffer = Console.ReadLine().Split();
                _index = 0;
            }

            return _buffer[_index++];
        }
        static int NextInt()
        {
            return int.Parse(Next());
        }
        static long NextLong()
        {
            return long.Parse(Next());
        }
        static int ReadInt()
        {
            return int.Parse(Console.ReadLine());
        }
        static long ReadLong()
        {
            return long.Parse(Console.ReadLine());
        }
        static int[] ReadIntArray()
        {
            return Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        }
        static int[] ReadIntArray(int n)
        {
            var a = new int[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = ReadInt();
            }
            return a;
        }
        static long[] ReadLongArray()
        {
            return Console.ReadLine().Split(' ').Select(long.Parse).ToArray();
        }
        static long[] ReadLongArray(int n)
        {
            var a = new long[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = ReadLong();
            }
            return a;
        }
        static List<T> ReadObjects<T>(int n, Func<int[], T> creator)
        {
            var l = new List<T>(n);

            for (int i = 0; i < n; i++)
            {
                var a = ReadIntArray();
                l.Add(creator(a));
            }

            return l;
        }

        static int[,] ReadMatrix(int n, int m)
        {
            var r = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                var a = ReadIntArray();
                for (int j = 0; j < m; j++)
                {
                    r[i, j] = a[j];
                }
            }
            return r;
        }
        #endregion
    }
}
