using Core.Common.Exception;

namespace Takeaway.Data.Exception
{
    public enum DataQueryExceptionCode
    {
        Dqex001 //General Data QueryException

    }

    public class DataQueryException : CoreException
    {
        public const string ExceptionType = "DataQueryException";

        public DataQueryException(System.Exception inner, string code = null)
            : base(ExceptionType, CheckDefaultCode(code), inner)
        {
        }

        private static string CheckDefaultCode(string code)
        {
            return string.IsNullOrEmpty(code) ? DataQueryExceptionCode.Dqex001.ToString() : code;
        }
    }
}