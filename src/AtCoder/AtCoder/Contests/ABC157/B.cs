using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC157
{
    /// <summary>
    /// 
    /// </summary>
    static class B
    {
        static void Main(string[] args)
        {
            var aas = ReadMatrix(3, 3);
            var n = ReadInt();
            var bs = new HashSet<int>();
            for (int i = 0; i < n; i++)
            {
                bs.Add(ReadInt());
            }

            for (int i = 0; i < 3; i++)
            {
                if (Enumerable.Range(0, 3).All(j => bs.Contains(aas[i, j])))
                {
                    Console.WriteLine("Yes");
                    return;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (Enumerable.Range(0, 3).All(j => bs.Contains(aas[j, i])))
                {
                    Console.WriteLine("Yes");
                    return;
                }
            }
            if (Enumerable.Range(0, 3).All(i => bs.Contains(aas[i, i]))
              || Enumerable.Range(0, 3).All(i => bs.Contains(aas[2-i, i])))
            {
                Console.WriteLine("Yes");
                return;
            }

            Console.WriteLine("No");
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
