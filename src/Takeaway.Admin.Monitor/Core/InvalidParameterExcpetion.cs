using System;

namespace Takeaway.Admin.Service.Core
{
    public class InvalidParameterExcpetion : Exception
    {
        public InvalidParameterExcpetion(string message) : base(message) { }
    }
}