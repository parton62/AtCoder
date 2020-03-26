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

            var chars = Enumerable.Range(0, 26).Select(i => new List<int>()).ToArray();
            for (int i = 0; i < n; i++)
            {
                var c = s[i] - 'a';
                chars[c].Add(i);
            }

            for (int j = 0; j < q; j++)
            {
                var type = NextInt();
                if (type == 1)
                {
                    var i = NextInt() - 1;
                    var c = Next();

                    for (int k = 0; k < 26; k++)
                    {
                        var cs = chars[k];
                        var found = cs.BinarySearch(i);
                        if (k == c[0] - 'a')
                        {
                            if(found < 0)
                            {
                                cs.Insert(~found, i);
                            }
                        }
                        else
                        {
                            if (found >= 0)
                            {
                                cs.RemoveAt(found);
                            }
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
                        var li = BinarySearch.LowerBound(cs, l);
                        var ri = BinarySearch.UpperBound(cs, r);

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
    class BinarySearch
    {
        /// <summary>
        /// value 以上となる値を持つ最小のindex
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static int LowerBound<T>(IList<T> list, T value, IComparer<T> comp = null)
        {
            if (comp == null) comp = Comparer<T>.Default;

            var l = 0;
            var r = list.Count;

            while (l + 1 < r)
            {
                var m = l + (r - l) / 2;
                if (comp.Compare(value, list[m]) > 0)
                {
                    l = m;
                }
                else
                {
                    r = m;
                }
            }

            return r;
        }
        /// <summary>
        /// value より大きい値を持つ最小のindex
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static int UpperBound<T>(IList<T> list, T value, IComparer<T> comp = null)
        {
            if (comp == null) comp = Comparer<T>.Default;

            var l = 0;
            var r = list.Count;

            while (l + 1 < r)
            {
                var m = l + (r - l) / 2;
                if (comp.Compare(value, list[m]) >= 0)
                {
                    l = m;
                }
                else
                {
                    r = m;
                }
            }
            return r;
        }
    }
}
