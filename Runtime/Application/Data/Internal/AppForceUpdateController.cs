using System;
using System.Linq;
using UniRx;
using UniRx.Async;
using Zenject;

namespace Gamebase.Application.Data.Internal
{
    internal sealed class AppForceUpdateController : IAppForceUpdateController, IInitializable, IDisposable
    {
        private readonly IRequiredVersionRepository repository;

        private readonly CompositeDisposable disposable = new CompositeDisposable();
        
        private readonly BoolReactiveProperty forceUpdate = new BoolReactiveProperty();

        public AppForceUpdateController([InjectOptional] IRequiredVersionRepository repository)
        {
            this.repository = repository;
        }

        bool CheckForceUpdate(string requiredVersionString)
        {
            var applicationVersion = DeserializeVersion(UnityEngine.Application.version);
            var requiredVersion = DeserializeVersion(requiredVersionString);
            for (var i = 0; i < Math.Min(applicationVersion.Length, requiredVersion.Length); i++)
            {
                if (applicationVersion[i] < requiredVersion[i])
                    return true;
            }
            return false;
        }

        private int[] DeserializeVersion(string versionString)
        {
            return versionString.Split('.')
                .Select(int.Parse)
                .ToArray();
        }
        
        void IInitializable.Initialize()
        {
            repository?.OnChangeAsObservable()
                .Subscribe(x => forceUpdate.Value = CheckForceUpdate(x))
                .AddTo(disposable);
        }

        void IDisposable.Dispose()
        {
            disposable?.Dispose();
        }

        async UniTask IAppForceUpdateController.Check()
        {
            var requiredVersionString = await repository.Get();
            forceUpdate.Value = CheckForceUpdate(requiredVersionString);
        }
        
        IObservable<Unit> IAppForceUpdateController.OnForceUpdateAsObservable()
        {
            return forceUpdate.Where(x => x).AsUnitObservable();
        }
    }
}