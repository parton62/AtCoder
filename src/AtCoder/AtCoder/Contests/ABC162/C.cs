using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC162
{
    /// <summary>
    /// 
    /// </summary>
    static class C
    {
        static void Main(string[] args)
        {
            var k = NextInt();

            var sum = 0;
            for (int a = 1; a <= k; a++)
            {
                for (int b = 1; b <= k; b++)
                {
                    var gcd = GCD.Solve(a, b);
                    for (int c = 1; c <= k; c++)
                    {
                        sum += GCD.Solve(gcd, c);
                    }
                }
            }

            Console.WriteLine(sum);
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
