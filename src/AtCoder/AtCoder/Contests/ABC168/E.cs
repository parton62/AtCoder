using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC168
{
    /// <summary>
    /// 
    /// </summary>
    static class E
    {
        static void Main(string[] args)
        {
            var n = NextInt();

            var counts = new CountDic<Tuple<int, long, long>>();
            var zeros = 0;
            for (int i = 0; i < n; i++)
            {
                var a = NextLong();
                var b = NextLong();

                if (a == 0 && b == 0)
                {
                    zeros++;
                    continue;
                }

                if (a == 0 || b == 0)
                {
                    var aoverb = new Tuple<int, long, long>(0, a == 0 ? 0 : 1, b == 0 ? 0 : 1);
                    counts.CountUp(aoverb);
                    continue;
                }

                var asign = a >= 0 ? 1 : -1;
                var bsign = b >= 0 ? 1 : -1;

                a *= asign;
                b *= bsign;

                var gcd = GCD.Solve(a, b);

                a = a / gcd;
                b = b / gcd;
                var aoberb = new Tuple<int, long, long>(asign * bsign, a, b);

                counts.CountUp(aoberb);
            }


            mint result = 1;
            var hash = new HashSet<Tuple<int, long, long>>();
            foreach (var kv in counts)
            {
                var key = kv.Key;
                var v = kv.Value;

                var bovera = new Tuple<int, long, long>(-key.Item1, key.Item3, key.Item2);
                if (key.Item2 == 0 || key.Item3 == 0)
                {
                    bovera = new Tuple<int, long, long>(0, key.Item2 == 0 ? 1 : 0, key.Item3 == 0 ? 1 : 0);
                }

                if (hash.Contains(bovera))
                {
                    continue;
                }
                if (hash.Contains(key))
                {
                    continue;
                }

                var opponents = counts[bovera];
                result *= 1 + (mint.Pow(2, v) - 1) + (mint.Pow(2, opponents) - 1);

                hash.Add(key);
                hash.Add(bovera);
            }

            result += zeros;

            Console.WriteLine(result - 1);
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
    class CountDic<T> : IDictionary<T, int>
    {
        private Dictionary<T, int> _dic = new Dictionary<T, int>();
        public int this[T key]
        {
            get
            {
                return Get(key);
            }

            set
            {
                _dic[key] = value;
            }
        }

        public ICollection<T> Keys
        {
            get
            {
                return _dic.Keys;
            }
        }

        public ICollection<int> Values
        {
            get
            {
                return _dic.Values;
            }
        }

        public int Count
        {
            get
            {
                return _dic.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(T key, int value)
        {
            _dic.Add(key, value);
        }

        public void Add(KeyValuePair<T, int> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _dic = new Dictionary<T, int>();
        }

        public bool Contains(KeyValuePair<T, int> item)
        {
            return _dic.Contains(item);
        }

        public bool ContainsKey(T key)
        {
            return _dic.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<T, int>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            return _dic.GetEnumerator();
        }

        public bool Remove(T key)
        {
            return _dic.Remove(key);
        }

        public bool Remove(KeyValuePair<T, int> item)
        {
            return _dic.Remove(item.Key);
        }

        public bool TryGetValue(T key, out int value)
        {
            value = Get(key);
            return true;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dic.GetEnumerator();
        }

        private int Get(T key)
        {
            int c;
            if (_dic.TryGetValue(key, out c))
            {
                return c;
            }
            else
            {
                return 0;
            }
        }
        public int CountUp(T key)
        {
            var c = Get(key) + 1;
            _dic[key] = c;
            return c;
        }
    }
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
