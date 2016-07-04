using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OzCodeStatitics.Model;

namespace OzCodeStatitics.Analyze.Linq
{
    public class AnalizeFluentLinq : AnalizeLinq
    {
        private const string LinqDeclaration = "System.Linq.Enumerable";
        private readonly SyntaxNode _root;
        private readonly SemanticModel _semantic;

        public AnalizeFluentLinq(Document document)
        {
            var modelAsync = document.GetSemanticModelAsync();
            modelAsync.Wait();
            _semantic = modelAsync.Result;
            _root = _semantic.SyntaxTree.GetRoot();
        }

        public AnalizeFluentLinq(SemanticModel semantic)
        {
            _semantic = semantic;
            _root = _semantic.SyntaxTree.GetRoot();
            
        }

        public AnalizeFluentLinq(SyntaxNode root, SemanticModel semantic)
        {
            _root = root;
            _semantic = semantic;
        }

        public override bool IsExist()
        {
            return GetFluentLinqNodes().Any();
        }

        public override RepositoryStatistics Analize()
        {
            foreach (InvocationExpressionSyntax fluentExpressionSyntax in GetFluentLinqNodes())
            {
                SymbolInfo symbolInfo = _semantic.GetSymbolInfo(fluentExpressionSyntax);
                IMethodSymbol symbol = symbolInfo.Symbol as IMethodSymbol;
                if (symbol != null && symbol.ConstructedFrom.ContainingType.ToString() == LinqDeclaration)
                {
                    AddToStatistic(LinqKind.Fluent ,symbol.Name);
                }
            }

            return Statistic;
        }

        private IEnumerable<InvocationExpressionSyntax> GetFluentLinqNodes()
        {
            return _root.DescendantNodes().
                OfType<InvocationExpressionSyntax>();
        }
    }
}
