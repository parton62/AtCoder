using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC168
{
    /// <summary>
    /// 
    /// </summary>
    static class D
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var m = NextInt();

            var g = new Graph(n + 1);
            for (int i = 0; i < m; i++)
            {
                var a = NextInt();
                var b = NextInt();
                g[a].Add(b);
                g[b].Add(a);
            }

            var from = new int[n + 1];
            var cost = new int[n + 1];

            var q = new Queue<int>();
            q.Enqueue(1);
            while (q.Any())
            {
                var current = q.Dequeue();
                foreach (var next in g[current])
                {
                    if (from[next] > 0) continue;

                    q.Enqueue(next);
                    from[next] = current;
                }
            }


            Console.WriteLine("Yes");
            for (int i = 2; i <= n; i++)
            {
                Console.WriteLine(from[i]);
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

    class Graph
    {
        private int _size;
        private List<int>[] _edges;

        public Graph(int size)
        {
            _size = size;
            _edges = new List<int>[size];
        }

        public List<int> this[int i]
        {
            get
            {
                if (_edges[i] == null) _edges[i] = new List<int>();
                return _edges[i];
            }
        }
    }

}
