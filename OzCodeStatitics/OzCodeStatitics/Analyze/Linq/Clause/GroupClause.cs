using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OzCodeStatitics.Analyze.Linq.Clause
{
    public class GroupClause : ICommand
    {
        public void Execute(SyntaxNode clause, Action<SyntaxToken> action)
        {
            var groupByClause = clause as GroupClauseSyntax;
            if (groupByClause == null)
                return;

            action(groupByClause.GroupKeyword);
            action(groupByClause.ByKeyword);
        }
    }
}