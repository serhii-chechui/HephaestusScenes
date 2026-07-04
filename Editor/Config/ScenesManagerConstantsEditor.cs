using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine;

namespace WTFGames.Hephaestus.ScenesSystem.Editor
{
    [CustomEditor(typeof(ScenesManagerConstants))]
    public class ScenesManagerConstantsEditor : UnityEditor.Editor
    {
        private const string EntityType = "Scenes";

        private StringBuilder _stringBuilder;

        private string _enumClassName = "ScenesManagerConfigConstants";

        private string _newConstantKey = string.Empty;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var mapConstants = (ScenesManagerConstants)target;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Export:", EditorStyles.largeLabel);

            _enumClassName = EditorGUILayout.TextField("Enum Class Name:", _enumClassName);

            EditorGUILayout.BeginHorizontal();

            if (!string.IsNullOrEmpty(mapConstants.enumsPath))
            {
                EditorGUILayout.LabelField("Path", mapConstants.enumsPath, GUILayout.ExpandWidth(true));
            }

            if (GUILayout.Button("Pick", GUILayout.Width(96)))
            {
                var absolutePath = EditorUtility.OpenFolderPanel("Pick The Folder", Application.dataPath, "");

                if (!string.IsNullOrEmpty(absolutePath))
                {
                    var relativePath = FileUtil.GetProjectRelativePath(absolutePath);

                    if (string.IsNullOrEmpty(relativePath))
                    {
                        EditorUtility.DisplayDialog("Invalid Folder",
                            "The folder must be inside the project, otherwise the path breaks on other machines.", "OK");
                    }
                    else
                    {
                        Undo.RecordObject(mapConstants, "Change Enums Path");
                        mapConstants.enumsPath = relativePath;
                        EditorUtility.SetDirty(mapConstants);
                    }
                }
            }

            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Export to enum", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                if (string.IsNullOrEmpty(mapConstants.enumsPath))
                {
                    EditorUtility.DisplayDialog("No Export Path", "Pick the export folder first.", "OK");
                }
                else if (string.IsNullOrEmpty(_enumClassName))
                {
                    EditorUtility.DisplayDialog("No Enum Class Name", "Enter the enum class name first.", "OK");
                }
                else
                {
                    ExportKeysToEnum(mapConstants);
                }
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField($"Add New {EntityType} Keys:", EditorStyles.largeLabel);

            _newConstantKey = EditorGUILayout.TextField("New Key:", _newConstantKey).Replace(' ', '_').ToUpper();

            if (GUILayout.Button($"Add New {EntityType} Key", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                if (!string.IsNullOrEmpty(_newConstantKey))
                {
                    Undo.RecordObject(mapConstants, "Add Scene Key");
                    mapConstants.sceneMapKeys.Add(_newConstantKey);
                    EditorUtility.SetDirty(mapConstants);
                    _newConstantKey = string.Empty;
                }
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField($"List of {EntityType} Keys:", EditorStyles.largeLabel);

            if (mapConstants.sceneMapKeys == null)
            {
                EditorGUILayout.EndVertical();
                return;
            }

            var removeIndex = -1;

            for (int i = 0; i < mapConstants.sceneMapKeys.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

                var newKey = EditorGUILayout.TextField(mapConstants.sceneMapKeys[i]);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(mapConstants, "Edit Scene Key");
                    mapConstants.sceneMapKeys[i] = newKey;
                    EditorUtility.SetDirty(mapConstants);
                }

                if (GUILayout.Button("Remove"))
                {
                    removeIndex = i;
                }

                EditorGUILayout.EndHorizontal();
            }

            if (removeIndex >= 0)
            {
                Undo.RecordObject(mapConstants, "Remove Scene Key");
                mapConstants.sceneMapKeys.RemoveAt(removeIndex);
                EditorUtility.SetDirty(mapConstants);
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            if (GUILayout.Button("Save Config", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private void ExportKeysToEnum(ScenesManagerConstants scenesManagerConstants)
        {
            _stringBuilder = new StringBuilder();

            _stringBuilder.Append($"public enum {_enumClassName} : byte\n");

            _stringBuilder.Append("{\n");

            for (var i = 0; i < scenesManagerConstants.sceneMapKeys.Count; i++)
            {
                var coma = i < scenesManagerConstants.sceneMapKeys.Count - 1 ? "," : string.Empty;
                _stringBuilder.Append($"\t{scenesManagerConstants.sceneMapKeys[i].ToUpper()} = {i}{coma}\n");
            }

            _stringBuilder.Append("}");

            var filename = $"{_enumClassName}.cs";
            File.WriteAllText(Path.Combine(scenesManagerConstants.enumsPath, filename), _stringBuilder.ToString());
            AssetDatabase.Refresh();
        }
    }
}
