using System;
using Octokit;

namespace OzCodeStatitics.Model
{
    [Flags]
    public enum LinqKind
    {
        Fluent = 1,
        Sql = 2,
        Both = Fluent | Sql
    }

    public class RepositoryModel
    {

        public RepositoryModel(Repository repository)
        {
            GitHubRepositoryRef = repository;
            Statistics = new RepositoryStatistics();
            Lines = 0;
        }

        public Repository GitHubRepositoryRef { get; set; }
        public LinqKind Kind { get; set; }
        public int Lines { get; set; }
        public RepositoryStatistics Statistics { get; set; }
    }
}

