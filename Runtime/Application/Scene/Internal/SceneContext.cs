using System.Collections.Generic;
using UniRx.Async;

namespace Gamebase.Application.Scene.Internal
{
    internal class SceneContext
    {
        public readonly string Name;

        private List<ISceneLifecycle> lifecycles;

        public bool IsLifecycleEmpty => lifecycles == null || lifecycles.Count <= 0;
        
        public SceneContext(string name)
        {
            Name = name;
        }

        public void SetLifecycles(List<ISceneLifecycle> values)
        {
            lifecycles = values;
        }

        public async UniTask Initialize(object transData)
        {
            if (IsLifecycleEmpty)
                return;
            
            foreach (var lifecycle in lifecycles)
                await lifecycle.OnInitialize(transData);
        }

        public void Dispose()
        {
            if (IsLifecycleEmpty)
                return;
            
            foreach (var lifecycle in lifecycles)
                lifecycle.OnDispose();
        }

        public async UniTask OnFadeIn()
        {
            if (IsLifecycleEmpty)
                return;

            await UniTask.WhenAll(lifecycles.Select(x => x.OnFadeIn()));
        }
        
        public async UniTask OnFadeOut()
        {
            if (IsLifecycleEmpty)
                return;

            await UniTask.WhenAll(lifecycles.Select(x => x.OnFadeOut()));
        }
    }
}