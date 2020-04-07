using System;
using Gamebase.Presentation.View;
using JetBrains.Annotations;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Gamebase.Application.Scene.Internal
{
    [PublicAPI]
    internal sealed class SceneController : IInitializable, ISceneController
    {
        private enum InitState
        {
            None,
            Initializing,
            Initialized,
        }
        
        private readonly CanvasGroupFader transition = default;

        private readonly SceneGeneralSettings generalSettings = default;
        
        private readonly SceneTransSettings transSettings = default;

        private SceneContext currScene;

        private SceneContext prevScene;

        private object transData;

        private InitState initState = InitState.None;

        private bool transitioning = false;
        
        private readonly ISubject<string> onTransComplete = new Subject<string>();

        public SceneController(CanvasGroupFader transition, SceneGeneralSettings generalSettings,
            SceneTransSettings transSettings)
        {
            this.transition = transition;
            this.generalSettings = generalSettings;
            this.transSettings = transSettings;
        }

        private async UniTask Initialize()
        {
            initState = InitState.Initializing;
            
            // どこからでも開始できるようにする
            // 子シーンを取得する
            var activeScene = SceneManager.GetActiveScene();
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.handle != activeScene.handle)
                {
                    currScene = new SceneContext(scene.name);
                    if (generalSettings.ChangeActiveScene)
                    {
                        await UniTask.WaitUntil(() => scene.isLoaded);
                        SceneManager.SetActiveScene(scene);
                    }

                    break;
                }
            }
            
            if (CollectLifecycleInterfaces(currScene)) 
                await currScene.Initialize(transData);
            await FadeIn(currScene);

            initState = InitState.Initialized;
        }
        
        void IInitializable.Initialize()
        {
            if (generalSettings.AutoInitialize && initState == InitState.None)
                Initialize().Forget();
        }

        private async UniTask ChangeScene(string sceneName)
        {
            if (initState != InitState.Initialized)
                throw new InvalidOperationException($"Not initialized (sceneName:{sceneName})");
            
            if (transitioning)
                throw new InvalidOperationException($"In transition (sceneName:{sceneName})");
            
            if (string.IsNullOrEmpty(sceneName) || sceneName == currScene?.Name)
            {
                onTransComplete.OnNext(sceneName);
                return;
            }

            transitioning = true;
            prevScene = currScene;
            currScene = new SceneContext(sceneName);

            await FadeOut(prevScene);
            await Unload(prevScene);
            await Load(currScene, transData, generalSettings.ChangeActiveScene);
            await FadeIn(currScene);
            
            onTransComplete.OnNext(sceneName);
            transitioning = false;
        }

        private async UniTask FadeOut(SceneContext context)
        {
            if (context == null)
                return;
            
            await context.OnFadeOut();
            if (transition != null)
                await transition.FadeOut(transSettings?.FadeDuration ?? 0.2f);
            else
                Debug.LogAssertion("Transition(CanvasGroupFader)が設定されていません SceneMangementInstaller.Transitionで設定してください 入力ができる状態のまま遷移しようとしています");
        }

        private async UniTask FadeIn(SceneContext context)
        {
            if (transition != null)
                await transition.FadeIn(transSettings?.FadeDuration ?? 0.2f);
            await context.OnFadeIn();
        }
        
        private static async UniTask Load(SceneContext context, object transData, bool activeChange)
        {
            await SceneManager.LoadSceneAsync(context.Name, LoadSceneMode.Additive);

            if (CollectLifecycleInterfaces(context))
                await context.Initialize(transData);

            var scene = SceneManager.GetSceneByName(context.Name);
            if (activeChange)
                SceneManager.SetActiveScene(scene);
        }

        private static async UniTask Unload(SceneContext context)
        {
            if (string.IsNullOrEmpty(context?.Name))
                return;
            
            context.Dispose();
            await SceneManager.UnloadSceneAsync(context.Name);
        }
        
        private static bool CollectLifecycleInterfaces(SceneContext context)
        {
            var scene = SceneManager.GetSceneByName(context.Name);
            foreach (var rootObject in scene.GetRootGameObjects())
            {
                var sceneContext = rootObject.GetComponent<Zenject.SceneContext>();
                if (sceneContext != null)
                {
                    context.SetLifecycles(sceneContext.Container.ResolveAll<ISceneLifecycle>());
                    return true;
                }
            }

            return false;
        }
        
        #region ISceneController implementation
        
        bool ISceneController.Initialized => initState == InitState.Initialized;

        async UniTask ISceneController.Initialize()
        {
            if (initState == InitState.None)
                await Initialize();
            else if (initState == InitState.Initializing)
                await UniTask.WaitUntil(() => initState == InitState.Initialized);
        }
        
        void ISceneController.Change(string sceneName)
        {
            ChangeScene(sceneName).Forget();
        }

        void ISceneController.Change(string sceneName, object transData)
        {
            this.transData = transData;
            ChangeScene(sceneName).Forget();
        }

        T ISceneController.GetTransData<T>()
        {
            if (transData is T)
                return (T) transData;
            else
                return default(T);
        }

        public IObservable<string> OnTransCompleteAsObservable()
        {
            return onTransComplete;
        }
        
        #endregion
    }
}