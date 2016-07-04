using Octokit;
using System;
using System.Net;
using OzCodeStatitics.Analyze;
using OzCodeStatitics.GitHub;

namespace OzCodeStatitics
{
    class Program
    {
        private static AnalizeManager _analizeManager;

        static void Main(string[] args)
        {
            _analizeManager = new AnalizeManager();
            var analize = _analizeManager.Analize();
            analize.Wait();
            Console.ReadLine();
        }
    }
}
