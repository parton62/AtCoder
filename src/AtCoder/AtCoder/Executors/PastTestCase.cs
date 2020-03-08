using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AtCoder.Executors
{
    class PastTestCase : ITestCase
    {
        //public Probrem Probrem { get; set; }
        public string SourcePath { get; set; }

        public string Name { get; set; }

        public TextReader GetInputStream()
        {
            throw new NotImplementedException();
        }

        public TextReader GetOutputStream()
        {
            throw new NotImplementedException();
        }
    }
}
