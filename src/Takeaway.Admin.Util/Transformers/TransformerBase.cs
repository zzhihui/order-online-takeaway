using System;
using System.Diagnostics;
using AutoMapper;
using Core.Common.Core;
using log4net;
using Takeaway.Service.Exceptions;

namespace Takeaway.Admin.Util.Transformers
{

    public interface ITransformer : IHandler
    {
        TDestination Transform<TSource, TDestination>(TSource source) where TDestination: class, new();
    }

    public interface ITransformer<T> : ITransformer
    {

    }

    public abstract class TransformerBase : ITransformer
    {

        protected readonly ILog Logger;

        public TransformerBase()
        {
            Logger = LogManager.GetLogger(this.GetType());
        }


        public TResponse Execute<TResponse>(Func<TResponse> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Transformer:[Exception] {0}-{1}", ex.Message, ex.StackTrace));
                throw new TransformerException(ex);
            }
        }

        public void Execute(Action action)
        {
            throw new NotImplementedException();
        }


        public virtual TDestination Transform<TSource, TDestination>(TSource source) where TDestination : class, new()
        {
            var destination = new TDestination();
            return Execute(() =>
            {
                Mapper.Map(source, destination);
                return destination;
            });
        }
    }
}