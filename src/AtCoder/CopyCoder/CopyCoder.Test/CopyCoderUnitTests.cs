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
        [DataRow(���O��ԂȂ�, ���O��ԂȂ�fix)]
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
    }


}
