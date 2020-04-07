using System.IO;
using UnityEditor;
using UnityEngine;

namespace Gamebase.Editor
{
    public static class PrefabVariantUtility
    {
        public static bool LoadPrefabVariant(string assetPath, out GameObject prefab)
        {
            if (Path.GetExtension(assetPath) != ".prefab")
            {
                prefab = null;
                return false;
            }

            prefab = AssetDatabase.LoadMainAssetAtPath(assetPath) as GameObject;
            if (prefab == null)
            {
                prefab = null;
                return false;
            }

            if (!PrefabUtility.IsPartOfVariantPrefab(prefab))
                return false;

            return true;
        }
    }
}