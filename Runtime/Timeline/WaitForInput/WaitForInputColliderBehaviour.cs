using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Playables;

namespace Gamebase.Core.Timeline
{
    [Serializable]
    public sealed class WaitForInputColliderBehaviour : WaitForInputBehaviour
    {
        [Serializable]
        public enum Method
        {
            TriggerEnter,
            TriggerExit,
            CollisionEnter,
            CollisionExit,
        }

        [SerializeField] private Method method = Method.TriggerEnter;
        
        [SerializeField] private ExposedReference<Collider> colliderReference = default;

        protected override IObservable<Unit> OnRegisterInputTrigger(Playable playable)
        {
            var collider = colliderReference.Resolve(playable.GetGraph().GetResolver());
            if (collider == null)
                return null;

            switch (method)
            {
                case Method.TriggerEnter:
                    return collider.OnTriggerEnterAsObservable().AsUnitObservable();
                case Method.TriggerExit:
                    return collider.OnTriggerExitAsObservable().AsUnitObservable();
                case Method.CollisionEnter:
                    return collider.OnCollisionEnterAsObservable().AsUnitObservable();
                case Method.CollisionExit:
                    return collider.OnCollisionExitAsObservable().AsUnitObservable();
                default:
                    Debug.unityLogger.LogError(nameof(WaitForInputColliderBehaviour), $"Not implementation {method}");
                    return null;
            }
        }
    }
}