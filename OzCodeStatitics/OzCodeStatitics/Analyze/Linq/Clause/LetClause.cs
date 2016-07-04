using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OzCodeStatitics.Analyze.Linq.Clause
{
    public class LetClause : ICommand
    {
        public void Execute(SyntaxNode clause, Action<SyntaxToken> action)
        {
            var letClause = clause as LetClauseSyntax;
            if (letClause == null)
                return;

            action(letClause.LetKeyword);
        }
    }
}