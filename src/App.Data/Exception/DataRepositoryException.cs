using Core.Common.Exception;

namespace Takeaway.Data.Exception
{
    public enum DataRepositoryExceptionCode
    {
        Drex001
    }

    public class DataRepositoryException : CoreException
    {
        public const string ExceptionType = "DataRepositoryException";
        public DataRepositoryException(System.Exception inner, string code = null)
            : base(ExceptionType, CheckDefaultCode(code), inner)
        {

        }

        private static string CheckDefaultCode(string code)
        {
            return string.IsNullOrEmpty(code) ? DataRepositoryExceptionCode.Drex001.ToString() : code;
        }
    }
}