using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AtCoder.Executors
{
    class LiveTestCase : ITestCase
    {
        public string Name { get; set; }

        public string Input { get; set; }
        public string Output { get; set; }


        public TextReader GetInputStream()
        {
            return new StringReader(Input);
        }

        public TextReader GetOutputStream()
        {
            return new StringReader(Output);
        }
    }
}
