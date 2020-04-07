using System;
using UnityEngine;

namespace Gamebase.Application.Scene.Internal
{
    [Serializable]
    internal sealed class SceneGeneralSettings
    {
        [SerializeField] private bool autoInitialize = true;
        
        [SerializeField] private bool changeActiveScene = true;

        public bool AutoInitialize => autoInitialize;
        
        public bool ChangeActiveScene => changeActiveScene;
    }
}
