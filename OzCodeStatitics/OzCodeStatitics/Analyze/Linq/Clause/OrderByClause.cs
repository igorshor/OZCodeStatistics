using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OzCodeStatitics.Analyze.Linq.Clause
{
    public class OrderByClause : ICommand
    {
        public void Execute(SyntaxNode clause, Action<SyntaxToken> action)
        {
            var orderByClause = clause as OrderByClauseSyntax;
            if (orderByClause == null)
                return;

            foreach (var orderingSyntax in orderByClause.Orderings)
            {
                action(orderingSyntax.AscendingOrDescendingKeyword);
            }
        }
    }
}