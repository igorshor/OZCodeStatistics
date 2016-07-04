using OzCodeStatitics.Model;

namespace OzCodeStatitics.Analyze.Linq
{
    public abstract class AnalizeLinq : IFileAnalyzer
    {
        protected readonly RepositoryStatistics Statistic;

        protected AnalizeLinq()
        {
            Statistic = new RepositoryStatistics();
        }

        public abstract bool IsExist();
        public abstract RepositoryStatistics Analize();

        protected void AddToStatistic(LinqKind kind ,string value)
        {
            Statistic.Add(kind, value.ToLower());
        }
    }
}
