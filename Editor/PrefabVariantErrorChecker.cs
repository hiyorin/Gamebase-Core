#if false
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Gamebase.Editor
{
    /// <summary>
    /// Notify when a GameObject or Component added to Prefab Variant is deleted.
    /// </summary>
    public sealed class PrefabVariantErrorChecker : AssetPostprocessor
    {
        private sealed class AddedGameObject
        {
            public int InstanceId;
            public string Name;
            public string ParentName;
        }

        private sealed class AddedComponent
        {
            public int InstanceId;
            public string Name;
            public string GameObjectName;
            public int GameObjectInstanceId;
        }


        private static string CurrentAssetPath = null;

        private static List<AddedGameObject> PreAddedGameObjects = null;

        private static List<AddedComponent> PreAddedComponents = null;

        private void OnPreprocessAsset()
        {
            if (!PrefabVariantUtility.LoadPrefabVariant(assetPath, out var obj))
                return;
            
            // オブジェクトが消えたかチェック
            // 同じ名前のGameObjectが同Prefab内にあると､消えてしまう
            // できるだけ違う名前をつけるようにする
            var addedGameObjects = PrefabUtility.GetAddedGameObjects(obj);
            if (addedGameObjects != null && addedGameObjects.Count > 0)
            {
                PreAddedGameObjects = new List<AddedGameObject>();
                foreach (var addedGameObject in addedGameObjects)
                {
                    var parent = addedGameObject.instanceGameObject.transform.parent;
                    PreAddedGameObjects.Add(new AddedGameObject()
                    {
                        InstanceId = addedGameObject.instanceGameObject.GetInstanceID(),
                        Name = addedGameObject.instanceGameObject.name,
                        ParentName = parent != null ? parent.name : null
                    });
                }
            }

            // GetAddedComponentsで取得されるものはobjに入っているコンポーネントでは無いようです
            var addedComponents = PrefabUtility.GetAddedComponents(obj);
            if (addedComponents != null && addedComponents.Count > 0)
            {
                PreAddedComponents = new List<AddedComponent>();
                foreach (var addedComponent in addedComponents)
                {
                    var gameObject = addedComponent.instanceComponent.gameObject;
                    PreAddedComponents.Add(new AddedComponent()
                    {
                        InstanceId = addedComponent.instanceComponent.GetInstanceID(),
                        Name = addedComponent.instanceComponent.name,
                        GameObjectName = gameObject != null ? gameObject.name : null,
                        GameObjectInstanceId = gameObject != null ? gameObject.GetInstanceID() : 0
                    });
                }
            }

            CurrentAssetPath = assetPath;
        }

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            if (string.IsNullOrEmpty(CurrentAssetPath))
                return;

            var obj = AssetDatabase.LoadMainAssetAtPath(CurrentAssetPath) as GameObject;

            if (PreAddedGameObjects != null)
            {
                var postAddedGameObjects = PrefabUtility.GetAddedGameObjects(obj);
                foreach (var preAddedGameObject in PreAddedGameObjects)
                {
                    if (postAddedGameObjects.All(x =>
                        x.instanceGameObject.GetInstanceID() != preAddedGameObject.InstanceId))
                    {
                        Debug.unityLogger.LogError(nameof(PrefabVariantErrorChecker),
                            $"GameObject removed. {preAddedGameObject.ParentName}/{preAddedGameObject.Name}\n{CurrentAssetPath}");
                    }
                }
            }

            if (PreAddedComponents != null)
            {
                var postAddedComponents = PrefabUtility.GetAddedComponents(obj);
                foreach (var preAddedComponent in PreAddedComponents)
                {
                    if (postAddedComponents.All(
                        x => x.instanceComponent.GetInstanceID() != preAddedComponent.InstanceId))
                        Debug.unityLogger.LogError(nameof(PrefabVariantErrorChecker),
                            $"Component removed. {preAddedComponent.GameObjectName}/{preAddedComponent.Name}\n{CurrentAssetPath}");
                }
            }

            CurrentAssetPath = null;
            PreAddedGameObjects = null;
            PreAddedComponents = null;
        }
    }
}
#endif