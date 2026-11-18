using System;

namespace Core.Common.Core
{
    public interface IHandler
    {
        TResponse Execute<TResponse>(Func<TResponse> action);
    }
}