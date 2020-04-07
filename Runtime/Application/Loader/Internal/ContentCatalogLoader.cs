using JetBrains.Annotations;
using UniRx.Async;
using UnityEngine.AddressableAssets;

namespace Gamebase.Application.Loader.Internal
{
    [PublicAPI]
    internal sealed class ContentCatalogLoader : IContentCatalogLoader
    {
        private readonly IErrorRequester errorRequester;
        
        public ContentCatalogLoader(IErrorRequester errorRequester)
        {
            this.errorRequester = errorRequester;
        }

        public async UniTask Load(string domain, string catalogName)
        {
            var uri = $"{domain}/{PlatformMappingService.GetPlatform()}/{catalogName}";

            while (true)
            {
                var operation = Addressables.LoadContentCatalogAsync(uri);
                await operation.Task;
                if (operation.OperationException != null)
                {
                    var errorResult =
                        await errorRequester.Request(ErrorMessage.Create(ErrorStatus.Fatal,
                            operation.OperationException));
                    if (errorResult == ErrorResult.Retry)
                        continue;
                    else if (errorResult == ErrorResult.Error)
                        throw operation.OperationException;
                }
                
                Addressables.ResourceLocators.Add(operation.Result);
            }
        }
    }
}