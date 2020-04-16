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


            for (int i = 0; i < Math.Pow(2, h-1); i++)
            {
                //横に折る個所を決める

                //一番下に仮想の折り目を入れる
                var bits = i + (int)Math.Pow(2, h - 1);
                var prevh = -1;

                var wsep = new bool[w];
                //wsep[0] = true;

                //仮想の折り目の分
                var count = -1;
                var goNext = false;
                for (int ih = 0; ih < h; ih++)
                {
                    //折る場所じゃなかったら次へ
                    if ((bits &1) == 0)
                    {
                        bits = bits >> 1;
                        continue;
                    }
                    count++;
                    
                    //折った上側について、縦に
                    var row = Enumerable.Range(0, w)
                             .Select(wi => Enumerable.Range(prevh+1, ih - prevh)
                                          .Count(hii => ss[hii][wi] == '1'))
                             .ToList();

                    goNext = false;
                    var block = 0;
                    for (int j = 0; j < w; j++)
                    {
                        if(row[j] > k)
                        {
                            //一つのセルで超えてしまう場合は次のh分割パターンへ
                            goNext = true;
                            break;
                        }
                        if (wsep[j])
                        {
                            block = row[j];
                            continue;
                        }

                        block += row[j];
                        if (block > k)
                        {
                            wsep[j] = true;
                            block = row[j];
                            count++;
                        }
                    }
                    //if(block > k)
                    //{
                    //    goNext = true;
                    //}

                    if (goNext) break;


                    bits = bits >> 1;
                    prevh = ih;
                }

                if (goNext) continue;

                min = Math.Min(min, count);
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
}
