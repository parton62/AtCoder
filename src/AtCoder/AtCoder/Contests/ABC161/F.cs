using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC161
{
    /// <summary>
    /// 
    /// </summary>
    static class F
    {
        static void Main(string[] args)
        {
            var n = NextLong();

            //n = k^i * (j*k+1) とかける k の数を探す

            if(n == 2)
            {
                Console.WriteLine(1);
                return;
            }

            var count = GetDivisors(n - 1).Count;

            foreach (var d in GetDivisors(n))
            {
                var num = n;
                while(num % d == 0)
                {
                    num /= d;
                }
                if (num % d == 1) count++;
            }
            
            Console.WriteLine(count + 2);
        }
        static HashSet<long> GetDivisors(long n)
        {
            var f = Math.Sqrt(n);
            var d = new HashSet<long>();
            for (int i = 2; i <= f; i++)
            {
                if (n % i != 0) continue;
                d.Add(i);
                d.Add(n / i);
            }
            return d;
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
