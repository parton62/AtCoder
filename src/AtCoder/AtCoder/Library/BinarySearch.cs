using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Library
{
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
            var r = list.Count;

            while (l + 1 < r)
            {
                var m = l + (r - l) / 2;
                if (comp.Compare(value, list[m]) > 0)
                {
                    l = m;
                }
                else
                {
                    r = m;
                }
            }

            return r;
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
            var r = list.Count;

            while (l + 1 < r)
            {
                var m = l + (r - l) / 2;
                if (comp.Compare(value, list[m]) >= 0)
                {
                    l = m;
                }
                else
                {
                    r = m;
                }
            }
            return r;
        }
    }
}
