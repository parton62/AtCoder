using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            if (!di.EnumerateFiles(DownloadFolder).Any())
            {
                //var context = CreateContext();
                //context.SetCookie(new Url("https://atcoder.jp"), @"REVEL_SESSION=3be5de2287655806e811991ff58b71c2cff1eb98-%00csrf_token%3A2%2FzAK%2BS7ZZ1YWwkfJJfA98BrfGLAg%2BSIBcnnxWTRqE8%3D%00%00SessionKey%3A31deb3e3b2801d69ef5e0dca56ca35e18ae1d41015cc13d49775f38c186c276d9c57db2f02c7ec-7a51ec3706f79d4d86483d16930695090f8f0cfdc032da58d61f2a0234a06152%00%00UserScreenName%3Aparton%00%00Rating%3A1217%00%00a%3Afalse%00%00w%3Afalse%00%00_TS%3A1600792075%00%00UserName%3Aparton%00");
                //DownloadTestCase(context, di.FullName, probrem.ID.ToString(), string.Format(ProbremURL, probrem.Contest, probrem.ID.ToString()));
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

        //static private IBrowsingContext CreateContext()
        //{
        //    var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
        //    return context;
        //}

        //static private string Login(IBrowsingContext context, string username, string password)
        //{
        //    var document = context.OpenAsync(LoginURL).Result;
        //    var hidden = document.QuerySelector("form input[name=\"csrf_token\"]");
        //    var token = hidden.GetAttribute("value");

        //    var form = context.Active.QuerySelector("#main-container form") as IHtmlFormElement;
        //    document = form.SubmitAsync(new { username, password, csrf_token = token }).Result;

        //    var cookies = context.Active.Cookie;
        //    return cookies;
        //}

        //private void DownloadTestCase(IBrowsingContext context, string folder, string name, string url)
        //{   
        //    var document = context.OpenAsync(url).Result;
        //    var taskStatement = document.QuerySelector("div#task-statement");
        //    var h3s = taskStatement.QuerySelectorAll("h3");
        //    var pres = h3s.Where(h => h.TextContent.StartsWith("入力例") || h.TextContent.StartsWith("出力例"))
        //                  .Select(h => h.NextSibling).ToArray();

        //    for (int i = 0; i < pres.Length / 2; i++)
        //    {
        //        var inputNode = pres[i * 2];
        //        var outputNode = pres[i * 2 + 1];

        //        File.WriteAllText(Path.Combine(folder, $"testcase{i+1}.txt"), inputNode.TextContent + Environment.NewLine + outputNode.TextContent);
        //    }

        //}

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

    //class ChromeCookie
    //{
    //    static public void GetGookie()
    //    {
    //        var hostName = "atcoder.jp";

    //        var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default\Cookies";

    //        if (!File.Exists(dbPath)) return;

    //        //var connectionString = $"Data Source={dbPath};";
    //        var sb = new SqliteConnectionStringBuilder()
    //        {
    //            DataSource = dbPath,
    //        };
    //        //sb.Ver
    //        using (var con = new SqliteConnection($"Data Source={dbPath};"))
    //        using (var com = con.CreateCommand())
    //        {
    //            var prm = com.CreateParameter();
    //            prm.ParameterName = "hostName";
    //            prm.Value = hostName;
    //            com.Parameters.Add(prm);

    //            com.CommandText = "SELECT name, encrypted_value FROM cookies WHERE host_key = @hostName";

    //            con.Open();
                
    //            using (var reader = com.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    //Console.WriteLine(reader[0] + ":" + reader[1]);
    //                    var encryptedData = (byte[])reader[1];
    //                    var decodedData = System.Security.Cryptography.ProtectedData.Unprotect(encryptedData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
    //                    var plainText = Encoding.ASCII.GetString(decodedData);

    //                    Console.WriteLine(" " + plainText);
    //                }
    //            }
    //        }
    //    }


    //}
}
