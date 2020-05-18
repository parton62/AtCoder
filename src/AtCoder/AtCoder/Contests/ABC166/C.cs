using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC166
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
            var hs = ReadIntArray();
            var abs = ReadObjects(m, a => new { A = a[0] - 1, B = a[1] - 1 });

            var good = new int[n];

            for (int i = 0; i < m; i++)
            {
                var ab = abs[i];
                var ha = hs[ab.A];
                var hb = hs[ab.B];

                if(ha == hb)
                {
                    good[ab.A] = 1;
                    good[ab.B] = 1;
                    continue;
                }
                if(ha > hb)
                {
                    good[ab.B] = 1;
                    continue;
                }
                else
                {
                    good[ab.A] = 1;
                    continue;
                }
            }

            Console.WriteLine(good.Count(g => g == 0));
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
