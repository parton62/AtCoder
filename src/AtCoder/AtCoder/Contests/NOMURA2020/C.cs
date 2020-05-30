using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.NOMURA2020
{
    /// <summary>
    /// 
    /// </summary>
    static class C
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var aas = ReadLongArray();

            if (n == 0)
            {
                if (aas[0] == 1)
                {
                    Console.WriteLine(1);
                    return;
                }
                else
                {
                    Console.WriteLine(-1);
                    return;
                }
            }


            long count = 1;
            
            var min = new long[n + 1];
            var max = new long[n + 1];

            long eda = aas[n];
            for (int i = n - 1; i >= 0; i--)
            {
                var a = aas[i];

                min[i] = (min[i + 1] + aas[i + 1] + 1) / 2;
                max[i] = max[i + 1] + aas[i + 1];

                eda = eda + a;
            }

            eda = 1 - aas[0];
            for (int i = 1; i <= n; i++)
            {
                var m = eda * 2 - aas[i];
                if(m < min[i])
                {
                    Console.WriteLine(-1);
                    return;
                }

                eda = Math.Min(m, max[i]);
                count += aas[i] + eda;
            }

            Console.WriteLine(count);
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
