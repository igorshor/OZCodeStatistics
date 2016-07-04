using System.Collections.Generic;
using System.Text;

namespace OzCodeStatitics.Model
{
    public class RepositoryStatistics
    {
        public LinqKind Kind { get; set; }
        public static readonly List<string> _validKeys;
        private Dictionary<LinqKind, Dictionary<string, int>> Statistics { get; }

        static RepositoryStatistics()
        {
            _validKeys = new List<string>();

        }

        public RepositoryStatistics()
        {
            Statistics = new Dictionary<LinqKind, Dictionary<string, int>>
            {
                {LinqKind.Fluent, new Dictionary<string, int>()},
                {LinqKind.Sql, new Dictionary<string, int>()}
            };
        }

        public void Add(LinqKind kind, string key)
        {
            CreateOrAppendValue(kind ,key, 1);
        }

        private void Add(LinqKind kind, string key, int value)
        {
            CreateOrAppendValue(kind, key, value);
        }

        public void Append(RepositoryStatistics statistics)
        {
            if (statistics == null)
            {
                return;
            }

            foreach (var item in statistics.Statistics)
            {
                foreach (var specificItem in item.Value)
                {
                    Add(item.Key, specificItem.Key, specificItem.Value);
                }
                
            }
        }

        private void CreateOrAppendValue(LinqKind kind, string key, int value)
        {
            if (!Statistics.ContainsKey(kind))
            {
                return;
            }

            if (Statistics[kind].ContainsKey(key))
            {
                Statistics[Kind][key] += value;
            }
            else
            {
                Statistics[kind].Add(key, value);
            }

        }

        private bool IsValidKey(LinqKind key)
        {
            return true;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder(string.Empty);

            str.AppendFormat("Kind - {0}", Kind);
            str.AppendLine();
            str.AppendLine("-------------------------------");
            str.AppendLine();

            foreach (var keyValuePair in Statistics)
            {
                str.AppendFormat("{0} - {1}", keyValuePair.Key, keyValuePair.Value);
                str.AppendLine();
            }

            str.AppendLine();

            return str.ToString();
        }
    }
}
