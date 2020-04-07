using System;
using UniRx;
using UniRx.Async;

namespace Gamebase.Application.Data
{
    public interface IAppForceUpdateController
    {
        UniTask Check();
        
        IObservable<Unit> OnForceUpdateAsObservable();
    }
}