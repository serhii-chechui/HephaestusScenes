using HephaestusMobile.ScenesSystem.Data;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace HephaestusMobile.ScenesSystem.Config.Editor {
    [CustomEditor(typeof(ScenesManagerConfig))]
    public class ScenesManagerConfigEditor : UnityEditor.Editor {
        
        private ReorderableList _list;

        private ScenesManagerConfig ScenesManagerConfig {
            get { return target as ScenesManagerConfig; }
        }

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
            item.sceneKey = EditorGUI.TextField(new Rect(rect.x, rect.y, rect.width * 0.4f - 8, rect.height), item.sceneKey);
            item.sceneAsset = (SceneAsset)EditorGUI.ObjectField(new Rect(rect.width * 0.4f + 32, rect.y, rect.width * 0.5f, rect.height), item.sceneAsset, typeof(SceneAsset), false);
            item.sceneName = item.sceneAsset != null ? item.sceneAsset.name : "";
            item.loadAsync = EditorGUI.Toggle(new Rect(rect.width * 0.95f + 32, rect.y, rect.width * 0.15f - 18, rect.height), item.loadAsync);

            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(target);
            }
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            // Actually draw the list in the inspector
            _list.DoLayoutList();

            EditorGUILayout.Space();

            if (GUILayout.Button("Save Config", GUILayout.ExpandWidth(true), GUILayout.Height(32))) {
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
