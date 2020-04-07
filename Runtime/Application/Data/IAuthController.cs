using UniRx.Async;

namespace Gamebase.Application.Data
{
    public interface IAuthController
    {
        UniTask<string> SignIn();
        
        UniTask SignOut();
    }
}