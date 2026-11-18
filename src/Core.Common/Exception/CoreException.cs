using System;
using Core.Common.Resources;

namespace Core.Common.Exception
{
    public class CoreException : System.Exception
    {
        public CoreException(System.Exception ex):base(ex.Message)
        {
            
        }

        public CoreException(string message) : base(message)
        {

        }
    }
}