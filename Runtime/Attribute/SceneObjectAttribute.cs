using UnityEngine;

namespace Gamebase
{
    public sealed class SceneObjectAttribute : PropertyAttribute
    {
        
    }
    
#if UNITY_EDITOR
    namespace Internal
    {
        using UnityEditor;
    
        [CustomPropertyDrawer(typeof(SceneObjectAttribute))]
        public class SceneObjectDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                var sceneObj = property.objectReferenceValue;
                property.objectReferenceValue = EditorGUI.ObjectField(position, label, sceneObj, typeof(SceneAsset), false);
            }
        }
    }
#endif
}