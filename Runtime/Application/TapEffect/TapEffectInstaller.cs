using System;
using System.Collections.Generic;
using Gamebase.Application.Internal.TapEffect;
using UnityEngine;
using Zenject;

namespace Gamebase.Application.TapEffect
{
    [CreateAssetMenu(fileName = "TapEffectInstaller", menuName = "Installers/Gamebase/TapEffectInstaller")]
    public sealed class TapEffectInstaller : ScriptableObjectInstaller<TapEffectInstaller>
    {
        [SerializeField] private TapEffectSettings settings = null;
        
        public override void InstallBindings()
        {
            Container.Bind<ITapEffectController>()
                .FromSubContainerResolve()
                .ByMethod(InstallSubContainer)
                .WithKernel()
                .AsSingle();
        }

        private void InstallSubContainer(DiContainer subContainer)
        {
            subContainer.BindInterfacesTo<TapEffectManager>().AsSingle();
            subContainer.BindInstance(settings).AsSingle();
        }
    }

    [Serializable]
    public sealed class TapEffectSettings
    {
        [SerializeField] private TapEffectBase[] prefabs = {};
        
        [SerializeField] private bool defaultEnable = false;

        public IEnumerable<TapEffectBase> Prefabs => prefabs;
        
        public bool DefaultEnable => defaultEnable;
    }
}
