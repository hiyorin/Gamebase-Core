using Gamebase.Data.Entity;
using UniRx.Async;

namespace Gamebase.Domain.Repository
{
    public interface ISoundConfigRepository
    {
        UniTask<SoundConfigEntity> Get();
        
        UniTask Put(SoundConfigEntity value);
    }
}