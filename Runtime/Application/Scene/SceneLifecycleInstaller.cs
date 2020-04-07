using UnityEngine;
using Zenject;

namespace Gamebase.Application.Scene
{
    public sealed class SceneLifecycleInstaller : MonoInstaller<SceneLifecycleInstaller>
    {
        [SerializeField] private MonoBehaviour[] monoBehaviours = default;

        public override void InstallBindings()
        {
            foreach (var monoBehaviour in monoBehaviours)
            {
                var sceneLifecycles = monoBehaviour.GetComponents<ISceneLifecycle>();
                foreach (var sceneLifecycle in sceneLifecycles)
                {
                    Container.Bind<ISceneLifecycle>().FromInstance(sceneLifecycle);
                }
            }
        }
    }
}