using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC157
{
    /// <summary>
    /// 
    /// </summary>
    static class E
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var s = Next();
            var q = NextInt();

            var chars = Enumerable.Range(0, 26).Select(i => new SortedList<int, int>()).ToArray();
            for (int i = 0; i < n; i++)
            {
                var c = s[i] - 'a';
                chars[c].Add(i, i);
            }

            for (int j = 0; j < q; j++)
            {
                var type = NextInt();
                if (type == 1)
                {
                    var i = NextInt();
                    var c = Next();

                    for (int k = 0; k < 26; k++)
                    {
                        var cs = chars[k];
                        if (k == c[0] - 'a')
                        {
                            var found = cs.IndexOfKey(i);
                            if(found < 0)
                            {
                                cs.Add(i, i);
                            }
                        }
                        else
                        {
                            cs.Remove(i);
                        }
                    }
                }
                else
                {
                    var l = NextInt() - 1;
                    var r = NextInt() - 1;

                    var sum = 0;
                    for (int k = 0; k < 26; k++)
                    {
                        var cs = chars[k];
                        var li = cs.IndexOfKey(l);
                        if (li < 0) li = ~li;
                        var ri = cs.IndexOfKey(r);
                        if (ri < 0) ri = ~ri;

                        if (li < ri)
                        {
                            sum++;
                        }
                    }
                    Console.WriteLine(sum);
                }

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
