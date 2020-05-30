using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;

namespace AtCoder.Contests.AGC044
{
    /// <summary>
    /// 
    /// </summary>
    static class A
    {
        static void Main(string[] args)
        {
            var t = NextInt();
            for (int i = 0; i < t; i++)
            {
                var n = (ulong)NextLong();
                var a = (ulong)NextLong();
                var b = (ulong)NextLong();
                var c = (ulong)NextLong();
                var d = (ulong)NextLong();

                var dic = new Dictionary<ulong, ulong>();
                dic[1] = (ulong)d;
                ulong Solve(ulong n, Dictionary<ulong, ulong> memo)
                {
                    if (memo.ContainsKey(n)) return memo[n];
                    if (n == 0) return 0;

                    var min = ulong.MaxValue;
                    if ((double)n*d < ulong.MaxValue)
                    {
                        min = n * d;
                    }
                    min = Min(min, Solve(n / 2, memo) + a + (n % 2) * d);
                    min = Min(min, Solve((n + 1) / 2, memo) + a + (2 - (n % 2)) * d);
                    min = Min(min, Solve(n / 3, memo) + b + (n % 3) * d);
                    min = Min(min, Solve((n + 2) / 3, memo) + b + (3 - (n % 3)) * d);
                    min = Min(min, Solve(n / 5, memo) + c + (n % 5) * d);
                    min = Min(min, Solve((n + 4) / 5, memo) + c + (5 - (n % 5)) * d);

                    memo[n] = min;

                    return min;
                }

                Solve(n, dic);
                Console.WriteLine(dic[n]);
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
