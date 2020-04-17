using System;
using UniRx;
using UnityEngine.Playables;

namespace Gamebase.Core.Timeline
{
    public abstract class WaitForInputBehaviour : PlayableBehaviour
    {
        private bool isLooping = false;

        private IDisposable disposable;

        private void Dispose()
        {
            disposable?.Dispose();
        }

        protected abstract IObservable<Unit> OnRegisterInputTrigger(Playable playable);
        
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            var observable = OnRegisterInputTrigger(playable);
            if (observable == null)
                return;

            disposable = observable.Subscribe(_ => isLooping = false);
            isLooping = true;
        }
        
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (isLooping && UnityEngine.Application.isPlaying)
            {
                var rootPlayable = playable.GetGraph().GetRootPlayable(0);
                rootPlayable.SetTime(rootPlayable.GetTime() - playable.GetTime());
            }
            else
            {
                Dispose();
            }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (isLooping)
                return;
            
            var rootPlayable = playable.GetGraph().GetRootPlayable(0);
            rootPlayable.SetTime(rootPlayable.GetTime() + playable.GetDuration() - playable.GetTime());
        }   
    }
}