using System;
using UniRx;
using UniRx.Async;
using Zenject;

namespace Gamebase.Application.Data.Internal
{
    internal sealed class AppMaintenanceController : IAppMaintenanceController, IInitializable, IDisposable
    {
        private readonly IMaintenanceRepository repository;

        private readonly CompositeDisposable disposable = new CompositeDisposable();
        
        private readonly BoolReactiveProperty isMaintenance = new BoolReactiveProperty();

        public AppMaintenanceController([InjectOptional] IMaintenanceRepository repository)
        {
            this.repository = repository;
        }
        
        void IInitializable.Initialize()
        {
            repository?.OnChangeAsObservable()
                .Subscribe(x => isMaintenance.Value = x)
                .AddTo(disposable);
        }

        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }

        async UniTask IAppMaintenanceController.Check()
        {
            isMaintenance.Value = await repository.Get();
        }

        IObservable<Unit> IAppMaintenanceController.OnMaintenanceAsObservable()
        {
            return isMaintenance.Where(x => x).AsUnitObservable();
        }
    }
}