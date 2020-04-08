using UnityEngine;

namespace Gamebase
{
    public sealed class TagAttribute : PropertyAttribute
    {
        
    }

#if UNITY_EDITOR
    namespace Internal
    {
        using UnityEditor;
    
        [CustomPropertyDrawer(typeof(TagAttribute))]
        public class TagFieldDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                if (fieldInfo.FieldType == typeof(string))
                {
                    property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
                }
            }
        }
    }
#endif
}
