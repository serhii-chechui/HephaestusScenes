using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace WTFGames.Hephaestus.ScenesSystem.Editor
{
    [CustomEditor(typeof(ScenesManagerConfig))]
    public class ScenesManagerConfigEditor : UnityEditor.Editor
    {
        private ReorderableList _list;

        private ScenesManagerConfig ScenesManagerConfig => target as ScenesManagerConfig;

        private string[] _keys;

        private void OnEnable()
        {
            _list = new ReorderableList(ScenesManagerConfig.scenesDataList, typeof(ScenesManagerConfigData), true, true,
                true, true);

            _list.onAddCallback += AddElementCallback;
            _list.onRemoveCallback += RemoveElementCallback;

            _list.drawHeaderCallback += DrawHeader;
            _list.drawElementCallback += DrawElement;
            _list.onChangedCallback += OnChangedCallback;
        }


        private void OnDisable()
        {
            _list.drawElementCallback -= DrawElement;
        }

        private void OnChangedCallback(ReorderableList list)
        {
            EditorUtility.SetDirty(target);
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Scenes List: [Scene Key | Scene Name]", EditorStyles.boldLabel);
        }

        private void AddElementCallback(ReorderableList reorderableList)
        {
            Undo.RecordObject(target, "Add Scene Entry");
            ScenesManagerConfig.scenesDataList.Add(new ScenesManagerConfigData());
            EditorUtility.SetDirty(target);
        }

        private void RemoveElementCallback(ReorderableList reorderableList)
        {
            Undo.RecordObject(target, "Remove Scene Entry");
            ScenesManagerConfig.scenesDataList.RemoveAt(reorderableList.index);
            EditorUtility.SetDirty(target);
        }

        private void DrawElement(Rect rect, int index, bool active, bool focused)
        {
            var item = ScenesManagerConfig.scenesDataList[index];

            EditorGUI.BeginChangeCheck();

            var xPos = rect.x;
            var width = rect.width;

            var keyRect = new Rect(xPos, rect.y, width * 0.5f - 8f, EditorGUIUtility.singleLineHeight);

            var newSceneKey = _keys != null && _keys.Length > 0
                ? EditorGUI.Popup(keyRect, item.sceneKey, _keys)
                : EditorGUI.IntField(keyRect, item.sceneKey);

            var newSceneAsset = (SceneAsset)EditorGUI.ObjectField(
                new Rect(xPos + width * 0.5f, rect.y, width * 0.5f, rect.height), item.sceneAsset, typeof(SceneAsset),
                false);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Edit Scene Entry");
                item.sceneKey = newSceneKey;
                item.sceneAsset = newSceneAsset;
                item.sceneName = item.sceneAsset != null ? item.sceneAsset.name : "";
                EditorUtility.SetDirty(target);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (ScenesManagerConfig.scenesManagerConstants != null)
            {
                _keys = ScenesManagerConfig.scenesManagerConstants.sceneMapKeys.ToArray();
            }
            else
            {
                _keys = null;
                EditorGUILayout.HelpBox(
                    "Assign a ScenesManagerConstants asset to edit scene keys by name. Without it the keys are edited as raw integers.",
                    MessageType.Warning);
            }

            // Actually draw the list in the inspector
            if (_list != null)
            {
                _list.DoLayoutList();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Save Config", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
