using Microsoft.CodeAnalysis;
using OzCodeStatitics.Analyze.Linq;
using OzCodeStatitics.Model;

namespace OzCodeStatitics.Analyze
{
    public class CsFileAnalyzer
    {
        private readonly SemanticModel _semantic;
        private readonly RepositoryStatistics _statistic;


        public  CsFileAnalyzer(Document document)
        {
            var modelAsync = document.GetSemanticModelAsync();
            modelAsync.Wait();

            _semantic = modelAsync.Result;
            _statistic = new RepositoryStatistics();
        }

        public RepositoryStatistics Analyze()
        {
            if (!FileUsingLinq())
                return _statistic;

            AnalizeLinq(new AnalizeSqlLinq(_semantic.SyntaxTree), LinqKind.Sql);
            AnalizeLinq(new AnalizeFluentLinq(_semantic), LinqKind.Fluent);

            return _statistic;
        }

        private void AnalizeLinq(AnalizeLinq linq, LinqKind kind)
        {
            if (linq.IsExist())
            {
                _statistic.Kind |= kind;
                _statistic.Append(linq.Analize());
            }
        }

        private bool FileUsingLinq()
        {
            return true;
        }

    }
}
