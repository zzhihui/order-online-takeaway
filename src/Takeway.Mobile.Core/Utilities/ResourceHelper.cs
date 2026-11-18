

using System.IO;
using System.Reflection;

namespace Takeway.Mobile.Core.Utilities
{
    public class ResourceHelper
    {
        public static string ReadResourceFileToString(string resourceName)
        {
            var assembly = typeof(ResourceHelper).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}