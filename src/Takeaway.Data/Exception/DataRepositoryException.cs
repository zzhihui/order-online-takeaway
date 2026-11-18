using Core.Common.Exception;

namespace Takeaway.Data.Exception
{
 
    public class DataRepositoryException : CoreException
    {
        public DataRepositoryException(System.Exception ex) : base(ex)
        {
        }
    }
}