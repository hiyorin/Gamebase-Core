using UnityEngine;
using Zenject;

namespace Gamebase.Component
{
    [RequireComponent(typeof(Canvas))]
    public sealed class CameraCanvas : MonoBehaviour
    {
        [InjectOptional] private Camera uiCamera = default;

        private void Start()
        {
            if (uiCamera == null)
                return;

            var canvas = GetComponent<Canvas>();

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = uiCamera;
        }
    }
}