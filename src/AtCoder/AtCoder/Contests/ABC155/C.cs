using System;
using System.Linq;
using System.Collections.Generic;

namespace AtCoder.Contests.ABC155
{
    /// <summary>
    /// 
    /// </summary>
    static class C
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            
            var dic = new CountDic<string>();

            var max = 0;
            for (int i = 0; i < n; i++)
            {
                max = Math.Max(max, dic.CountUp(Next()));
            }

            foreach (var s in dic.Where(kv => kv.Value == max).Select(kv => kv.Key).OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine(s);
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
    class CountDic<T> : IDictionary<T, int>
    {
        private Dictionary<T, int> _dic = new Dictionary<T, int>();
        public int this[T key]
        {
            get
            {
                return Get(key);
            }

            set
            {
                _dic[key] = value;
            }
        }

        public ICollection<T> Keys
        {
            get
            {
                return _dic.Keys;
            }
        }

        public ICollection<int> Values
        {
            get
            {
                return _dic.Values;
            }
        }

        public int Count
        {
            get
            {
                return _dic.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(T key, int value)
        {
            _dic.Add(key, value);
        }

        public void Add(KeyValuePair<T, int> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _dic = new Dictionary<T, int>();
        }

        public bool Contains(KeyValuePair<T, int> item)
        {
            return _dic.Contains(item);
        }

        public bool ContainsKey(T key)
        {
            return _dic.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<T, int>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            return _dic.GetEnumerator();
        }

        public bool Remove(T key)
        {
            return _dic.Remove(key);
        }

        public bool Remove(KeyValuePair<T, int> item)
        {
            return _dic.Remove(item.Key);
        }

        public bool TryGetValue(T key, out int value)
        {
            value = Get(key);
            return true;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dic.GetEnumerator();
        }

        private int Get(T key)
        {
            int c;
            if (_dic.TryGetValue(key, out c))
            {
                return c;
            }
            else
            {
                return 0;
            }
        }
        public int CountUp(T key)
        {
            var c = Get(key) + 1;
            _dic[key] = c;
            return c;
        }
    }
}
