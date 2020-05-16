using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AtCoder.Library
{
    class CountDic<T> : IDictionary<T, int>
    {
        private Dictionary<T, int> _dic = new Dictionary<T, int>();
        public int this[T key] 
        {
            get => Get(key);
            set => _dic[key] = value;
        }

        public ICollection<T> Keys => _dic.Keys;

        public ICollection<int> Values => _dic.Values;

        public int Count => _dic.Count;

        public bool IsReadOnly => false;

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

        public bool TryGetValue(T key, [MaybeNullWhen(false)] out int value)
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
