using AngleSharp;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AtCoder.Executors
{
    class LiveTestCaseProvider : ITestCaseProvider
    {
        public string DownloadFolder { get; set; } = "Live";
        public string ProbremURL { get; set; } = "https://atcoder.jp/contests/{0}/tasks/{0}_{1}";

        public IEnumerable<ITestCase> Create(Probrem probrem)
        {
            var di = Directory.CreateDirectory(Path.Combine(DownloadFolder, probrem.Contest, probrem.ID.ToString()));

            if (!di.EnumerateFiles(DownloadFolder).Any())
            {
                DownloadTestCase(di.FullName, probrem.ID.ToString(), string.Format(ProbremURL, probrem.Contest, probrem.ID.ToString()));
            }

            foreach (var file in di.EnumerateFiles())
            {
                yield return LoadTestCase(file.FullName);
            }
        }

        private LiveTestCase LoadTestCase(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            var input = new StringBuilder();
            var output = new StringBuilder();
            var isInput = true;

            foreach (var line in lines)
            {
                if (isInput)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        isInput = false;
                        continue;
                    }

                    input.AppendLine(line);
                }
                else
                {
                    output.AppendLine(line);
                }
            }

            var testCase = new LiveTestCase()
            {
                Name = filePath.Substring(filePath.LastIndexOf("\\")),
                Input = input.ToString(),
                Output = output.ToString()
            };

            return testCase;
        }

        private void DownloadTestCase(string folder, string name, string url)
        {
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            var document = context.OpenAsync(url).Result;
            //var content = cl.GetAsync(url).Result.Content.ReadAsStreamAsync().Result;
            //var parser = new HtmlParser();
            //var dom = parser.ParseDocument(content);
            var taskStatement = document.QuerySelector("div#task-statement");
            //var pres = taskStatement.QuerySelectorAll("div.div-btn-copy + pre");
            var h3s = taskStatement.QuerySelectorAll("h3");
            var pres = h3s.Where(h => h.TextContent.StartsWith("入力例") || h.TextContent.StartsWith("出力例"))
                          .Select(h => h.NextSibling).ToArray();

            for (int i = 0; i < pres.Length / 2; i++)
            {
                var inputNode = pres[i * 2];
                var outputNode = pres[i * 2 + 1];

                File.WriteAllText(Path.Combine(folder, $"testcase{i+1}.txt"), inputNode.TextContent + Environment.NewLine + outputNode.TextContent);
            }

        }

    }
}
