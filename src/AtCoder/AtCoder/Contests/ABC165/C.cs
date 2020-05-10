using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC165
{
    /// <summary>
    /// 
    /// </summary>
    static class C
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var m = NextInt();
            var q = NextInt();
            var abcds = ReadObjects(q, i => i);

            var nums = Enumerable.Repeat(1, n).ToArray();
            nums[n - 1] = 0;

            var max = -1;
            while (NextNums(nums, m, n - 1))
            {
                max = Math.Max(max, abcds.Where(abcd => nums[abcd[1] - 1] - nums[abcd[0] - 1] == abcd[2]).Sum(abcd => abcd[3]));
            }

            Console.WriteLine(max);
        }

        static bool NextNums(int[] nums, int max, int digit)
        {
            if (digit == -1) return false;
            if(nums[digit] < max)
            {
                nums[digit]++;
                return true;
            }
            if (NextNums(nums, max, digit - 1))
            {
                nums[digit] = nums[digit - 1];
                return true;
            }
            return false;
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
