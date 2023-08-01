using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hephaestus.Scenes {
    [Serializable]
    public class ScenesManagerConfigData {
        public int sceneKey;
        public string sceneName;
        #if UNITY_EDITOR
        public SceneAsset sceneAsset;
        #endif
    }
}
