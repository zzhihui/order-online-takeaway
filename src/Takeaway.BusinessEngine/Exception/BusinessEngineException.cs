using Core.Common.Exception;

namespace Takeaway.BusinessEngine.Exception
{
    public class BusinessEngineException : CoreException
    {
        public BusinessEngineException(System.Exception ex) : base(ex)
        {
        }
    }
}