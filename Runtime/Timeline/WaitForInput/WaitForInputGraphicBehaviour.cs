using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Gamebase.Core.Timeline
{
    [Serializable]
    public sealed class WaitForInputGraphicBehaviour : WaitForInputBehaviour
    {
        [Serializable]
        public enum Method
        {
            Click,
            Up,
            Down,
            Enter,
            Exit,
        }

        [SerializeField] private Method method = Method.Click;
        
        [SerializeField] private ExposedReference<Graphic> graphicReference = default;

        protected override IObservable<Unit> OnRegisterInputTrigger(Playable playable)
        {
            var graphic = graphicReference.Resolve(playable.GetGraph().GetResolver());
            if (graphic == null)
                return null;

            IObservable<PointerEventData> observable;
            switch (method)
            {
                case Method.Click:
                    observable = graphic.OnPointerClickAsObservable();
                    break;
                case Method.Up:
                    observable = graphic.OnPointerUpAsObservable();
                    break;
                case Method.Down:
                    observable = graphic.OnPointerDownAsObservable();
                    break;
                case Method.Enter:
                    observable = graphic.OnPointerEnterAsObservable();
                    break;
                case Method.Exit:
                    observable = graphic.OnPointerExitAsObservable();
                    break;
                default:
                    Debug.unityLogger.LogError(nameof(WaitForInputGraphicBehaviour), $"Not implementation {method}");
                    return null;
            }

            return observable.AsUnitObservable();
        }
    }
}