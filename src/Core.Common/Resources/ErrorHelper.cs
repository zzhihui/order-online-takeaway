namespace Core.Common.Resources
{
    public class ErrorHelper
    {
        public static string GetLocaleString(string resourceName)
        {
            return ErrorDescription.ResourceManager.GetString(resourceName);
        }
    }
}