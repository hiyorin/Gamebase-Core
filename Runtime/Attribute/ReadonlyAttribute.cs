using UnityEngine;

namespace Gamebase
{
    public sealed class ReadonlyAttribute : PropertyAttribute
    {
        
    }
    
#if UNITY_EDITOR
    namespace Internal
    {
        using UnityEditor;
    
        [CustomPropertyDrawer(typeof(ReadonlyAttribute))]
        public class ReadonlyDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label, true);
                GUI.enabled = true;
            }
        }
    }
#endif
}