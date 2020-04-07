using UnityEditor;
using UnityEngine;

namespace Gamebase.Editor
{
    public static class GamebaseEditorUtility
    {
        public static T Load<T>() where T : ScriptableObject
        {
            var parentDir = "Assets/Editor";
            var dirName = "Gamebase";
            var dir = $"{parentDir}/{dirName}";
            var path = $"{dir}/{typeof(T).Name}.asset";
            
            var instance = AssetDatabase.LoadAssetAtPath<T>(path);
            if (instance == null)
            {
                if (!AssetDatabase.IsValidFolder(parentDir))
                    AssetDatabase.CreateFolder("Assets", "Editor");
                
                if (!AssetDatabase.IsValidFolder(dir))
                    AssetDatabase.CreateFolder(parentDir, dirName);
            
                instance = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(instance, path);
                AssetDatabase.SaveAssets();
            }

            Selection.objects = new Object[] { instance };

            return instance;
        }
    }
}