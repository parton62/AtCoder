using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC167
{
    /// <summary>
    /// 
    /// </summary>
    static class C
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var m = NextInt();
            var x = NextInt();

            var min = int.MaxValue;

            var cs = new int[n];
            var aas = new List<int>[n];

            for (int i = 0; i < n; i++)
            {
                cs[i] = NextInt();
                var l = new List<int>();
                for (int j = 0; j < m; j++)
                {
                    l.Add(NextInt());
                }
                aas[i] = l;
            }

            //全組み合わせ
            foreach (var bits in Bits.Enumerate(12))
            {
                var price = 0;
                var skills = new int[m];
                for (int i = 0; i < n; i++)
                {
                    if (!bits.HasFlag(i)) continue;
                    price += cs[i];
                    for (int j = 0; j < m; j++)
                    {
                        skills[j] += aas[i][j];
                    }
                }

                if (skills.All(s => s>= x))
                {
                    min = Math.Min(min, price);
                }
            }

            if (min == int.MaxValue)
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine(min);
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
