using Gamebase.Data.Repository;
using Gamebase.Domain.UseCase;
using UnityEngine;
using Zenject;

namespace Gamebase.Application.Sound.Unity
{
    [CreateAssetMenu(fileName = "UnitySoundInstaller", menuName = "Installers/Gamebase/UnitySoundInstaller")]
    public sealed class UnitySoundInstaller : ScriptableObjectInstaller<UnitySoundInstaller>
    {
        [SerializeField] private SoundSettings generalSettings = default;

        [SerializeField] private UnitySoundSettings settings = default;
        
        [SerializeField] private UnitySoundPack[] preloadPacks = { };
        
        [SerializeField] private bool log = false;
        
        public override void InstallBindings()
        {
            var subContainer = Container.CreateSubContainer();
            InstallSubContainer(subContainer);
            
            Container.BindInterfacesTo<UnitySoundManager>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();
            
            Container.BindInterfacesTo<UnitySoundVolumeController>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();

            
        }

        private void InstallSubContainer(DiContainer subContainer)
        {
            subContainer.Bind<UnitySoundManager>().AsSingle();
            subContainer.Bind<UnitySoundVolumeController>().AsSingle();
            
            subContainer.BindInterfacesTo<SoundConfigRepository>().AsSingle();
            subContainer.BindInterfacesTo<SoundConfigUseCase>().AsSingle();
            subContainer.BindInstance(generalSettings).AsSingle();
            subContainer.BindInstance(settings).AsSingle();
            subContainer.BindInstance(preloadPacks).AsSingle();
            
            if (log && Debug.isDebugBuild)
                subContainer.BindInterfacesTo<UnitySoundLogger>().AsSingle();
        }
    }
    
}