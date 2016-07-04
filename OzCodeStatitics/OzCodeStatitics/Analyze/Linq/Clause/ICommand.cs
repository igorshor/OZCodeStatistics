using System;
using Microsoft.CodeAnalysis;

namespace OzCodeStatitics.Analyze.Linq.Clause
{
    public interface ICommand
    {
        void Execute(SyntaxNode clause, Action<SyntaxToken> action);
    }
}