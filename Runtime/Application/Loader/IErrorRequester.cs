using UniRx.Async;

namespace Gamebase.Application.Loader
{
    public interface IErrorRequester
    {
        UniTask<ErrorResult> Request(ErrorMessage errorMessage);
    }
}