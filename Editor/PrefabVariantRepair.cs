using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gamebase.Editor
{
    /// <summary>
    /// Sequence
    /// 1. OnPreprocessAsset
    ///      Duplicate AddedGameObjects
    ///      Add task
    ///      Save Prefab
    /// 2. OnPreprocessAsset
    ///      Return
    /// 3. OnPostprocessAllAssets
    ///      Execute task
    ///      Delete duplicated objects
    ///      Save Prefab
    /// 4. OnPreprocessAsset
    ///      Remove task
    /// </summary>
    public sealed class PrefabVariantRepair : AssetPostprocessor
    {
        private struct AddedPrefab
        {
            public string AssetPath;

            public AddedGameObjectContext[] AddedGameObjects;

            public bool PostProcessed;
        }
        
        private struct AddedGameObjectContext
        {
            public int InstanceId;
            public string TempObjectName;
        }

        private const string SalvageTag = "[Salvage]";
        
        private const string TempTag = "[Temp]";
        
        private static readonly List<AddedPrefab> PreAddedPrefabs = new List<AddedPrefab>();

        private static bool IsProcessing = false;
        
        private readonly List<AddedGameObject> addedGameObjects = new List<AddedGameObject>();
        
        /// <summary>
        ///　Specify a larger value such as Baum2 importer.
        /// </summary>
        public override int GetPostprocessOrder()
        {
            return 2000;
        }
        
        #region UnityEvent implementation
        
        private void OnPreprocessAsset()
        {
            addedGameObjects.Clear();
            if (!GetAddedGameObjects(assetPath, addedGameObjects))
                return;
            
            // load prefab variant
            var prefabContents = PrefabUtility.LoadPrefabContents(assetPath);
            var isDirty = false;

            var salvageContainer = new GameObject(SalvageTag);
            salvageContainer.transform.SetParent(prefabContents.transform);
            
            var addedPrefab = new AddedPrefab();
            addedPrefab.AssetPath = assetPath;
            addedPrefab.AddedGameObjects = new AddedGameObjectContext[addedGameObjects.Count];
            for (var i = 0; i < addedGameObjects.Count; i++)
            {
                var addedGameObject = addedGameObjects[i];
                if (addedGameObject.instanceGameObject.name.StartsWith(SalvageTag))
                    continue;
                
                if (addedGameObject.instanceGameObject.name.StartsWith(TempTag))
                    continue;

                var tempObject = GenerateTempObject(addedGameObject.instanceGameObject, salvageContainer.transform);
                var context = new AddedGameObjectContext
                {
                    InstanceId = addedGameObject.instanceGameObject.GetInstanceID(),
                    TempObjectName = tempObject.name,
                };
                
                addedPrefab.AddedGameObjects[i] = context;
                isDirty = true;
            }
            
            
            // unload prefab variant
            if (isDirty)
            {
                PreAddedPrefabs.Add(addedPrefab);
                PrefabUtility.SaveAsPrefabAsset(prefabContents, assetPath);
            }

            PrefabUtility.UnloadPrefabContents(prefabContents);
        }

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            // force single task
            if (IsProcessing)
            {
                return;
            }

            IsProcessing = true;
            
            List<GameObject> repairedObjects = new List<GameObject>();

            for (var i = 0; i < PreAddedPrefabs.Count; i++)
            {
                var preAddedPrefab = PreAddedPrefabs[i];
                if (!PrefabVariantUtility.LoadPrefabVariant(preAddedPrefab.AssetPath, out var prefab))
                    continue;
                
                var salvageContainer = PrefabUtility.GetAddedGameObjects(prefab).LastOrDefault();
                if (salvageContainer == null)
                    continue;

                var postAddedGameObjects = salvageContainer.instanceGameObject.transform.GetComponentsInChildren<Transform>();
                if (postAddedGameObjects.Length <= 0)
                    continue;

                // load prefab variant
                var prefabContents = PrefabUtility.LoadPrefabContents(preAddedPrefab.AssetPath);
                var destroyCount = 0;
                
                // delete non problem objects
                foreach (var preAddedGameObject in preAddedPrefab.AddedGameObjects)
                {
                    if (postAddedGameObjects.Any(x => x.GetInstanceID() == preAddedGameObject.InstanceId))
                    {
                        Object.DestroyImmediate(prefab.transform.Find(preAddedGameObject.TempObjectName));
                        destroyCount++;
                    }
                }

                if (destroyCount >= preAddedPrefab.AddedGameObjects.Length)
                {
                    // Success !
                    Object.DestroyImmediate(prefabContents.transform.Find(SalvageTag));
                }
                else
                {
                    // Fail, Add timestamp for name, And Deactivate.
                    var salvage = prefabContents.transform.Find(SalvageTag);
                    salvage.name += $" {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    salvage.gameObject.SetActive(false);
                    repairedObjects.Add(prefab);
                }

                PrefabUtility.SaveAsPrefabAsset(prefabContents, preAddedPrefab.AssetPath);
                PrefabUtility.UnloadPrefabContents(prefabContents);

                preAddedPrefab.PostProcessed = true;
                PreAddedPrefabs[i] = preAddedPrefab;
            }
            
            ConfirmRepairObjectsWindow(repairedObjects);
            IsProcessing = false;
        }
        
        #endregion
        
        #region Private Methods

        private static bool GetAddedGameObjects(string assetPath, List<AddedGameObject> results)
        {
            if (!DeveloperSettings.EnablePrefabVariantRepair)
                return false;

            if (PreAddedPrefabs.Any(x => x.AssetPath == assetPath && x.PostProcessed))
            {
                var preAddedPrefab = PreAddedPrefabs.FirstOrDefault(x => x.AssetPath == assetPath);
                PreAddedPrefabs.Remove(preAddedPrefab);
                return false;
            }

            if (!PrefabVariantUtility.LoadPrefabVariant(assetPath, out var prefab))
                return false;

            var addedGameObjects = PrefabUtility.GetAddedGameObjects(prefab);
            if (addedGameObjects.Count <= 0)
                return false;

            results.AddRange(addedGameObjects);
            
            return true;
        }
        
        private static bool ConfirmRepairObjectsWindow(List<GameObject> repairedObjects)
        {
            if (repairedObjects.Count <= 0)
                return false;

            if (!EditorUtility.DisplayDialog("Broken !", "Prefab variant is broken.\nDo you want to repair it ?", "Yes", "No"))
                return false;
            
            var window = EditorWindow.GetWindow<PrefabVariantRepairWindow>();
            window.AddRepairObject(repairedObjects);
            window.Show();

            return true;
        }
        
        private static string GetHierarchyPath(Transform self)
        {
            string path = self.gameObject.name;
            var parent = self.parent;
            while (parent != null && parent.parent != null)
            {
                path = parent.name + "/" + path;
                parent = parent.parent;
            }
            return path;
        }

        private static Object GenerateTempObject(GameObject source, Transform parent)
        {
            var tempObject = Object.Instantiate(source, parent, true);
            tempObject.name = $"{TempTag} {GetHierarchyPath(source.transform)}";
            return tempObject;
        }
        
        #endregion
    }
}