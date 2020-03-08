using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Executors
{
    interface ITestCaseProvider
    {
        IEnumerable<ITestCase> Create(Probrem probrem);
    }
}
