using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OzCodeStatitics.Analyze.Linq.Clause
{
    public class JoinClause : ICommand
    {
        public void Execute(SyntaxNode clause, Action<SyntaxToken> action)
        {
            var joinClause = clause as JoinClauseSyntax;
            if (joinClause == null)
                return;

            action(joinClause.JoinKeyword);

            if (joinClause.Into != null)
            {
                action(joinClause.Into.IntoKeyword);
            }

            action(joinClause.EqualsKeyword);
        }
    }
}
