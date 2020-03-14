using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC157
{
    /// <summary>
    /// 
    /// </summary>
    static class C
    {
        static void Main(string[] args)
        {
            var nm = ReadIntArray();
            var n = nm[0];
            var m = nm[1];

            var digit = Enumerable.Repeat(-1, n).ToArray();

            for (int i = 0; i < m; i++)
            {
                var sc = ReadIntArray();
                var s = sc[0] - 1;
                var c = sc[1];

                if (digit[s] == -1)
                {
                    digit[s] = c;
                    continue;
                }
                else if(digit[s] != c)
                {
                    Console.WriteLine(-1);
                    return;
                }
            }

            if (n > 1 && digit[0] == 0)
            {
                Console.WriteLine(-1);
                return;
            }

            if (n==1 && digit[0] < 0)
            {
                Console.WriteLine(0);
                return;
            }

            var result = 0;
            if (digit[0] < 0)
            {
                result = 1;
            }
            else
            {
                result = digit[0];
            }
            for (int i = 1; i < n; i++)
            {
                if (digit[i] < 0) digit[i] = 0;
                result = result * 10 + digit[i];
            }
            Console.WriteLine(result);
        }

        #region Console
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
