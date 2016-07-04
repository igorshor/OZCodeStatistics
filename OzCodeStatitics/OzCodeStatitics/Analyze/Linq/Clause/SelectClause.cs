using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OzCodeStatitics.Analyze.Linq.Clause
{
    public class SelectClause : ICommand
    {
        public void Execute(SyntaxNode clause, Action<SyntaxToken> action)
        {
            var selectByClause = clause as SelectClauseSyntax;
            if (selectByClause == null)
                return;

            action(selectByClause.SelectKeyword);
        }
    }
}