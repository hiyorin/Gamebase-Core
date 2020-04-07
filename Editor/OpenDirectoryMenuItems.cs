using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gamebase.Editor
{
    internal static class OpenDirectoryMenuItems
    {
        [MenuItem("Tools/Gamebase/Open Directory/Caching")]
        private static void OpenCaching()
        {
            Process.Start(Caching.currentCacheForWriting.path);
        }

        [MenuItem("Tools/Gamebase/Open Directory/Addressable Runtime Path")]
        private static void OpenAddressableRuntimePath()
        {
            Process.Start(Addressables.RuntimePath);
        }
        
        [MenuItem("Tools/Gamebase/Open Directory/Persistant Data Path")]
        private static void OpenPersistentDataPath()
        {
            Process.Start(UnityEngine.Application.persistentDataPath);
        }

        [MenuItem("Tools/Gamebase/Open Directory/Streaming Assets Path")]
        private static void OpenStreamingAssetsPath()
        {
            Process.Start(UnityEngine.Application.streamingAssetsPath);
        }

        [MenuItem("Tools/Gamebase/Open Directory/Temporary Cache Path")]
        private static void Open()
        {
            Process.Start(UnityEngine.Application.temporaryCachePath);
        }
    }
}