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
            var directoryList = GetEmptyDirectoriesList(@"P:\2010\");

            var directoryDictionary = OrderDirectories(directoryList);

            foreach (var item in directoryList)
            {
                Log(item, "test.txt");
            }
        }

        private static Dictionary<string, int> OrderDirectories(List<string> directoryList)
        {
            Dictionary<string, int> directoryDictionary = new Dictionary<string, int>();
            foreach (var d in directoryList)
            {
                if (directoryDictionary[d] == 0)
                {
                    directoryDictionary.Add(d, 1);
                }
                else
                {
                    directoryDictionary[d]++;
                }
            }

            return directoryDictionary;
        }

        private static List<string> GetEmptyDirectoriesList(string searchPath)
        {
            try
            {
                var directories = from directory in Directory.EnumerateDirectories(searchPath, "*.*", SearchOption.AllDirectories)
                                  where directory.Contains("01_Documents")
                                  && Directory.GetFileSystemEntries(directory).Length == 0
                                  select new
                                  {
                                      Directory = directory
                                  };

                var collection = directories.Select(c => c.Directory.Substring(c.Directory.LastIndexOf("\\") + 1)).ToList(); // Rename directory to cut the path and leave the name and add them to a list

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
