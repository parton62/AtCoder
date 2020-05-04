using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC166
{
    /// <summary>
    /// 
    /// </summary>
    static class F
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var abc = new[] { NextInt(), NextInt(), NextInt() };
            var ss = Enumerable.Range(0, n).Select(s => Next()).ToArray();

            var sum = abc.Sum();
            if (sum == 0)
            {
                Console.WriteLine("No");
                return;
            }
            else if (sum == 1)
            {
                var results = new List<char>();
                for (int i = 0; i < n; i++)
                {
                    var x = ss[i][0] - 'A';
                    var y = ss[i][1] - 'A';

                    if(abc[x] == 0 && abc[y] == 0)
                    {
                        Console.WriteLine("No");
                        return;
                    }

                    if (abc[x] > 0)
                    {
                        abc[x]--;
                        abc[y]++;
                        results.Add((char)(y + 'A'));
                    }
                    else
                    {
                        abc[x]++;
                        abc[y]--;
                        results.Add((char)(x + 'A'));
                    }
                }
                Console.WriteLine("Yes");
                foreach (var r in results)
                {
                    Console.WriteLine(r);
                }
                return;
            }
            else
            {
                var x = ss[0][0] - 'A';
                var y = ss[0][1] - 'A';
                if (abc[x] == 0 && abc[y] == 0)
                {
                    Console.WriteLine("No");
                    return;
                }

                Console.WriteLine("Yes");
                
                for (int i = 0; i < n; i++)
                {
                    x = ss[i][0] - 'A';
                    y = ss[i][1] - 'A';

                    var needswap = false;
                    if (abc[x] == 0 || abc[y] == 0)
                    {
                        if (abc[y] == 0) needswap = true;
                    }
                    else if (sum == 2 && abc[x] == 1 && abc[y] == 1 && i < n - 1)
                    {
                        var next = ss[i + 1];
                        if (!next.Contains((char)(x + 'A'))) needswap = true;
                    }
                    else if(abc[x] > abc[y])
                    {
                        needswap = true;
                    }
                    if(needswap)
                    {
                        var z = y;
                        y = x;
                        x = z;
                    }

                    abc[x]++;
                    abc[y]--;
                    Console.WriteLine((char)(x + 'A'));
                }
                return;
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
