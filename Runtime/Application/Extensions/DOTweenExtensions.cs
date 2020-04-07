using System;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Async;

namespace Gamebase
{
    public static class DOTweenExtensions
    {
        public static IObservable<Unit> OnFinishAsObservable(this Tween self)
        {
            return new WaitUntil(() => !self.IsPlaying()).ToObservable();
        }

        public static IObservable<Unit> OnCompleteAsObservable(this Tween self)
        {
            return Observable.FromEvent<TweenCallback>(
                x => x.Invoke,
                x => self.onComplete += x,
                x => self.onComplete -= x);
        }

        #if UNITY_2018_3_OR_NEWER
        public static async UniTask OnCompleteAsUniTask(this Tween self)
        {
            await Observable.FromEvent<TweenCallback>(
                    x => x.Invoke,
                    x => self.onComplete += x,
                    x => self.onComplete -= x)
                .First();
        }
        #endif
        
        public static IObservable<Unit> OnPlayAsObservable(this Tween self)
        {
            return Observable.FromEvent<TweenCallback>(
                x => x.Invoke,
                x => self.onPlay += x,
                x => self.onPlay -= x);
        }

        public static IObservable<Unit> OnPauseAsObservable(this Tween self)
        {
            return Observable.FromEvent<TweenCallback>(
                x => x.Invoke,
                x => self.onPause += x,
                x => self.onPause -= x);
        }

        public static IObservable<Unit> OnUpdateAsObservable(this Tween self)
        {
            return Observable.FromEvent<TweenCallback>(
                x => x.Invoke,
                x => self.onUpdate += x,
                x => self.onUpdate -= x);
        }

        public static IObservable<Unit> OnRewindAsObservable(this Tween self)
        {
            return Observable.FromEvent<TweenCallback>(
                x => x.Invoke,
                x => self.onRewind += x,
                x => self.onRewind -= x);
        }

        public static IObservable<Unit> OnStepCompleteAsObservable(this Tween self)
        {
            return Observable.FromEvent<TweenCallback>(
                x => x.Invoke,
                x => self.onStepComplete += x,
                x => self.onStepComplete -= x);
        }

        public static IObservable<int> OnWaypointChangeAsObservable(this Tween self)
        {
            return Observable.FromEvent<TweenCallback<int>, int>(
                x => y => x.Invoke(y),
                x => self.onWaypointChange += x,
                x => self.onWaypointChange -= x);
        }

        public static IObservable<Unit> OnKillAsObservable(this Tween self)
        {
            return Observable.FromEvent<TweenCallback>(
                x => x.Invoke,
                x => self.onKill += x,
                x => self.onKill -= x);
        }
    }
}
