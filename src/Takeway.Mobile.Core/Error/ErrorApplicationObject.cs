using System;
using Cirrious.CrossCore.Core;

namespace Takeway.Mobile.Core.Error
{
    public interface IErrorReporter
    {
        void ReportError(string error);
    }

    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public ErrorEventArgs(string message)
        {
            Message = message;
        }
    }

    public interface IErrorSource
    {
        event EventHandler<ErrorEventArgs> ErrorReported;
    }

    public class ErrorApplicationObject
        : MvxMainThreadDispatchingObject
        , IErrorReporter
        , IErrorSource
    {
        public void ReportError(string error)
        {
            if (ErrorReported == null)
                return;

            InvokeOnMainThread(() =>
            {
                var handler = ErrorReported;
                if (handler != null)
                    handler(this, new ErrorEventArgs(error));
            });
        }

        public event EventHandler<ErrorEventArgs> ErrorReported;
    }
}