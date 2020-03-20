using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Library
{
    class Graph
    {
        private int _size;
        private List<int>[] _edges;

        public Graph(int size)
        {
            _size = size;
            _edges = new List<int>[size];
        }

        public List<int> this[int i]
        {
            get
            {
                if (_edges[i] == null) _edges[i] = new List<int>();
                return _edges[i];
            }
        }
        public static Graph<T> NewGraph<T>(int size, T template)
        {
            return new Graph<T>(size);
        }
    }
    class Graph<T>
    {
        private int _size;
        private List<T>[] _edges;

        public Graph(int size)
        {
            _size = size;
            _edges = new List<T>[size];
        }

        public List<T> this[int i]
        {
            get
            {
                if (_edges[i] == null) _edges[i] = new List<T>();
                return _edges[i];
            }
        }
    }
}
