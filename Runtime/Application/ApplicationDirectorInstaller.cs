using UnityEngine;
using Zenject;

namespace Gamebase.Application
{
    internal sealed class ApplicationDirectorInstaller : MonoInstaller
    {
        [SerializeField] private Camera uiCamera = default;
        
        public override void InstallBindings()
        {
            Container.BindInstance(uiCamera).AsSingle();
        }
    }
}