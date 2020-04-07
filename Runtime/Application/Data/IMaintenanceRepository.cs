using System;
using UniRx.Async;

namespace Gamebase.Application.Data
{
    public interface IMaintenanceRepository
    {
        UniTask<bool> Get();
        
        IObservable<bool> OnChangeAsObservable();
    }
}