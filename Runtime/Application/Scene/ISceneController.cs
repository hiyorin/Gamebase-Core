using System;
using UniRx.Async;

namespace Gamebase.Application.Scene
{
    public interface ISceneController
    {   
        /// <summary>
        /// 初期化済みか
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <returns></returns>
        UniTask Initialize();
        
        /// <summary>
        /// シーンの切り替え
        /// </summary>
        /// <param name="sceneName"></param>
        void Change(string sceneName);

        /// <summary>
        /// シーンの切り替え
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="transData"></param>
        void Change(string sceneName, object transData);

        /// <summary>
        /// 遷移元からのデータを取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetTransData<T>();

        /// <summary>
        /// 遷移完了通知
        /// </summary>
        /// <returns></returns>
        IObservable<string> OnTransCompleteAsObservable();
    }
}