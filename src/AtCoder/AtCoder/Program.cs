using AtCoder.Executors;
using AtCoder.Library;
using System;
using System.Collections.Generic;

namespace AtCoder
{
    class Program
    {
        public static void Main(string[] args)
        {
            Execute(typeof(Contests.NOMURA2020.D));
        }

        static void ExecuteLive(Type probrem, int? index=null)
        {
            var e = new LiveExecutor();
            e.Index = index;
            e.Initialize(probrem);
            e.Execute();
        }
        
        static void Execute(Type probrem)
        {
            var e = new DebugExecutor();
            e.Initialize(probrem);
            e.Execute();
        }
    }
}
