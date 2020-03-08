using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;
using CopyLibraryCode;

namespace CopyLibraryCode.Test
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
                Id = CopyLibraryCodeAnalyzer.DiagnosticId,
                Message = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources), name).ToString(),
                Severity = DiagnosticSeverity.Info,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", line, column) }
            };
            
            VerifyCSharpDiagnostic(test, expected);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new CopyLibraryCodeCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new CopyLibraryCodeAnalyzer();
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
    }


}
