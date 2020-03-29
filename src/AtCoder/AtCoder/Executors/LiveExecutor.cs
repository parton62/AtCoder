using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AtCoder.Executors
{
    class LiveExecutor : Executor
    {
        public int? Index { get; set; } = null;

        public override void Execute()
        {
            Console.WriteLine(Probrem.Contest + " " + Probrem.ID);
            var d = new LiveTestCaseProvider();

            var index = 1;
            foreach (var test in d.Create(Probrem))
            {
                if (Index.HasValue && Index.Value != index++) continue;

                var display = new StringBuilder();
                display.AppendLine(test.Name);

                var output = new StringBuilder();
                using (var w = new StringWriter(output))
                {
                    Console.SetIn(test.GetInputStream());
                    Console.SetOut(w);

                    var sw = new Stopwatch();
                    sw.Start();
                    ExecuteMain();
                    sw.Stop();

                    var result = output.ToString().Trim();
                    var answer = test.GetOutputStream().ReadToEnd().Trim();

                    display.AppendLine("result:").AppendLine(result);
                    display.AppendLine("answer:").AppendLine(answer);

                    display.Append("elapsed:").AppendLine(sw.Elapsed.TotalSeconds.ToString());

                    if (result == answer)
                    {
                        display.AppendLine("---AC---");
                    }
                    else
                    {
                        display.AppendLine("---WA---");
                    }
                    display.AppendLine();
                }

                using (var w = new StreamWriter(Console.OpenStandardOutput()))
                {
                    Console.SetOut(w);
                    Console.Write(display.ToString());
                }
            }
            
        }
    }
}
