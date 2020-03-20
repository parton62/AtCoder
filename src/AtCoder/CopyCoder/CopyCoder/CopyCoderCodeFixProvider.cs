using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace CopyCoder
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(CopyCoderCodeFixProvider)), Shared]
    public class CopyCoderCodeFixProvider : CodeFixProvider
    {
        private const string title = "Copy class definition source code";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(CopyCoderAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;
            var name = diagnostic.Properties["name"];
            // Find the type declaration identified by the diagnostic.
            //var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().First();



            //var identifier = root.FindNode(diagnosticSpan).AncestorsAndSelf().OfType<IdentifierNameSyntax>().First();
            var namespaceDecl = root.FindNode(diagnosticSpan).Ancestors().OfType<NamespaceDeclarationSyntax>().First();
            var ns = namespaceDecl.Name.ToString();//diagnostic.Properties["ns"];

            var semanticModel = await context.Document.GetSemanticModelAsync();

            var classDefs = semanticModel.Compilation.SyntaxTrees
                .SelectMany(t => t.GetRoot().DescendantNodes())
                .OfType<ClassDeclarationSyntax>()
                .Where(c => c.Identifier.Text == name)
                .Where(c => c
                    .AncestorsAndSelf()
                    .OfType<NamespaceDeclarationSyntax>()
                    .FirstOrDefault()?.Name.ToString() != ns)
                .ToList();


            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: title,
                    createChangedSolution: c => CopySourceCodeAsync(context.Document, namespaceDecl, classDefs.First(), c),
                    equivalenceKey: title),
                diagnostic);
        }

        private async Task<Solution> CopySourceCodeAsync(Document document, NamespaceDeclarationSyntax namespaceDecl, ClassDeclarationSyntax classDecl, CancellationToken cancellationToken)
        {
            //var  = await document.GetSyntaxTreeAsync(cancellationToken);
            
            var newMembers = namespaceDecl.AddMembers(classDecl);

            var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            var newRoot = root.ReplaceNode(namespaceDecl, newMembers);


            //var newSolution = document.Project.Solution.WithDocumentText(document.Id, document.Tre)

            return document.Project.Solution.WithDocumentSyntaxRoot(document.Id, newRoot);
        }
        private async Task<Solution> MakeUppercaseAsync(Document document, TypeDeclarationSyntax typeDecl, CancellationToken cancellationToken)
        {
            // Compute new uppercase name.
            var identifierToken = typeDecl.Identifier;
            var newName = identifierToken.Text.ToUpperInvariant();

            // Get the symbol representing the type to be renamed.
            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);

            // Produce a new solution that has all references to that type renamed, including the declaration.
            var originalSolution = document.Project.Solution;
            var optionSet = originalSolution.Workspace.Options;
            var newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);

            // Return the new solution with the now-uppercase type name.
            return newSolution;
        }
    }
}
