using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine;

namespace Hephaestus.Scenes.Editor
{
    [CustomEditor(typeof(ScenesManagerConfigConstants))]
    public class ScenesManagerConfigConstantsEditor : UnityEditor.Editor
    {
        private const string EntityType = "Scenes";
        
        private StringBuilder _stringBuilder;
        
        private string _enumClassName = "ScenesManagerConfigConstants";

        private string _path;

        private string _newConstantKey = string.Empty;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var mapConstants = (ScenesManagerConfigConstants)target;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            EditorGUILayout.LabelField("Export:", EditorStyles.largeLabel);
            
            _enumClassName = EditorGUILayout.TextField("Enum Class Name:", _enumClassName);

            if (GUILayout.Button("Export to enum", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                if (!string.IsNullOrEmpty(_path))
                {
                    ExportKeysToEnum(mapConstants, _path);
                }
                else
                {
                    _path = EditorUtility.SaveFilePanelInProject("Pick The Folder", _enumClassName, "cs", "Assets");
                    Debug.Log(_path);
                }
            }
            
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space();
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            EditorGUILayout.LabelField($"Add New {EntityType} Keys:", EditorStyles.largeLabel);
            
            _newConstantKey = EditorGUILayout.TextField("New Key:", _newConstantKey).Replace(' ', '_').ToUpper();

            if (GUILayout.Button($"Add New {EntityType} Key", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                mapConstants.sceneMapKeys.Add(_newConstantKey);
            }
            
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space();
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            EditorGUILayout.LabelField($"List of {EntityType} Keys:", EditorStyles.largeLabel);

            if (mapConstants.sceneMapKeys == null) return;

            for (int i = 0; i < mapConstants.sceneMapKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                mapConstants.sceneMapKeys[i] = EditorGUILayout.TextField(mapConstants.sceneMapKeys[i]);
                
                if (GUILayout.Button("Remove"))
                {
                    mapConstants.sceneMapKeys.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Save Config", GUILayout.ExpandWidth(true), GUILayout.Height(32))) {
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
        
        private void ExportKeysToEnum(ScenesManagerConfigConstants scenesManagerConfigConstants, string path)
        {
            _stringBuilder = new StringBuilder();
            
            _stringBuilder.Append($"public enum {_enumClassName} : byte\n");
            
            _stringBuilder.Append("{\n");

            for (var i = 0; i < scenesManagerConfigConstants.sceneMapKeys.Count; i++)
            {
                var coma = i < scenesManagerConfigConstants.sceneMapKeys.Count - 1 ? "," : string.Empty;
                _stringBuilder.Append($"\t{scenesManagerConfigConstants.sceneMapKeys[i].ToUpper()} = {i}{coma}\n");
            }
            
            _stringBuilder.Append("}");

            File.WriteAllText(path, _stringBuilder.ToString());
            AssetDatabase.Refresh();
        }
    }
}