using System.Collections.Generic;
using UnityEditor;

namespace Gamebase.Editor
{
    internal static class DeveloperSettings
    {
        private const string PrefabVariantRepairPrefsKey = nameof(PrefabVariantRepairPrefsKey);
        
        [SettingsProvider]
        private static SettingsProvider PreferenceView()
        {
            var provider = new SettingsProvider( "Preferences/Gamebase", SettingsScope.User ) {
                label = "Gamebase", // メニュー名やタイトルで利用される
                guiHandler = ( searchText ) =>
                {
                    EditorGUI.BeginChangeCheck();

                    var enablePrefabVariantRepair = EditorGUILayout.Toggle("Prefab Variant Repair",
                        EditorPrefs.GetBool(PrefabVariantRepairPrefsKey, true));
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        EditorPrefs.SetBool(PrefabVariantRepairPrefsKey, enablePrefabVariantRepair);
                    }
                },
                keywords = new HashSet<string>( new[] { "Gamebase" } ) // 検索でヒットさせたいワード
            };
            return provider;
        }

        public static bool EnablePrefabVariantRepair => EditorPrefs.GetBool(PrefabVariantRepairPrefsKey, true);
    }
}