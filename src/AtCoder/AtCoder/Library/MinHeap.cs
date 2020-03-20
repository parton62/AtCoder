using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoder.Library
{

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
