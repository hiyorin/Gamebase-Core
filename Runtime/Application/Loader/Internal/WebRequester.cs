using JetBrains.Annotations;
using UniRx.Async;
using UnityEngine.Networking;

namespace Gamebase.Application.Loader.Internal
{
    [PublicAPI]
    internal sealed class WebRequester : IWebRequester
    {
        private const string TimeoutMessage = "Request timeout";

        private readonly NetworkSettings settings;

        private readonly IErrorRequester errorRequester;

        public WebRequester(NetworkSettings settings, IErrorRequester errorRequester)
        {
            this.settings = settings;
            this.errorRequester = errorRequester;
        }

        private async UniTask SendRequest(UnityWebRequest request)
        {
            request.timeout = settings.Timeout;
            await request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                if (request.error == TimeoutMessage)
                    errorRequester.Request(ErrorMessage.Create(ErrorStatus.Timeout, request.error));
                else
                    errorRequester.Request(ErrorMessage.Create(ErrorStatus.Fatal, request.error));
            }
        }
        
        #region IWebRequester implementation

        public async UniTask<DownloadHandler> Get(string url)
        {
            var request = UnityWebRequest.Get(url);
            await SendRequest(request);
            return request.downloadHandler;
        }

        public async UniTask<DownloadHandler> Post(string url)
        {
            var request = UnityWebRequest.Post(url, "");
            await SendRequest(request);
            return request.downloadHandler;
        }

        #endregion
    }
}
