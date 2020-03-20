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
        [DataRow(変数宣言のみ)]
        [DataRow(クラス参照途中)]
        [DataRow(変数参照)]
        [DataRow(いきなりa)]
        [DataRow(ZAlgorithm)]
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
                Id = CopyCoderAnalyzer.DiagnosticId,
                Message = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources), name).ToString(),
                Severity = DiagnosticSeverity.Info,
                Locations = new[] { new DiagnosticResultLocation("Test0.cs", line, column) }
            };
            
            VerifyCSharpDiagnostic(test, expected);
        }
        [DataTestMethod]
        [DataRow(別名前空間Hoge参照, 別名前空間Hoge参照fix)]
        public void 置き換え(string source, string edited)
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
        private const string 別名前空間Hoge参照fix = @"
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
        /// s と その各部分文字列(s[i:|s|-1] との最長共通接頭辞の長さ をもつ配列a を構築
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
