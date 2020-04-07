using UnityEditor;
using UnityEngine;

namespace Gamebase.Editor
{
    internal sealed class NewProjectWindow : EditorWindow
    {
        [MenuItem("Tools/Gamebase/New Project", false, 0)]
        private static void Open()
        {
            var window = GetWindow<NewProjectWindow>("NewProject");
            window.Show();
        }

        private string projectName = "Example";
        
        private void OnGUI()
        {
            projectName = EditorGUILayout.TextField("Name", projectName);
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Create"))
            {
                var root = CreateFolder("Assets", projectName);
                
                var installer = CreateFolder(root, "Installer");
                var scripts = CreateFolder(root, "Scripts");
                
                var scenes = CreateFolder(root, "Scenes");
                GamebaseMenuItems.CreateTemplateApplicationScene(scenes);

                var application = CreateFolder(scripts, "Application");
                CreateFolder(application, "Installer");
                
                var data = CreateFolder(scripts, "Data");
                CreateFolder(data, "Entity");
                CreateFolder(data, "Repository");
                
                var domain = CreateFolder(scripts, "Domain");
                CreateFolder(domain, "UseCase");
                CreateFolder(domain, "Repository");
                
                var presentation = CreateFolder(scripts, "Presentation");
                CreateFolder(presentation, "Presenter");
                CreateFolder(presentation, "View");

                AssetDatabase.Refresh();
            }
        }

        private string CreateFolder(string parentFolder, string newFolderName)
        {
            var guid = AssetDatabase.CreateFolder(parentFolder, newFolderName);
            return AssetDatabase.GUIDToAssetPath(guid);
        }
    }
}