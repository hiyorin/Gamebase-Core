namespace Gamebase.Editor
{
    public abstract class CustomInspectorBase :
#if ODIN_INSPECTOR
        Sirenix.OdinInspector.Editor.OdinEditor
    {
        
    }
#else
        UnityEditor.Editor
    {
        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }
    }
#endif
}