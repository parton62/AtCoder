using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Executors
{
    class DebugExecutor : Executor
    {
        public override void Execute()
        {
            while (true)
            {
                ExecuteMain();
                Console.WriteLine();
            }
        }
    }
}
