using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Library
{
    static class ZAlgorithm
    {
        /// <summary>
        /// s と その各部分文字列(s[i:|s|-1] との最長共通接頭辞の長さ をもつ配列a を構築
        /// a[0]==|s|
        /// </summary>
        /// <param name="s"></param>
        public static int[] Solve(string s)
        {
            var len = s.Length;

            var a = new int[len];
            a[0] = len;
            var i = 1;
            var j = 0;
            while (i < len)
            {
                while (i + j < len && s[j] == s[i + j]) j++;
                a[i] = j;

                if (j == 0)
                {
                    i++;
                    continue;
                }

                var k = 1;
                while (i + k < len && k + a[k] < j)
                {
                    a[i + k] = a[k];
                    k++;
                }
                i += k;
                j -= k;
            }

            return a;
        }
    }
}
