using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AtCoder.Executors
{
    interface ITestCase
    {
        string Name { get; set; }
        TextReader GetInputStream();
        TextReader GetOutputStream();
    }
}
