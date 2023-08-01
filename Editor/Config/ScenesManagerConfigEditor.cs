using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Hephaestus.Scenes.Editor.Config {
    [CustomEditor(typeof(ScenesManagerConfig))]
    public class ScenesManagerConfigEditor : UnityEditor.Editor {
        
        private ReorderableList _list;

        private ScenesManagerConfig ScenesManagerConfig => target as ScenesManagerConfig;
        
        private string[] _keys;

        private void OnEnable() {
            _list = new ReorderableList(ScenesManagerConfig.scenesDataList, typeof(ScenesManagerConfigData), true, true, true, true);

            _list.onAddCallback += AddElementCallback;
            _list.onRemoveCallback += RemoveElementCallback;

            _list.drawHeaderCallback += DrawHeader;
            _list.drawElementCallback += DrawElement;
            _list.onChangedCallback += OnChangedCallback;
        }


        private void OnDisable() {
            _list.drawElementCallback -= DrawElement;
        }

        private void OnChangedCallback(ReorderableList list) {
            EditorUtility.SetDirty(target);
        }

        private void DrawHeader(Rect rect) {
            EditorGUI.LabelField(rect, "Scenes List: [Scene Key | Scene Name | Load Async]", EditorStyles.boldLabel);
        }

        private void AddElementCallback(ReorderableList reorderableList) {
            ScenesManagerConfig.scenesDataList.Add(new ScenesManagerConfigData());
            EditorUtility.SetDirty(target);
        }

        private void RemoveElementCallback(ReorderableList reorderableList) {
            ScenesManagerConfig.scenesDataList.RemoveAt(reorderableList.index);
            EditorUtility.SetDirty(target);
        }

        private void DrawElement(Rect rect, int index, bool active, bool focused) {
            var item = ScenesManagerConfig.scenesDataList[index];

            EditorGUI.BeginChangeCheck();

            var xPos = rect.x;
            var width = rect.width;

            item.sceneKey = EditorGUI.Popup(new Rect(xPos, rect.y, width * 0.5f - 8f, EditorGUIUtility.singleLineHeight), item.sceneKey, _keys);
            
            item.sceneAsset = (SceneAsset)EditorGUI.ObjectField(new Rect(xPos + width * 0.5f, rect.y, width * 0.5f, rect.height), item.sceneAsset, typeof(SceneAsset), false);
            item.sceneName = item.sceneAsset != null ? item.sceneAsset.name : "";

            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(target);
            }
        }

        public override void OnInspectorGUI() {
            
            base.OnInspectorGUI();
            
            if (ScenesManagerConfig.scenesManagerConfigConstants != null)
            {
                _keys = ScenesManagerConfig.scenesManagerConfigConstants.sceneMapKeys.ToArray();
                ConvertIntValuesFromKeys(_keys);
            }

            // Actually draw the list in the inspector
            if (_list != null)
            {
                _list.DoLayoutList();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Save Config", GUILayout.ExpandWidth(true), GUILayout.Height(32))) {
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
            }
        }
        
        private void ConvertIntValuesFromKeys(string[] input)
        {
            var options = new int[input.Length];

            for (int i = 0; i < options.Length; i++)
            {
                options[i] = i;
            }
        }
    }
}
