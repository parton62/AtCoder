using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Contests.ABC158
{
    /// <summary>
    /// 
    /// </summary>
    static class D
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine();
            var q = ReadInt();

            var forward = true;
            var f = new StringBuilder();
            var b = new StringBuilder();

            for (int i = 0; i < q; i++)
            {
                var qs = Console.ReadLine().Split();
                if (qs.Length == 1)
                {
                    forward = !forward;
                    continue;
                }

                var f1 = qs[1] == "1";
                if (forward ^ f1)
                {
                    b.Append(qs[2]);
                }
                else
                {
                    f.Append(qs[2]);
                }
            }

            if (forward)
            {
                var ss = f.ToString();
                for (int i = ss.Length - 1; i >= 0; i--)
                {
                    Console.Write(ss[i]);
                }
                Console.Write(s);
                Console.Write(b.ToString());
            }
            else
            {
                var ss = b.ToString();
                for (int i = ss.Length - 1; i >= 0; i--)
                {
                    Console.Write(ss[i]);
                }
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    Console.Write(s[i]);
                }
                Console.Write(f.ToString());
            }
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
