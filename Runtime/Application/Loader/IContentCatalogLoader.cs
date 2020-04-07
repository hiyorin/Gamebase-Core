using UniRx.Async;

namespace Gamebase.Application.Loader
{
    public interface IContentCatalogLoader
    {
        UniTask Load(string domain, string catalogName);
    }
}