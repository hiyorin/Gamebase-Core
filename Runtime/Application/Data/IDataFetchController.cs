using UniRx.Async;

namespace Gamebase.Application.Data
{
    public interface IDataFetchController
    {
        UniTask Fetch();
    }
}