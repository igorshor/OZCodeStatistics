using System;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Octokit;
using OzCodeStatitics.Model;
using OzCodeStatitics.MongoDb;
using OzCodeStatitics.Utiles;

namespace OzCodeStatitics.Analyze
{
    public class RepositoryAnalyzer
    {
        private readonly string _archivePath;
        private static FlieLoader _fileLoader;
        private readonly RepositoryModel _repository;

        public RepositoryAnalyzer(Repository repository, string archivePath)
        {
            _archivePath = archivePath;
            _repository = new RepositoryModel(repository);
            _fileLoader = new FlieLoader();
        }

        public async Task<RepositoryModel> AnalizeArchiveAsync()
        {
            var slnPath = _fileLoader.LoadSolution(_archivePath);
            var solution = await MSBuildWorkspace.Create().OpenSolutionAsync(slnPath);

            foreach (var project in solution.Projects)
            {
                foreach (var document in project.Documents)
                {
                    if (document.SupportsSyntaxTree && document.SupportsSemanticModel)
                    {
                        AnalizeFile(document);
                    }
                }
            }
            Console.WriteLine(_repository.Statistics);
            return _repository;
        }

        private void AnalizeFile(Document document)   
        {
            var csFileAnalyzer = new CsFileAnalyzer(document);
            InvestigateStatistics(csFileAnalyzer.Analyze());
        }

        private void InvestigateStatistics(RepositoryStatistics statistics)
        {
            CheckLinqUsage(statistics);
            _repository.Statistics.Append(statistics);
        }

        private void CheckLinqUsage(RepositoryStatistics statistics)
        {
            Console.WriteLine(statistics);
            ChackValue(LinqKind.Fluent, statistics);
            ChackValue(LinqKind.Sql, statistics);

        }

        private void ChackValue(LinqKind kind, RepositoryStatistics statistics)
        {
            if (((statistics.Kind & kind) != 0) && ((_repository.Kind & kind) == 0))
            {
                _repository.Kind |= kind;
            }
        }
    }
}
