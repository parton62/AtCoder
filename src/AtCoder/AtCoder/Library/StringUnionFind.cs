using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Library
{
    class StringUnionFind
    {
        private Dictionary<string, string> _parent = new Dictionary<string, string>();
        private Dictionary<string, int> _size = new Dictionary<string, int>();

        public bool IsSame(string x, string y)
        {
            return Root(x) == Root(y);
        }
        public void Unit(string x, string y)
        {
            x = Root(x);
            y = Root(y);

            if (x == y) return;

            var sx = Size(x);
            var sy = Size(y);
            if (sx < sy)
            {
                var t = x;
                x = y;
                y = t;
            }

            _size[x] += _size[y];
            _size.Remove(y);

            _parent[y] = x;
        }

        private string Root(string x)
        {
            return _parent.TryGetValue(x, out var r) ? _parent[x] = Root(r) : x;
        }

        private int Size(string x)
        {
            return _size.TryGetValue(x, out var s) ? s : _size[x] = 1;
        }
    }
}
