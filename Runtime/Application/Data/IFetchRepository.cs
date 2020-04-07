using UniRx.Async;

namespace Gamebase.Application.Data
{
    public interface IFetchRepository
    {
        UniTask Fetch(string userId);
    }
}