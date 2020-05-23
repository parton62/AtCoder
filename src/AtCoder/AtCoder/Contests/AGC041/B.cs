using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AtCoder.Contests.AGC041
{
    /// <summary>
    /// 
    /// </summary>
    static class B
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var m = NextInt();
            var v = NextInt();
            var p = NextInt();
            var aas = ReadIntArray().ToList();
            aas.Sort();

            if (p >= v)
            {
                var threshold = aas[n - p];
                var i = BinarySearch.LowerBound(aas, threshold - m);
                Console.WriteLine(n - i);
                return;
            }
            else
            {
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
            var r = list.Count - 1;

            while (l <= r)
            {
                var m = l + (r - l) / 2;
                if (comp.Compare(value, list[m]) > 0)
                {
                    l = m + 1;
                }
                else
                {
                    r = m - 1;
                }
            }

            return l;
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
            var r = list.Count - 1;

            while (l <= r)
            {
                var m = l + (r - l) / 2;
                if (comp.Compare(value, list[m]) >= 0)
                {
                    l = m + 1;
                }
                else
                {
                    r = m - 1;
                }
            }
            return l;
        }
    }

    public class MinHeap<T> : IEnumerable<T>
    {
        private List<T> _list;
        private Comparer<T> _comp;

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public MinHeap() : this(0) { }
        public MinHeap(int capacity, Comparison<T> comp = null)
        {
            _list = new List<T>(capacity);

            if (comp == null)
            {
                _comp = Comparer<T>.Default;
            }
            else
            {
                _comp = Comparer<T>.Create(comp);
            }
        }

        public void Push(T elem)
        {
            Upheap(elem);
        }

        public T Pop()
        {
            return Downheap();
        }

        public T Peek()
        {
            return _list[0];
        }


        private void Swap(int i, int j)
        {
            var a = _list[j];
            _list[j] = _list[i];
            _list[i] = a;
        }

        private void Upheap(T elem)
        {
            _list.Add(elem);
            var i = _list.Count - 1;

            while (true)
            {
                if (i == 0) return;
                var j = (i - 1) / 2;

                var e = _list[i];
                var p = _list[j];

                var c = _comp.Compare(e, p);
                if (c < 0)
                {
                    Swap(i, j);
                    i = j;
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        private T Downheap()
        {
            var result = _list[0];

            var last = _list.Count - 1;
            Swap(0, last);
            _list.RemoveAt(last);
            if (_list.Count == 0) return result;

            var i = 0;
            while (true)
            {
                if (i == _list.Count - 1) return result;
                var p = _list[i];

                var j = i * 2 + 1;
                if (j >= _list.Count) return result;

                if (j + 1 < _list.Count && _comp.Compare(_list[j], _list[j + 1]) > 0)
                {
                    j++;
                }

                if (_comp.Compare(_list[i], _list[j]) > 0)
                {
                    Swap(i, j);
                    i = j;
                }
                else
                {
                    return result;
                }
            }
        }

        public MinHeap<T> Copy(Comparison<T> comp = null)
        {
            var c = new MinHeap<T>(0, comp);
            c._list = _list.ToList();

            return c;
        }
        public MinHeap(List<T> list)
        {
            _list = list;

            _comp = Comparer<T>.Default;
        }

        public List<T> GetList()
        {
            return _list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
