using System;
using UniRx;
using UniRx.Async;
using Zenject;

namespace Gamebase.Application.Loader.Internal
{
    internal sealed class ErrorManager : IErrorRequester, IErrorReceiver
    {
        private readonly IErrorHandler handler;
        
        private readonly ISubject<ErrorMessage> onError = new Subject<ErrorMessage>();
        
        public ErrorManager([InjectOptional] IErrorHandler handler)
        {
            this.handler = handler;
        }

        public async UniTask<ErrorResult> Request(ErrorMessage message)
        {
            if (handler != null)
            {
                var result = await handler.Handle(message);
                if (result == ErrorResult.Error)
                    onError.OnNext(message);
                return result;
            }
            else
            {
                return ErrorResult.Error;
            }
        }

        public IObservable<ErrorMessage> OnErrorAsObservable()
        {
            return onError;
        }
    }
}