using System;

namespace Takeaway.Admin.Service.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class ProcessHandlerAttribute : Attribute { }
}