using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtCoder.Executors
{
    interface ITestCaseProvider
    {
        IEnumerable<ITestCase> Create(Probrem probrem);
    }
}
