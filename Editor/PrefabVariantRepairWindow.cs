using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Gamebase.Editor
{
    public sealed class PrefabVariantRepairWindow : EditorWindow
    {
        private readonly List<GameObject> repairObjects = new List<GameObject>();
        
        private readonly List<GameObject> completeObjects = new List<GameObject>();
        
        private void OnEnable()
        {
            titleContent = new GUIContent(nameof(PrefabVariantRepairWindow));
        }

        private void OnGUI()
        {
            foreach (var repairObject in repairObjects)
            {
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.LabelField(repairObject.name);
                
                if (GUILayout.Button("Select"))
                {
                    Selection.objects = new Object[] { repairObject };
                }

                if (GUILayout.Button("Open"))
                {
                    AssetDatabase.OpenAsset(repairObject);
                }
                
                if (GUILayout.Button("Complete"))
                {
                    completeObjects.Add(repairObject);
                }
                
                EditorGUILayout.EndHorizontal();
            }

            RemoveCompleteObjects();
        }

        private void RemoveCompleteObjects()
        {
            foreach (var completeObject in completeObjects)
                repairObjects.Remove(completeObject);
            completeObjects.Clear();
        }
        
        public void AddRepairObject(IEnumerable<GameObject> values)
        {
            foreach (var gameObject in values)
                if (!repairObjects.Contains(gameObject))
                    repairObjects.Add(gameObject);
        }
    }
}