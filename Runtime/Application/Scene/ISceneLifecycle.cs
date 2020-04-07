using UniRx.Async;

namespace Gamebase.Application.Scene
{
    public interface ISceneLifecycle
    {
        UniTask OnInitialize(object transData);
        
        UniTask OnFadeIn();
        
        UniTask OnFadeOut();
        
        void OnDispose();
    }
}