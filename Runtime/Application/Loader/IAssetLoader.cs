using System.Collections.Generic;
using UniRx.Async;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Gamebase.Application.Loader
{
    public interface IAssetLoader
    {
        UniTask<long> GetDownloadSize(IList<object> keys);
        
        UniTask Download(IList<object> keys);
        
        UniTask<AsyncOperationHandle<T>> Load<T>(object key);
        
        void Unload(AsyncOperationHandle handle);
    }
}