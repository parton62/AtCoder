using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC157
{
    /// <summary>
    /// FriendSuggestions
    /// </summary>
    static class D
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var m = NextInt();
            var k = NextInt();

            var blocked = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                blocked[i] = new List<int>();
            }
            var uf = new UnionFind(n);

            for (int i = 0; i < m; i++)
            {
                var a = NextInt() - 1;
                var b = NextInt() - 1;
                uf.Unite(a, b);
                blocked[a].Add(b);
                blocked[b].Add(a);
            }

            for (int i = 0; i < k; i++)
            {
                var c = NextInt() - 1;
                var d = NextInt() - 1;
                blocked[c].Add(d);
                blocked[d].Add(c);
            }

            for (int i = 0; i < n; i++)
            {
                var s = uf.Size(i) - 1;
                var b = blocked[i];
                s -= b.Count(j => uf.IsSame(i, j));

                Console.Write(s);
                Console.Write(" ");
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
    public class UnionFind
    {
        private int[] _data;
        
        public UnionFind(int size)
        {
            _data = Enumerable.Repeat(-1, size).ToArray();
        }

        public bool IsSame(int x, int y)
        {
            return Root(x) == Root(y);
        }

        public void Unite(int x , int y)
        {
            x = Root(x);
            y = Root(y);

            if (x == y) return;

            if (_data[x] > _data[y])
            {
                var t = x;
                x = y;
                y = t;
            }
            _data[x] += _data[y];
            _data[y] = x;
        }

        public int Size(int x)
        {
            return -_data[Root(x)];
        }

        private int Root(int x)
        {
            return _data[x] < 0 ? x : _data[x] = Root(_data[x]);
        }
        
    }
}
