using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OzCodeStatitics.Analyze.Linq.Clause
{
    public class WhereClause : ICommand
    {
        public void Execute(SyntaxNode clause, Action<SyntaxToken> action)
        {
            var whereClause = clause as WhereClauseSyntax;
            if (whereClause == null)
                return;

            action(whereClause.WhereKeyword);
        }
    }
}