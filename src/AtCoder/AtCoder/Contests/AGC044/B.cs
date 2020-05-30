using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.AGC044
{
    /// <summary>
    /// 
    /// </summary>
    static class B
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var ps = ReadIntArray();
            var nn = new bool[n + 2, n + 2];

            var mm = new int[n + 2, n + 2];

            for (int i = 2; i < n; i++)
            {
                for (int j = 2; j < n; j++)
                {
                    mm[i, j] = Math.Min(Math.Min(i - 1, n - i), Math.Min(j - 1, n - j));
                }
            }

            long sum = 0;
            var q = new Queue<P>();
            for (int i = 0; i < n * n; i++)
            {
                var pp = ps[i];
                var p = new P((pp-1) / n + 1,(pp-1) % n + 1);

                sum += mm[p.x, p.y];
                nn[p.x, p.y] = true;

                q.Enqueue(p);

                while (q.Count > 0)
                {
                    var c = q.Dequeue();
                    var x = c.x;
                    var y = c.y;
                    var cc = mm[x, y] + (nn[x, y] ? 0 : 1);
                    
                    if (x > 1 && mm[x - 1, y] > cc)
                    {
                        mm[x - 1, y] = cc;
                        q.Enqueue(new P(x - 1, y));
                    }
                    if (x < n && mm[x + 1, y] > cc)
                    {
                        mm[x + 1, y] = cc;
                        q.Enqueue(new P(x + 1, y));
                    }
                    if (y > 1 && mm[x, y - 1] > cc)
                    {
                        mm[x, y - 1] = cc;
                        q.Enqueue(new P(x, y - 1));
                    }
                    if (y < n && mm[x, y + 1] > cc)
                    {
                        mm[x, y + 1] = cc;
                        q.Enqueue(new P(x, y + 1));
                    }
                }
            }

            Console.WriteLine(sum);
        }
        class P
        {
            public int x;
            public int y;

            public P(int x, int y)
            {
                this.x = x;
                this.y = y;
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
        static int[] ReadIntArray()
        {
            return Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        }
        #endregion
    }
}
