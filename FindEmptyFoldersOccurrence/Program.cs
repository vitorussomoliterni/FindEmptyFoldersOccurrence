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
            var filename = string.Format("scan result {0}.csv", DateTime.Now.ToString("dd-MM-yyyy"));
            var directoryList = GetEmptyDirectoriesList(@"P:\");
            
            var directoryDictionary = OrderDirectories(directoryList);

            foreach (var item in directoryDictionary)
            {
                Log(item.Key, item.Value, filename);
            }
        }

        private static Dictionary<string, int> OrderDirectories(List<string> directoryList)
        {
            Dictionary<string, int> directoryDictionary = new Dictionary<string, int>();
            foreach (var d in directoryList)
            {
                if (directoryDictionary.ContainsKey(d))
                {
                    directoryDictionary[d]++;
                }
                else
                {
                    directoryDictionary.Add(d, 1);
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

        private static void Log(string message, int count, string path)
        {
            try
            {
                using (TextWriter w = File.AppendText(path))
                {
                    w.WriteLine(message + "," + count);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not log the file: " + e.ToString());
            }
        }
    }
}
