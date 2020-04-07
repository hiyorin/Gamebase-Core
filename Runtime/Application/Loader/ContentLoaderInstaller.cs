using System;
using Gamebase.Application.Loader.Internal;
using UnityEngine;
using Zenject;

namespace Gamebase.Application.Loader
{
    [CreateAssetMenu(fileName = "ContentLoaderInstaller", menuName = "Installers/Gamebase/ContentLoaderInstaller")]
    public sealed class ContentLoaderInstaller : ScriptableObjectInstaller<ContentLoaderInstaller>
    {
        [SerializeField] private NetworkSettings networkSettings = default;
        
        public override void InstallBindings()
        {
            var subContainer = Container.CreateSubContainer();
            InstallSubContainer(subContainer);
            
            Container.BindInterfacesTo<ContentCatalogLoader>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();
            
            Container.BindInterfacesTo<AssetLoader>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();
            
            Container.BindInterfacesTo<WebRequester>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();

            Container.BindInterfacesTo<ErrorManager>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();
        }

        private void InstallSubContainer(DiContainer subContainer)
        {
            subContainer.Bind<ContentCatalogLoader>().AsSingle();
            subContainer.Bind<AssetLoader>().AsSingle();
            subContainer.Bind<WebRequester>().AsSingle();
            subContainer.Bind<ErrorManager>().AsSingle();

            subContainer.BindInstance(networkSettings).AsSingle();
        }
    }

    [Serializable]
    public sealed class NetworkSettings
    {
        [SerializeField, Range(0, 10)] private int timeout = default;

        public int Timeout => timeout;
    }
}