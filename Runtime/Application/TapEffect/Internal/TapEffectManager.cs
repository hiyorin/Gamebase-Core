using System;
using System.Linq;
using Gamebase.Application.TapEffect;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Gamebase.Application.Internal.TapEffect
{
    internal sealed class TapEffectManager : IInitializable, ITickable, IDisposable, ITapEffectController
    {
        [Inject] private TapEffectSettings settings = null;

        [Inject] private Camera uiCamera = null;

        private string currentName;
        
        private TapEffectBase instance;

        private bool isEnable = false;
        
        void IInitializable.Initialize()
        {
            if (settings.Prefabs == null || settings.Prefabs.IsEmpty())
            {
                Debug.unityLogger.LogError(GetType().Name, $"prefab is null");
                return;
            }

            isEnable = settings.DefaultEnable;
            Change(settings.Prefabs.FirstOrDefault()?.name);
            
            Input.simulateMouseWithTouches = true;
        }

        void ITickable.Tick()
        {
            if (instance == null || !isEnable)
                return;
            
            var worldPosition = uiCamera.ScreenToWorldPoint(Input.mousePosition + uiCamera.transform.forward * 10.0f);
            
            if (Input.GetMouseButtonDown(0))
            {
                instance.OnPress(worldPosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                instance.OnRelease(worldPosition);
            }
            else if (Input.GetMouseButton(0))
            {
                instance.OnSwipe(worldPosition);
            }
        }

        void IDisposable.Dispose()
        {
            UnityEngine.Object.DestroyImmediate(instance);
            instance = null;
        }
        
        #region ITapEffectController implementation

        public void Enable()
        {
            isEnable = true;
        }

        public void Disable()
        {
            isEnable = false;
        }

        public void Change(string name)
        {
            if (currentName == name)
                return;
            
            var prefab = settings.Prefabs.FirstOrDefault(x => x.name == name);
            if (prefab == null)
            {
                Debug.unityLogger.LogError(GetType().Name, $"prefab is null");
                return;
            }
            
            if (instance != null)
                UnityEngine.Object.DestroyImmediate(instance);
            
            instance = UnityEngine.Object.Instantiate(prefab);
            instance.OnRelease(Vector3.zero);

            currentName = name;
        }
        
        #endregion
    }
}
