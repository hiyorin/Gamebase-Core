using System;
using UnityEngine;

namespace Gamebase.Application
{
    [Serializable]
    public sealed class PlatformStoreSettings
    {
        [SerializeField] private string playStoreUrl = null;

        [SerializeField] private string appStoreUrl = null;

        public string StoreUrl
        {
            get
            {
#if UNITY_ANDROID
                return playStoreUrl;
#elif UNITY_IOS
                return appStoreUrl;
#else
                Debug.unityLogger.LogError(GetType().Name, "This platform is not implementation.");
                return string.Empty;
#endif
            }
        }
    }
}
