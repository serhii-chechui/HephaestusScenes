using System;
using UnityEditor;

namespace HephaestusMobile.ScenesSystem.Data {
    [Serializable]
    public class ScenesManagerConfigData {
        public string sceneKey;
        public string sceneName;
        #if UNITY_EDITOR
        public SceneAsset sceneAsset;
        #endif
        public bool loadAsync;
    }
}
