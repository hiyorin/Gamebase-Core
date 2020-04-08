using System;
using System.Collections;
using UniRx;
using UniRx.Async;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Gamebase
{
    public static class IAsyncOperationExtensions
    {
        public static IObservable<T> ToObservable<T>(this AsyncOperationHandle<T> ap)
        {
            return Observable.FromEvent<AsyncOperationHandle<T>>(
                    h => ap.Completed += h,
                    h => ap.Completed -= h)
                .Select(x => x.Result);
        }

        public static IEnumerator Progress(this AsyncOperationHandle self, Action<float> progress)
        {
            yield return new UnityEngine.WaitUntil(() =>
            {
                progress?.Invoke(self.PercentComplete);
                return self.IsDone;
            });
        }
        
        public static async UniTask TaskProgress(this AsyncOperationHandle self, Action<float> progress)
        {
            await UniTask.WaitUntil(() =>
            {
                progress?.Invoke(self.PercentComplete);
                return self.IsDone;
            });
        }
        
        public static async UniTask<T> TaskProgress<T>(this AsyncOperationHandle<T> self, Action<float> progress)
        {
            await UniTask.WaitUntil(() =>
            {
                progress?.Invoke(self.PercentComplete);
                return self.IsDone;
            });

            return self.Result;
        }
    }
}
