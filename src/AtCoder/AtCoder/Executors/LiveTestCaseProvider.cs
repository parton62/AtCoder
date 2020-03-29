using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AtCoder.Executors
{
    class LiveTestCaseProvider : ITestCaseProvider
    {
        public string DownloadFolder { get; set; } = "Live";
        public string ProbremURL { get; set; } = "https://atcoder.jp/contests/{0}/tasks/{0}_{1}";
        static public string LoginURL { get; set; } = "https://atcoder.jp/login";


        public IEnumerable<ITestCase> Create(Probrem probrem)
        {
            var di = Directory.CreateDirectory(Path.Combine(DownloadFolder, probrem.Contest, probrem.ID.ToString()));

            if (!di.EnumerateFiles().Any())
            {
                using (var chrome = CreateDriver())
                {
                    Login(chrome);
                    Thread.Sleep(500);
                    DownloadTestCase(chrome, di.FullName, probrem.ID.ToString(), string.Format(ProbremURL, probrem.Contest, probrem.ID.ToString()));
                }
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

        private ChromeDriver CreateDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");

            var chrome = new ChromeDriver(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), options);
            return chrome;
        }

        private void Login(ChromeDriver chrome)
        {
            var cookiePath = "cookie.json";

            if (File.Exists(cookiePath))
            {
                chrome.Url = "https://google.co.jp/";
                Thread.Sleep(100);
                var cookie = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(File.ReadAllText(cookiePath));

                foreach (var c in cookie)
                {
                    chrome.Manage().Cookies.AddCookie(Cookie.FromDictionary(c));
                }
                return;
            }

            Console.WriteLine("input username:");
            var username = Console.ReadLine();
            Console.WriteLine("input password");
            var password = Console.ReadLine();

            chrome.Url = LoginURL;
            chrome.FindElementById("username").SendKeys(username);
            Thread.Sleep(100);
            chrome.FindElementById("password").SendKeys(password);
            Thread.Sleep(100);
            chrome.FindElementById("submit").Click();
            Thread.Sleep(500);
            
            var cookies = chrome.Manage().Cookies.AllCookies;
            File.WriteAllText(cookiePath, JsonConvert.SerializeObject(cookies));
        }
        private void DownloadTestCase(ChromeDriver chrome, string folder, string name, string url)
        {
            chrome.Url = url;
            Thread.Sleep(200);
            var taskStatement = chrome.FindElementById("task-statement");
            var ja = taskStatement.FindElement(By.ClassName("lang-ja"));
            var pres = ja.FindElements(By.TagName("pre")).Where(p => p.GetAttribute("id").StartsWith("pre-sample")).ToList();

            for (int i = 0; i < pres.Count / 2; i++)
            {
                var inputNode = pres[i * 2];
                var outputNode = pres[i * 2 + 1];

                var path = Path.Combine(folder, $"testcase{i + 1}.txt");
                File.WriteAllText(path, inputNode.Text.Trim() + Environment.NewLine + Environment.NewLine + outputNode.Text.Trim());
            }
        }
    }
}
