using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Hephaestus.Scenes {
    public class ScenesManager : IInitializable, IDisposable, IScenesManager {
    
        [Inject]
        readonly SignalBus _signalBus;

        [Inject]
        private ScenesManagerConfig _scenesManagerConfig;

        public AsyncOperation CurrentLoadingOperation { get; private set; }
        
        public void LoadScene(Enum sceneKey)
        {
            var sceneConfigData = _scenesManagerConfig.scenesDataList.Find(s => s.sceneKey == Convert.ToInt32(sceneKey));
            var sceneName = sceneConfigData.sceneName;
            SceneManager.LoadScene(sceneName);
        }

        public AsyncOperation LoadSceneAsync(Enum sceneKey)
        {
            var sceneConfigData = _scenesManagerConfig.scenesDataList.Find(s => s.sceneKey == Convert.ToInt32(sceneKey));
            var sceneName = sceneConfigData.sceneName;
            CurrentLoadingOperation = SceneManager.LoadSceneAsync(sceneName);
            return CurrentLoadingOperation;
        }

        public void Initialize()
        {
            Debug.Log("Hephaestus Scenes Manager Initialization.");
            // _signalBus.Subscribe<ISceneChangeSignal>(x => ScenesManager.LoadScene(x.SceneKey));
        }

        public void Dispose()
        {
            Debug.Log("Hephaestus Scenes Manager Dispose.");
            // _signalBus.TryUnsubscribe<ISceneChangeSignal>(x => ScenesManager.LoadSceneAsync(x.SceneKey));
        }

        public int GetCurrentSceneIndex() {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
}