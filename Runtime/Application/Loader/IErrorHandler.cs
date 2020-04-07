using UniRx.Async;

namespace Gamebase.Application.Loader
{
    public interface IErrorHandler
    {
        UniTask<ErrorResult> Handle(ErrorMessage message);
    }
}