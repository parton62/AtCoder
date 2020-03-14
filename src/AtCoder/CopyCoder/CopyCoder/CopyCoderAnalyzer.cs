using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CopyCoder
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CopyCoderAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "CopyLibraryCode";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Info, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information

            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.IdentifierName);
        }
        
        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var identifier = (IdentifierNameSyntax)context.Node;

            if (identifier.IsVar) return;

            var errorType = context.SemanticModel.GetTypeInfo(identifier).Type as IErrorTypeSymbol;
            if (errorType == null) return;

            var ns = identifier.Ancestors().OfType<NamespaceDeclarationSyntax>().First().Name.ToString();
            var classDefs = context.Compilation.SyntaxTrees
                .SelectMany(t => t.GetRoot().DescendantNodes())
                .OfType<ClassDeclarationSyntax>()
                .Where(c => c.Identifier.Text == identifier.Identifier.Text)
                .Where(c => c
                    .AncestorsAndSelf()
                    .OfType<NamespaceDeclarationSyntax>()
                    .First().Name.ToString() != ns)
                .ToList();
            
            if (!classDefs.Any()) return;

            var dic = new Dictionary<string, string>() 
            {
                { "name", classDefs.First().Identifier.Text },
                { "ns" , ns}
            }.ToImmutableDictionary();

            var diagnostic = Diagnostic.Create(Rule, identifier.GetLocation(), dic,identifier.GetText().ToString().Trim());
            context.ReportDiagnostic(diagnostic);
        }
    }
}
