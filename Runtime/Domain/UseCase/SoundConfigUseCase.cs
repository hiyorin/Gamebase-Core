using Gamebase.Application.Sound;
using Gamebase.Data.Entity;
using Gamebase.Data.Translator;
using Gamebase.Domain.Model;
using Gamebase.Domain.Repository;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace Gamebase.Domain.UseCase
{
    public interface ISoundConfigUseCase
    {
        SoundConfigModel Model { get; }
        UniTask Initialize();
        void Apply();
        void Reset();
        UniTask Save();
    }

    public sealed class SoundConfigUseCase : ISoundConfigUseCase
    {
        [Inject] private ISoundConfigRepository repository = null;

        [Inject] private ISoundVolumeController volumeController = null;

        [Inject] private SoundSettings settings = null;

        private SoundConfigModel model;

        private bool isLoading = false;
        
        #region ISoundConfigUseCase implementation

        public SoundConfigModel Model => model;
        
        public async UniTask Initialize()
        {
            if (model != null)
                return;

            if (isLoading)
            {
                await UniTask.WaitUntil(() => !isLoading);
                return;
            }
            
            isLoading = true;
            var entity = await repository.Get();
            if (entity == null)
            {
                entity = new SoundConfigEntity();
                entity.Volume = settings.DefaultVolume;
            }
            
            model = SoundConfigTranslator.Translate(entity);
            Apply();
            
            Debug.unityLogger.Log(GetType().Name, $"Initialize {model.Volume}");
            isLoading = false;
        }

        public void Apply()
        {
            volumeController.Set(model.Volume);
        }
        
        public void Reset()
        {
            volumeController.Set(settings.DefaultVolume);
            Apply();
        }

        public async UniTask Save()
        {
            var entity = SoundConfigTranslator.Translate(model);
            await repository.Put(entity);
        }
        
        #endregion
    }
}