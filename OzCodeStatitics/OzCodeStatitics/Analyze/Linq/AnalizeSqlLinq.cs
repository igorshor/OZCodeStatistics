using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OzCodeStatitics.Analyze.Linq.Clause;
using OzCodeStatitics.Model;

namespace OzCodeStatitics.Analyze.Linq
{
    public class AnalizeSqlLinq : AnalizeLinq
    {
        private readonly SyntaxNode _root;
        private static readonly Dictionary<SyntaxKind, ICommand> ClauseActionsDictionary;

        static AnalizeSqlLinq()
        {
            ClauseActionsDictionary = new Dictionary<SyntaxKind, ICommand>
            {
                {SyntaxKind.FromClause, new FromClause()},
                {SyntaxKind.GroupClause, new GroupClause()},
                {SyntaxKind.JoinClause, new JoinClause()},
                {SyntaxKind.LetClause, new LetClause()},
                {SyntaxKind.OrderByClause, new OrderByClause()},
                {SyntaxKind.SelectClause, new SelectClause()},
                {SyntaxKind.WhereClause, new WhereClause()}
            };
        }

        public AnalizeSqlLinq(Document document)
        {
            var modelAsync = document.GetSemanticModelAsync();
            modelAsync.Wait();

            _root = modelAsync.Result.SyntaxTree.GetRoot();
        }

        public AnalizeSqlLinq(SyntaxNode root)
        {
            _root = root;
        }

        public AnalizeSqlLinq(SyntaxTree syntaxTree)
        {
            _root = syntaxTree.GetRoot();
        }

        public override bool IsExist()
        {
            return GetSqlLinqNodes().Any();
        }

        public override RepositoryStatistics Analize()
        {
            foreach (var sqlExpressionSyntax in GetSqlLinqNodes())
            {
                var body = sqlExpressionSyntax.Body;
                QueryContinuationSyntax continuation;
                var clauses = body.Clauses;

                AnalyzeClause(sqlExpressionSyntax.FromClause);

                do
                {
                    foreach (var clause in clauses)
                    {
                        AnalyzeClause(clause);
                    }

                    AnalyzeClause(body.SelectOrGroup);
                    continuation = body.Continuation;

                    if (continuation == null)
                        continue;

                    AnalyzeKeyword(continuation.IntoKeyword);
                    body = continuation.Body;
                    clauses = body.Clauses;

                } while (continuation != null);
            }

            return Statistic;
        }

        private void AnalyzeKeyword(SyntaxToken intoKeyword)
        {
            if (string.IsNullOrEmpty(intoKeyword.ValueText))
                return;

            AddToStatistic(LinqKind.Sql ,intoKeyword.ValueText);
        }


        private void AnalyzeClause(SyntaxNode clause)
        {
            if (!ClauseActionsDictionary.ContainsKey(clause.Kind()))
                return;

            ClauseActionsDictionary[clause.Kind()].Execute(clause, AnalyzeKeyword);
        }

        private IEnumerable<QueryExpressionSyntax> GetSqlLinqNodes()
        {
            return _root.DescendantNodes()
                .OfType<QueryExpressionSyntax>();
        }
    }
}
