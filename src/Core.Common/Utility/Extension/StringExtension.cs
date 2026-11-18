using System.Text.RegularExpressions;

namespace Core.Common.Utility.Extension
{
    public static  class StringExtension
    {
        public static string ReplaceHtmlLineBreak(this string source)
        {
            return Regex.Replace(source, @"\r\n?|\n", "<br />");
        }
        
         
    }
}