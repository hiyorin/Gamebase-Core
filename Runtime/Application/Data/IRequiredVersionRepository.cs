using System;
using UniRx.Async;

namespace Gamebase.Application.Data
{
    public interface IRequiredVersionRepository
    {
        UniTask<string> Get();

        IObservable<string> OnChangeAsObservable();
    }
}