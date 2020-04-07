using Gamebase.Data.Entity;
using Gamebase.Domain.Repository;
using UniRx.Async;
using UnityEngine;

namespace Gamebase.Data.Repository
{
    public sealed class SoundConfigRepository : ISoundConfigRepository
    {
        private const string PlayerPrefsKey = "SoundConfig";
        
        public async UniTask<SoundConfigEntity> Get()
        {
            var json = PlayerPrefs.GetString(PlayerPrefsKey, null);
            if (json == null)
                return null;

            var entity = await UniTask.Run(() => JsonUtility.FromJson<SoundConfigEntity>(json));
            return entity;
        }

        public async UniTask Put(SoundConfigEntity value)
        {
            var json = await UniTask.Run(() => JsonUtility.ToJson(value));
            PlayerPrefs.SetString(PlayerPrefsKey, json);
        }
    }
}