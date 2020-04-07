using System;

namespace Gamebase.Application.Loader
{
    public interface IErrorReceiver
    {
        IObservable<ErrorMessage> OnErrorAsObservable();
    }
}