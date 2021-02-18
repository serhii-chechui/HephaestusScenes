using HephaestusMobile.ScenesSystem.Config;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HephaestusMobile.ScenesSystem {
    public interface IScenesManager {
        AsyncOperation CurrentLoadingOperation { get; }
        void Initialize(ScenesManagerConfig scenesManagerConfig);
        void LoadScene(int sceneIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
        void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
        AsyncOperation LoadSceneAsync(int sceneIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
        AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
        void LoadSceneByKeyFromConfig(string sceneKey);
        AsyncOperation LoadSceneByKeyFromConfigAsync(string sceneKey);
        int GetCurrentSceneIndex();
    }
}
