using Gamebase.Application.Data.Internal;
using JetBrains.Annotations;
using Zenject;

namespace Gamebase.Application.Data
{
    [PublicAPI]
    public sealed class DataLoaderInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<AuthController>().AsSingle();
            Container.Bind<DataFetchController>().AsSingle();
            Container.Bind<DataValidateController>().AsSingle();
            Container.Bind<AppForceUpdateController>().AsSingle();
            Container.Bind<AppMaintenanceController>().AsSingle();
        }

        public static void BindInterface(DiContainer container, DiContainer subContainer)
        {
            container.BindInterfacesTo<AuthController>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();
            
            container.BindInterfacesTo<DataFetchController>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();
            
            container.BindInterfacesTo<DataValidateController>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();

            container.BindInterfacesTo<AppForceUpdateController>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();

            container.BindInterfacesTo<AppMaintenanceController>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();
        }
    }
}