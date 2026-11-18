using System.Configuration;
using System.IO;

namespace JsonFileGenerator
{
    public class JsonExporter
    {
        public static void Export(string fileName, string jsonString)
        {
            var path = Path.Combine(ConfigurationManager.AppSettings["FolderPath"], string.Format("{0}.json",fileName));
            File.WriteAllText(path, jsonString);
        } 
    }
}