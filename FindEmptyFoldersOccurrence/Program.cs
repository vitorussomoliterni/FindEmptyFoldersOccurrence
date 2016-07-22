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
            var directoryList = GetEmptyDirectoryList(@"P:\");

        }

        private static IEnumerable<object> GetEmptyDirectoryList(string searchPath)
        {
            try
            {
                var emptyDirectories = from directory in Directory.EnumerateDirectories(searchPath, "*.*", SearchOption.AllDirectories)
                                  where IsDirectoryEmpty(directory) == true
                                  select new
                                  {
                                      Directory = directory
                                  };

                return emptyDirectories;
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
