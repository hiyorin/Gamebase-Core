using UnityEngine;
using Zenject;

namespace Gamebase.Application
{
    [CreateAssetMenu(fileName = "ApplicationSettingsInstaller", menuName = "Installers/Gamebase/ApplicationSettingsInstaller")]
    internal sealed class ApplicationSettingsInstaller : ScriptableObjectInstaller<ApplicationSettingsInstaller>
    {
        [SerializeField, Range(-1, 120)]
        private int targetFrameRate = -1;

        [SerializeField] private GraphicSettings graphicSettings = default;

        [SerializeField] private PlatformStoreSettings platformStoreSettings = default;
        
        public override void InstallBindings()
        {
            Container.BindInstance(graphicSettings).AsSingle();
            Container.BindInstance(platformStoreSettings).AsSingle();
            
            if (targetFrameRate > 0)
            {
                QualitySettings.vSyncCount = 0;
                UnityEngine.Application.targetFrameRate = targetFrameRate;
            }
        }
    }
}
