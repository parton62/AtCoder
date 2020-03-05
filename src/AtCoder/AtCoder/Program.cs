using System;
using System.Collections.Generic;

namespace AtCoder
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample();
        }

        static void Sample()
        {
            var dic = new Dictionary<string, string>();
            
            var a = new List<(string Source, string Dest)>()
            {
                ("a","b"),
                ("a","b"),
                ("a","b"),
                ("b","c"),
                ("c","d"),
                ("e","d"),
                ("f","g"),
                ("h","g"),
                ("i","j"),
            };

            Func<string, string> fd = null;
            fd = (string x) =>
            {
                return dic.TryGetValue(x, out var f) ? dic[x] = fd(f) : x;
            };

            foreach (var item in a)
            {
                var s = item.Source;
                var d = item.Dest;

                dic[s] = fd(d);
            }


            foreach (var item in a)
            {
                Console.WriteLine($"依存元:{item.Source}, 　　依存先:{item.Dest}");
            }
            Console.WriteLine();

            foreach (var item in a)
            {
                Console.WriteLine($"依存元:{item.Source}, 最終依存先:{fd(item.Source)}");
            }
        }
    }
}
