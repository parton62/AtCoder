using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC162
{
    /// <summary>
    /// 
    /// </summary>
    static class D
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var s = Next();

            var R = 0;
            var G = 1;
            var B = 2;

            var sums = new int[3, n + 1];
            var colors = new int[n];
            for (int i = 0; i < n; i++)
            {
                var c = s[i] == 'R' ? R : s[i] == 'G' ? G : B;
                colors[i] = c;
                for (int j = 0; j < 3; j++)
                {
                    sums[j, i+1] = sums[j, i];
                    if (j == c) sums[j, i+1]++;
                }
            }

            long counts = 0;
            for (int i = 0; i < n - 2; i++)
            {
                var left = colors[i];
                for (int j = i + 2; j < n; j++)
                {
                    var right = colors[j];
                    if (left == right) continue;

                    var middle = 3 - left - right;
                    counts += sums[middle, j + 1] - sums[middle, i];

                    if((i + j) % 2 == 0)
                    {
                        if (colors[(i + j) / 2] == middle) counts--;
                    }

                }
            }

            Console.WriteLine(counts);
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
