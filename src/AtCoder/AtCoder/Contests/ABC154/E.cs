using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC154
{
    /// <summary>
    /// 
    /// </summary>
    static class E
    {
        static void Main(string[] args)
        {
            var n = Next().Reverse().Select(c => c - '0').ToArray();
            var k = NextInt();

            //下から i 桁目まで使ったときに時に 0 出ない数字がちょうど j 個であるようなものの個数
            //i桁目の数字が最大ではない場合の数
            //二桁目以降は0～9をとれる
            var dp1 = new int[n.Length, k + 1];
            //i桁目の数字が最大値をとる場合の場合の数
            //
            var dp2 = new int[n.Length, k + 1];

            dp2[0, 0] = 1;
            dp2[0, 1] = Math.Max(n[0], 0);

            //for (int i = 1; i <= k; i++)
            //{
            //    dp1[0, i] = 9;
            //    if (k == 0) dp2[0, i] 
            //    if (k == 1) dp2[0, i] = Math.Max(0, n[i] - 1);
            //}
            for (int i = 1; i < n.Length; i++)
            {
                dp1[i, 0] = 9 * dp1[i - 1, 0];
                dp2[i, 0] = Math.Max(0, n[i] - 1) * dp2[i - 1, 0];
                for (int j = 1; j <= k; j++)
                {
                    dp1[i, j] = 9 * dp1[i - 1, j - 1] + dp1[i - 1, j];
                    dp2[i, j] = Math.Max(0, n[i] - 1) * dp2[i - 1, j - 1];
                }
            }

            Console.WriteLine((n[n.Length - 1] - 1) * dp1[n.Length - 1, k] + dp2[n.Length - 1, k]);
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
