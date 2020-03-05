using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoder
{
    public class UnionFind
    {
        private int[] _data;

        public UnionFind(int size)
        {
            _data = Enumerable.Repeat(-1, size).ToArray();
        }

        public bool IsSame(int x, int y)
        {
            return Root(x) == Root(y);
        }

        public void Unite(int x , int y)
        {
            x = Root(x);
            y = Root(y);

            if (x == y) return;

            if (_data[x] > _data[y])
            {
                var t = x;
                x = y;
                y = t;
            }
            _data[x] += _data[y];
            _data[y] = x;
        }

        private int Root(int x)
        {
            return _data[x] < 0 ? x : _data[x] = Root(_data[x]);
        }

    }
}
