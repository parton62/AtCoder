using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AtCoder.Executors
{
    class LiveExecutor : Executor
    {
        //private static string _probremURL = "https://atcoder.jp/contests/{0}/tasks/{0}_{1}";

        //public string DownloadedTestCaseFolder { get; set; } = "Live";


        public override void Execute()
        {
            //base.Execute();


            var d = new LiveTestCaseProvider();
            foreach (var test in d.Create(Probrem))
            {
                var display = new StringBuilder();
                display.AppendLine(test.Name);

                using (var s = new MemoryStream())
                using (var w = new StreamWriter(s, Encoding.ASCII))
                {
                    Console.SetIn(test.GetInputStream());
                    Console.SetOut(w);
                    //writer.Write(test.GetInputStream().ReadToEnd());
                    var sw = new Stopwatch();
                    sw.Start();
                    ExecuteMain();
                    sw.Stop();
                    w.Flush();
                    
                    var result = Encoding.ASCII.GetString(s.ToArray()).Trim();
                    //var result = reader.ReadToEnd();
                    var answer = test.GetOutputStream().ReadToEnd().Trim();

                    display.Append("result:").AppendLine(result);
                    display.Append("answer:").AppendLine(answer);

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

        //private string GetDownloadFolderPath()
        //{
        //    return Path.Combine(DownloadedTestCaseFolder, Probrem.Contest);
        //}
        //private string GetProbremURL()
        //{
        //    return string.Format(_probremURL, Probrem.Contest.ToLower(), Probrem.ID.ToString().ToLower());
        //}
        



    }
}
