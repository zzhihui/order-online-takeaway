using Core.Common.Exception;

namespace Takeaway.Data.Exception
{
 
    public class DataQueryException : CoreException
    {
        public DataQueryException(System.Exception ex) : base(ex)
        {
        }
    }
}