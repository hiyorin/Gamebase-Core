using DG.Tweening;
using UniRx.Async;
using UnityEngine;

namespace Gamebase.Presentation.View
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class CanvasGroupFader : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1.0f;
        }

        public void FadeInImmediate()
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.blocksRaycasts = false;
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;
        }

        public async UniTask FadeIn(float duration)
        {
            await canvasGroup.DOFade(0.0f, duration).OnCompleteAsUniTask();
            canvasGroup.blocksRaycasts = false;
        }

        public async UniTask FadeOut(float duration)
        {
            canvasGroup.blocksRaycasts = true;
            await canvasGroup.DOFade(1.0f, duration).OnCompleteAsUniTask();
        }
    }
}