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
using Microsoft.CodeAnalysis.Simplification;

namespace CopyLibraryAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(CopyLibraryAnalyzerCodeFixProvider)), Shared]
    public class CopyLibraryAnalyzerCodeFixProvider : CodeFixProvider
    {
        private const string title = "Make constant";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(CopyLibraryAnalyzerAnalyzer.DiagnosticId); }
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

            // Find the type declaration identified by the diagnostic.
            //var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().First();
            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<LocalDeclarationStatementSyntax>().First();

            // Register a code action that will invoke the fix.
            //context.RegisterCodeFix(
            //    CodeAction.Create(
            //        title: title,
            //        createChangedSolution: c => MakeUppercaseAsync(context.Document, declaration, c),
            //        equivalenceKey: title),
            //    diagnostic);
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: title,
                    createChangedDocument: c => MakeConstAsync(context.Document, declaration, c),
                    equivalenceKey: title),
                diagnostic);
        }

        private async Task<Document> MakeConstAsync(Document document, LocalDeclarationStatementSyntax localDeclaration, CancellationToken cancellationToken)
        {
            var firstToken = localDeclaration.GetFirstToken();
            var leadingTrivia = firstToken.LeadingTrivia;
            var trimmedLocal = localDeclaration.ReplaceToken(firstToken, firstToken.WithLeadingTrivia(SyntaxTriviaList.Empty));

            var constToken = SyntaxFactory.Token(leadingTrivia, SyntaxKind.ConstKeyword, SyntaxFactory.TriviaList(SyntaxFactory.ElasticMarker));

            var newModifiers = trimmedLocal.Modifiers.Insert(0, constToken);
            //var newLocal = trimmedLocal.WithModifiers(newModifiers).WithDeclaration(localDeclaration.Declaration);

            var variableDeclaration = localDeclaration.Declaration;
            var variableTypeName = variableDeclaration.Type;
            if (variableTypeName.IsVar)
            {
                var semanticModel = await document.GetSemanticModelAsync(cancellationToken);

                var aliasInfo = semanticModel.GetAliasInfo(variableTypeName);
                if (aliasInfo == null)
                {
                    var type = semanticModel.GetTypeInfo(variableTypeName).ConvertedType;

                    if (type.Name != "var")
                    {
                        var typeName = SyntaxFactory.ParseTypeName(type.ToDisplayString())
                            .WithLeadingTrivia(variableTypeName.GetLeadingTrivia())
                            .WithTrailingTrivia(variableTypeName.GetTrailingTrivia());

                        var simplifiedTypeName = typeName.WithAdditionalAnnotations(Simplifier.Annotation);

                        variableDeclaration = variableDeclaration.WithType(simplifiedTypeName);
                    }
                }
            }

            var newLocal = trimmedLocal.WithModifiers(newModifiers)
                .WithDeclaration(variableDeclaration);



            var formattedLocal = newLocal.WithAdditionalAnnotations(Microsoft.CodeAnalysis.Formatting.Formatter.Annotation);

            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken);
            var newRoot = oldRoot.ReplaceNode(localDeclaration, formattedLocal);

            return document.WithSyntaxRoot(newRoot);

        }


        //private async Task<Solution> MakeUppercaseAsync(Document document, TypeDeclarationSyntax typeDecl, CancellationToken cancellationToken)
        //{
        //    // Compute new uppercase name.
        //    var identifierToken = typeDecl.Identifier;
        //    var newName = identifierToken.Text.ToUpperInvariant();

        //    // Get the symbol representing the type to be renamed.
        //    var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
        //    var typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);

        //    // Produce a new solution that has all references to that type renamed, including the declaration.
        //    var originalSolution = document.Project.Solution;
        //    var optionSet = originalSolution.Workspace.Options;
        //    var newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);

        //    // Return the new solution with the now-uppercase type name.
        //    return newSolution;
        //}
    }
}
