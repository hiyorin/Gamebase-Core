using System;
using System.Collections;
using UniRx.Async;
using UnityEngine;

namespace Gamebase
{
    public static class UniTaskExtensions
    {
        public static IEnumerator WaitForCompletion(this UniTask self)
        {
            yield return new WaitUntil(() => self.IsCompleted);
        }

        public static IEnumerator WaitForCompletion<T>(this UniTask<T> self, Action<T> result = null)
        {
            yield return new WaitUntil(() => self.IsCompleted);
            result?.Invoke(self.Result);
        }
    }
}