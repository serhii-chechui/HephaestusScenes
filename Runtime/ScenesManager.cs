﻿using HephaestusMobile.ScenesSystem.Config;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HephaestusMobile.ScenesSystem {
    public class ScenesManager : MonoBehaviour, IScenesManager {

        private ScenesManagerConfig _scenesManagerConfig;

        public AsyncOperation CurrentLoadingOperation { get; private set; }

        public void Initialize(ScenesManagerConfig scenesManagerConfig) {
            _scenesManagerConfig = scenesManagerConfig;
        }

        public void LoadScene(int sceneIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single) {
            SceneManager.LoadScene(sceneIndex, loadSceneMode);
        }

        public void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single) {
            SceneManager.LoadScene(sceneName, loadSceneMode);
        }

        public AsyncOperation LoadSceneAsync(int sceneIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single) {
            CurrentLoadingOperation = SceneManager.LoadSceneAsync(sceneIndex, loadSceneMode);
            return CurrentLoadingOperation;
        }

        public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single) {
            CurrentLoadingOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            return CurrentLoadingOperation;
        }

        public void LoadSceneByKeyFromConfig(string sceneKey) {
            var sceneConfigData = _scenesManagerConfig.scenesDataList.Find(s => s.sceneKey == sceneKey);
            var sceneName = sceneConfigData.sceneName;
            SceneManager.LoadScene(sceneName);
        }

        public AsyncOperation LoadSceneByKeyFromConfigAsync(string sceneKey) {
            var sceneConfigData = _scenesManagerConfig.scenesDataList.Find(s => s.sceneKey == sceneKey);
            var sceneName = sceneConfigData.sceneName;
            CurrentLoadingOperation = SceneManager.LoadSceneAsync(sceneName);
            return CurrentLoadingOperation;
        }

        public int GetCurrentSceneIndex() {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
}