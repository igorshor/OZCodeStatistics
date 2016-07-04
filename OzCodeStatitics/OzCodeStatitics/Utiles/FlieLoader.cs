using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace OzCodeStatitics.Utiles
{
    public class FlieLoader
    {
        public IEnumerable<Stream> LoadArchive(string archiveLocation, string fileExtention)
        {
            using (var file = File.OpenRead(archiveLocation))
            using (var zip = new ZipArchive(file, ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in zip.Entries)
                {
                    if (entry.FullName.EndsWith('.' + fileExtention))
                    {
                        using (var stream = entry.Open())
                        {
                            yield return stream;
                        }

                    }
                }
            }
        }

        public string LoadSolution(string archiveLocation)
        {
            string directoryLocation = archiveLocation.Replace(".zip", "-unziped");
            if (Directory.Exists(directoryLocation))
            {
                Directory.Delete(directoryLocation, true);
            }
            ZipFile.ExtractToDirectory(archiveLocation, directoryLocation);

            

            return DirSearch(directoryLocation, ".sln");
        }

        static string DirSearch(string directoryLocation,string searchPatern)
        {
            try
            {
                foreach (string directory in Directory.GetDirectories(directoryLocation))
                {
                    foreach (string str in Directory.GetFiles(directory))
                    {
                        if (str.EndsWith(searchPatern))
                        {
                            return str;
                        }
                    }
                    return DirSearch(directory, searchPatern);
                }
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

            return null;
        }
    }
}
