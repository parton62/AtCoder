using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC167
{
    /// <summary>
    /// 
    /// </summary>
    static class D
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var k = NextLong();
            var aas = ReadIntArray();

            var visited = Enumerable.Repeat(-1, n).ToArray();

            //ループを探す
            var count = 0;
            var machi = 0;
            visited[0] = 0;

            while (true)
            {
                machi = aas[machi] - 1;
                count++;


                if (count == k)
                {
                    Console.WriteLine(machi + 1);
                    return;
                }

                if (visited[machi] >= 0)
                {
                    //その値が最初の定数回
                    //count と の差がループの回数

                    var f = visited[machi];
                    var loop = count - f;
                    k = k - f;
                    k = k % loop;

                    for (int i = 0; i < k; i++)
                    {
                        machi = aas[machi] - 1;
                    }

                    Console.WriteLine(machi + 1);
                    return;

                }
                visited[machi] = count;
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
