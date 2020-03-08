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
        [DataRow(変数宣言のみ)]
        [DataRow(クラス参照途中)]
        [DataRow(変数参照)]
        [DataRow(いきなりa)]
        public void 何もせずスルー(string test)
        {
            VerifyCSharpDiagnostic(test);
        }
        [DataTestMethod]
        [DataRow(別名前空間Hoge参照, 10, 13, "Hoge")]
        public void ひっかかる(string test, int line, int column, string name)
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


        private const string 変数宣言のみ = @"
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

        private const string クラス参照途中 = @"
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
        private const string 変数参照 = @"
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
        private const string いきなりa = @"
using System;

namespace ConsoleApplication1
{
    class TypeName
    {
        a
    }
}
";
        private const string 別名前空間Hoge参照 = @"
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
