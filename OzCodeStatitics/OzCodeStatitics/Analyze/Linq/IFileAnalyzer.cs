using OzCodeStatitics.Model;

namespace OzCodeStatitics.Analyze.Linq
{
    public interface IFileAnalyzer
    {
        bool IsExist();
        RepositoryStatistics Analize();
    }
}