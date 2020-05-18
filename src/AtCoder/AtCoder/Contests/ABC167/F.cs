using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC167
{
    /// <summary>
    /// 
    /// </summary>
    static class F
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var plus = new List<K>(n);
            var minus = new List<K>(n);

            for (int i = 0; i < n; i++)
            {
                var s = Next();
                var open = 0;
                var close = 0;
                for (int j = 0; j < s.Length; j++)
                {
                    var c = s[j];
                    if (c == '(')
                    {
                        open++;
                    }
                    else
                    {
                        if (open > 0)
                        {
                            open--;
                        }
                        else
                        {
                            close++;
                        }
                    }

                }

                if(open >= close)
                {
                    plus.Add(new K() { Open = open, Close = close });
                }
                else if(open < close)
                {
                    minus.Add(new K() { Open = open, Close = close });
                }
            }

            //open と close の数がずれてる場合
            if (plus.Sum(k => k.Open - k.Close) != minus.Sum(k => k.Close - k.Open))
            {
                Console.WriteLine("No");
                return;
            }

            var comp = Comparer<K>.Create((a, b) => a.Close.CompareTo(b.Close));
            plus.Sort(comp);

            var o = 0;

            foreach (var k in plus)
            {
                if(k.Close > o)
                {
                    Console.WriteLine("No");
                    return;
                }

                o += k.Open - k.Close;
            }

            o = 0;

            comp = Comparer<K>.Create((a, b) => a.Open.CompareTo(b.Open));
            minus.Sort(comp);

            foreach (var k in minus)
            {
                if (k.Open > o)
                {
                    Console.WriteLine("No");
                    return;
                }

                o += k.Close - k.Open;
            }


            Console.WriteLine("Yes");
        }

        struct K
        {
            public int Open;
            public int Close;
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
