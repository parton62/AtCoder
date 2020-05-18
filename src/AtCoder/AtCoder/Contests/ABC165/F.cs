using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC165
{
    /// <summary>
    /// 
    /// </summary>
    static class F
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var aas = ReadIntArray();
            var g = new Graph(n+1);
            for (int i = 0; i < n-1; i++)
            {
                var u = NextInt();
                var v = NextInt();
                g[u].Add(v);
                g[v].Add(u);
            }
            var lists = new List<int>[n + 1];
            var s = new Stack<int>();
            var result = new int[n + 1];
            s.Push(1);
            lists[1] = new List<int> { aas[0] };
            result[1] = 1;
            while (s.Any())
            {
                var node = s.Pop();
                var list = lists[node];
                foreach (var child in g[node])
                {
                    if (result[child] > 0) continue;
                    var childList= NewList(list, aas[child - 1]);
                    lists[child] = childList;
                    result[child] = childList.Count;
                    s.Push(child);
                }
            }

            
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine(result[i]);
            }
        }

        static List<int> NewList(List<int> prev, int value)
        {
            var index = prev.BinarySearch(value);
            if (index < 0) index = ~index;

            var result = prev.ToList();
            if (index < prev.Count)
            {
                if (result[index] > value)
                {
                    result[index] = value;
                }
            }
            else result.Add(value);

            return result;
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
