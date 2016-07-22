using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindEmptyFoldersOccurrence
{
    public class Program
    {
        static void Main(string[] args)
        {
            var directoryList = GetDirectoryList(@"P:\");
        }

        private static object GetDirectoryList(string searchPath)
        {
            try
            {
                var files = from directory in Directory.EnumerateDirectories(searchPath, "*.*", SearchOption.AllDirectories)
                            select new
                            {
                                Directory = directory
                            };

                return files;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        private static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
    }
}
