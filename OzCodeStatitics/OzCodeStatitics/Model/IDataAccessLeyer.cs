using System.Threading.Tasks;

namespace OzCodeStatitics.Model
{
    public interface IDataAccessLeyer
    {
        bool IsConnected();
        RepositoryStatistics Get();
        Task Add(RepositoryStatistics item);
    }
}
