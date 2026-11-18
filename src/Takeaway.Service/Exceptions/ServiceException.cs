using System;
using Core.Common.Exception;

namespace Takeaway.Service.Exceptions
{
    public enum ServiceExceptionCode
    {
        SEx001, //General Error
        SEx002, //Validation Error

    }


    public class ServiceException : CoreException
    {
        public ServiceException(Exception ex) : base(ex)
        {
        }
    }
}