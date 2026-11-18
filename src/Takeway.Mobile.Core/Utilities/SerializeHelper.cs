using Newtonsoft.Json;

namespace Takeway.Mobile.Core.Utilities
{
    public class SerializeHelper
    {
        public static T CloneJson<T>(T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        } 
    }
}