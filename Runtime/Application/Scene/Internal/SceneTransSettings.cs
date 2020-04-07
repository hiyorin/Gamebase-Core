using System;
using UnityEngine;

namespace Gamebase.Application.Scene.Internal
{
    [Serializable]
    internal sealed class SceneTransSettings
    {
        [SerializeField] private float fadeDuration = 0.2f;

        public float FadeDuration => fadeDuration;
    }
}