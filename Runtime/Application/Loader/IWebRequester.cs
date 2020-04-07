using UniRx.Async;
using UnityEngine.Networking;

namespace Gamebase.Application.Loader
{
    public interface IWebRequester
    {
        UniTask<DownloadHandler> Get(string url);
     
        UniTask<DownloadHandler> Post(string url);
    }
}