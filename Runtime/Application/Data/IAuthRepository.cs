using UniRx.Async;

namespace Gamebase.Application.Data
{
    public interface IAuthRepository
    {
        /// <summary>
        /// 認証サーバーのUserIdを取得
        /// </summary>
        /// <returns></returns>
        UniTask<string> SignIn();

        UniTask SignOut();
    }
}