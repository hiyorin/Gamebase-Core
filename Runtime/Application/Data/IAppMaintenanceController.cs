using System;
using UniRx;
using UniRx.Async;

namespace Gamebase.Application.Data
{
    public interface IAppMaintenanceController
    {
        UniTask Check();

        IObservable<Unit> OnMaintenanceAsObservable();
    }
}