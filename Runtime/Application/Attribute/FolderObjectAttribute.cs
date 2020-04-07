using UnityEngine;

namespace Gamebase
{
    public sealed class FolderObjectAttribute : PropertyAttribute
    {
        
    }
#if UNITY_EDITOR
    namespace Internal
    {
        using UnityEditor;
    
        [CustomPropertyDrawer(typeof(FolderObjectAttribute))]
        public class FolderObjectDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                var obj = property.objectReferenceValue;
                obj = EditorGUI.ObjectField(position, label, obj, typeof(Object), false);

                if (obj != null && !AssetDatabase.IsValidFolder(AssetDatabase.GetAssetPath(obj)))
                    return;
                
                property.objectReferenceValue = obj;
            }
        }
    }
#endif
}