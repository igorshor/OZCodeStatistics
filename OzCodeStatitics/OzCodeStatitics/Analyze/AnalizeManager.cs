using System.Net;
using System.Threading.Tasks;
using Octokit;
using OzCodeStatitics.GitHub;
using OzCodeStatitics.Model;
using OzCodeStatitics.MongoDb;

namespace OzCodeStatitics.Analyze
{
    class AnalizeManager
    {
        private readonly RepositoriesDAL _repositories;
        private readonly GitHubManager _gitHubManager;

        public AnalizeManager()
        {
            const string connectionString = "mongodb://superuser:1234@ds019033.mlab.com:19033";
            const string dbName = "linq";
            _repositories = new RepositoriesDAL(new MongoDbAccessLeyer(connectionString, dbName));
            _gitHubManager = new GitHubManager();
        }

        public async Task Analize()
        {
            var repositories = await _gitHubManager.FindCSharpRepositoriesAsync();

            using (var client = new WebClient())
            {
                var repository = await _gitHubManager.GetRepositoryAsync(repositories.Items[0]);
                var outputFileName = @"C:\Users\CodeValue\Desktop\" + "igor" + ".zip";

                var repositoryAnalizer = new RepositoryAnalyzer(repository, outputFileName);


                //await client.DownloadFileTaskAsync(repository.HtmlUrl + @"/archive/master" + ".zip", outputFileName);
                var repositoryModel = await repositoryAnalizer.AnalizeArchiveAsync();
                _repositories.Add(repositoryModel);
            }
        }
    }
}
