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
            foreach (var item in directoryList)
            {
                Log(item, "test.txt");
            }
        }

        private static List<string> GetEmptyDirectoryList(string searchPath)
        {
            try
            {
                var directories = from directory in Directory.EnumerateDirectories(searchPath, "*.*", SearchOption.AllDirectories)
                                  where directory.Contains("01_Documents")
                                  where Directory.GetFileSystemEntries(directory).Length == 0
                                  select new
                                  {
                                      Directory = directory
                                  };

                var collection = directories.Select(c => c.Directory).ToList();

                return collection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        private static void Log(string message, string path)
        {
            try
            {
                using (TextWriter w = File.AppendText(path))
                {
                    w.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not log the file: " + e.ToString());
            }
        }
    }
}
