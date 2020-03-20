using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;
using CopyCoder;

namespace CopyCoder.Test
{
    [TestClass]
    public class UnitTest : CodeFixVerifier
    {

        //No diagnostics expected to show up
        [DataTestMethod]
        [DataRow(�ϐ��錾�̂�)]
        [DataRow(�N���X�Q�Ɠr��)]
        [DataRow(�ϐ��Q��)]
        [DataRow(�����Ȃ�a)]
        [DataRow(ZAlgorithm)]
        public void ���������X���[(string test)
        {
            VerifyCSharpDiagnostic(test);
        }
        [DataTestMethod]
        [DataRow(�ʖ��O���Hoge�Q��, 10, 13, "Hoge")]
        public void �Ђ�������(string test, int line, int column, string name)
        {
            var expected = new DiagnosticResult
            {
                Id = CopyCoderAnalyzer.DiagnosticId,
                Message = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources), name).ToString(),
                Severity = DiagnosticSeverity.Info,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", line, column) }
            };
            
            VerifyCSharpDiagnostic(test, expected);
        }
        [DataTestMethod]
        [DataRow(�ʖ��O���Hoge�Q��, �ʖ��O���Hoge�Q��fix)]
        public void �u������(string source, string edited)
        {
            VerifyCSharpFix(source, edited, allowNewCompilerDiagnostics:true);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new CopyCoderCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new CopyCoderAnalyzer();
        }


        private const string �ϐ��錾�̂� = @"
using System;

namespace ConsoleApplication1
{
    class TypeName
    {
        void Method()
        {
            var a = 1;
            Console.WriteLine(a);
        }
    }
}
";

        private const string �N���X�Q�Ɠr�� = @"
using System;

namespace ConsoleApplication1
{
    class TypeName
    {
        void Method()
        {
            Hoge
        }
    }
    class Hoge { }
}
";
        private const string �ϐ��Q�� = @"
using System;

namespace ConsoleApplication1
{
    class TypeName
    {
        void Method()
        {
            var a = 1;
            a
        }
    }
}
";
        private const string �����Ȃ�a = @"
using System;

namespace ConsoleApplication1
{
    class TypeName
    {
        a
    }
}
";
        private const string �ʖ��O���Hoge�Q�� = @"
using System;

namespace ConsoleApplication1
{
    class TypeName
    {
        void Method()
        {
            Hoge
        }
    }
}
namespace ConsoleApplication2
{
    class Hoge { }
}
";
        private const string �ʖ��O���Hoge�Q��fix = @"
using System;

namespace ConsoleApplication1
{
    class TypeName
    {
        void Method()
        {
            Hoge
        }
    }
    class Hoge { }
}
namespace ConsoleApplication2
{
    class Hoge { }
}
";

        private const string ZAlgorithm = @"using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Library
{
    static class ZAlgorithm
    {
        /// <summary>
        /// s �� ���̊e����������(s[i:|s|-1] �Ƃ̍Œ����ʐړ����̒��� �����z��a ���\�z
        /// a[0]==|s|
        /// </summary>
        /// <param name=""s""></param>
        public static int[] Solve(string s)
    {
        var len = s.Length;

        var a = new int[len];
        a[0] = len;
        var i = 1;
        var j = 0;
        while (i < len)
        {
            while (i + j < len && s[j] == s[i + j]) j++;
            a[i] = j;

            if (j == 0)
            {
                i++;
                continue;
            }

            var k = 1;
            while (i + k < len && k + a[k] < j)
            {
                a[i + k] = a[k];
                k++;
            }
            i += k;
            j -= k;
        }

        return a;
    }
}
}
";
    }


}
