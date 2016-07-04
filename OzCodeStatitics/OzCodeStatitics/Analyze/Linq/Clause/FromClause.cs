using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OzCodeStatitics.Analyze.Linq.Clause
{
    public class FromClause : ICommand
    {
        public void Execute(SyntaxNode clause, Action<SyntaxToken> action)
        {
            var formClause = clause as FromClauseSyntax;
            if (formClause == null)
                return;

            action(formClause.FromKeyword);
        }
    }
}