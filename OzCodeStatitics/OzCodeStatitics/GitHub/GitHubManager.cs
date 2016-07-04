using Octokit;
using System.Threading.Tasks;

namespace OzCodeStatitics.GitHub
{
    public class GitHubManager
    {
        private readonly GitHubClient _gitHubClient;
        public GitHubManager()
        {
            _gitHubClient = new GitHubClient(new ProductHeaderValue("TestGitHubAPI"));
        }
        public async Task<SearchRepositoryResult> FindCSharpRepositoriesAsync()
        {
            var searchRepositoriesRequest = new SearchRepositoriesRequest()
            {
                Language = Language.CSharp,
                SortField = RepoSearchSort.Forks,
                Order = SortDirection.Descending,
                PerPage = 10
            };

            return await _gitHubClient.Search.SearchRepo(searchRepositoriesRequest);
        }

        public async Task<Repository> GetRepositoryAsync(Repository repository)
        {
            return await _gitHubClient.Repository.Get(repository.Owner.Login, repository.Name);
        }
    }
}
