using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace AtCoder.Contests.ABC163
{
    /// <summary>
    /// 
    /// </summary>
    static class E
    {
        static void Main(string[] args)
        {
            var n = NextInt();
            var aas = ReadLongArray().OrderByDescending(a => a).ToArray();

            //各幼児は元の位置から右へ動くか左へ動くかの二通りが考えられる
            //最初からn人のうち a 人が左、残りが右へ動いた際に次の一人が左右に動いた時のうれしさを考える
            //動かす際には、できるだけ活発な幼児から動かしたほうが良い

            

            var dp = new long[n + 1, n + 1];
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {

                }
            }


            var heap = new MaxHeap<Activity>(n, (x, y) => x.Point.CompareTo(y.Point));

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    heap.Push(new Activity(i, j, aas[i] * Math.Abs(i - j)));
                }
            }
            var children = new HashSet<int>();
            var lines = new HashSet<int>();

            long sum = 0;
            while (heap.Any())
            {
                var p = heap.Pop();
                if (children.Contains(p.Before)) continue;
                if (lines.Contains(p.After)) continue;

                sum += p.Point;
                children.Add(p.Before);
                lines.Add(p.After);
            }

            Console.WriteLine(sum);
        }

        struct Activity
        {
            public int Before;
            public int After;
            public long Point;

            public Activity(int b, int a, long p)
            {
                Before = b;
                After = a;
                Point = p;
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
    public class MaxHeap<T> : IEnumerable<T>
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

        public MaxHeap() : this(0) { }
        public MaxHeap(int capacity, Comparison<T> comp = null)
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
                if (c > 0)
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

                if (j + 1 < _list.Count && _comp.Compare(_list[j], _list[j + 1]) < 0)
                {
                    j++;
                }

                if (_comp.Compare(_list[i], _list[j]) < 0)
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

        public MaxHeap<T> Copy(Comparison<T> comp = null)
        {
            var c = new MaxHeap<T>(0, comp);
            c._list = _list.ToList();

            return c;
        }
        public MaxHeap(List<T> list)
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
