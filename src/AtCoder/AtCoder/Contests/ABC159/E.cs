using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC159
{
    /// <summary>
    /// 
    /// </summary>
    static class E
    {
        static void Main(string[] args)
        {
            var h = NextInt();
            var w = NextInt();
            var k = NextInt();
            var ss = new string[h];
            for (int i = 0; i < h; i++)
            {
                ss[i] = Next();
            }

            var min = int.MaxValue;

            foreach (var i in Bits.Enumerate(h - 1))
            {
                //一番下に折り目を入れる
                var bits = i + (int)Math.Pow(2, h - 1);

                //一列ごとに縦方向の和を求める
                var panels = Enumerable.Range(0, w).Select(_ => new List<int>()).ToArray();
                for (int j = 0; j < w; j++)
                {
                    var panel = panels[j];
                    var sum = 0;

                    for (int b = 0; b < h; b++)
                    {
                        if (ss[b][j] == '1') sum++;
                        if (bits.HasFlag(b))
                        {
                            panel.Add(sum);
                            sum = 0;
                        }
                    }
                }

                var blockCount = panels[0].Count;

                var count = 0;
                var counts = new int[blockCount];
                var goNext = false;

                for (int j = 0; j < w; j++)
                {
                    var needFold = false;
                    for (int l = 0; l < blockCount; l++)
                    {
                        var c = panels[j][l];
                        if(c > k)
                        {
                            goNext = true;
                            break;
                        }

                        counts[l] += c;
                        if (counts[l] > k) needFold = true;
                    }

                    if (goNext) break;
                    if (needFold)
                    {
                        for (int l = 0; l < blockCount; l++) counts[l] = panels[j][l];
                        count++;
                    }
                }

                if (goNext) continue;

                min = Math.Min(min, count + blockCount - 1);
            }
            Console.WriteLine(min);
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
    static class Bits
    {
        public static IEnumerable<int> Enumerate(int bitDigits)
        {
            return Enumerable.Range(0, (int)Math.Pow(2, bitDigits));
        }

        public static bool HasFlag(this int bits, int index)
        {
            return (bits >> index & 1) == 1;
        }
    }
}
