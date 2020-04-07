using UnityEditor;
using UnityEngine;

namespace Gamebase.Editor
{
    public abstract class CreateWizard<T> : ScriptableWizard where T:ScriptableObject
    {
        protected static TU DisplayCreateWizard<TU>(string title) where TU:ScriptableWizard
        {
            return DisplayWizard<TU>(title, "Create", "Edit");
        }
        
        private T settings = default;
        
        private SerializedObject serializedObject;
        
        private void OnEnable()
        {
            settings = GamebaseEditorUtility.Load<T>();
            serializedObject = new SerializedObject(settings);
        }

        private void OnDisable()
        {
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
        }

        protected override bool DrawWizardGUI()
        {
            OnDraw(settings, serializedObject);
            
            serializedObject.ApplyModifiedProperties();
            
            return true;
        }

        /// <summary>
        /// 継承される場合privateでは呼ばれない...
        /// </summary>
        protected void OnWizardCreate()
        {
            OnCreate(settings, serializedObject);
        }

        /// <summary>
        /// 継承される場合privateでは呼ばれない...
        /// </summary>
        protected void OnWizardOtherButton()
        {
            Selection.objects = new Object[] { settings };
        }
        
        protected abstract void OnDraw(T settings, SerializedObject serializedObject);

        protected abstract void OnCreate(T settings, SerializedObject serializedObject);
    }
}