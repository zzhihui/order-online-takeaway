using System;
using Core.Common.Exception;

namespace Takeaway.Service.Exceptions
{


    public enum TransformerExceptionCode
    {
        TrEx001, //General Error

    }


    public class TransformerException : CoreException
    {
        public TransformerException(Exception ex) : base(ex)
        {
        }
    }


}