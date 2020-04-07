using System.IO;
using System.Linq;
using UnityEditor;
using Zenject.Internal;

namespace Gamebase.Editor
{
    internal static class GamebaseMenuItems
    {
        [MenuItem("Assets/Create/Gamebase/Template Scene")]
        private static void CreateTemplateScene()
        {
            string selectedDir = ZenUnityEditorUtil.GetSelectedAssetPathsInProjectsTab().FirstOrDefault();
            if (string.IsNullOrEmpty(selectedDir))
                return;
            
            string templateGuid = AssetDatabase.FindAssets("SceneTemplate").FirstOrDefault();
            string srcPath = AssetDatabase.GUIDToAssetPath(templateGuid);
            string dstPath = Path.Combine(selectedDir, "Scene.unity");
            AssetDatabase.CopyAsset(srcPath, dstPath);
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Create/Gamebase/Template Application Scene")]
        private static void CreateTemplateApplicationScene()
        {
            string selectedDir = ZenUnityEditorUtil.GetSelectedAssetPathsInProjectsTab().FirstOrDefault();
            CreateTemplateApplicationScene(selectedDir);
        }

        internal static void CreateTemplateApplicationScene(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
                return;

            string templateGuid = AssetDatabase.FindAssets("ApplicationTemplate").FirstOrDefault();
            string srcPath = AssetDatabase.GUIDToAssetPath(templateGuid);
            string dstPath = Path.Combine(path, "Application.unity");
            AssetDatabase.CopyAsset(srcPath, dstPath);
            AssetDatabase.Refresh();
        }
    }
}