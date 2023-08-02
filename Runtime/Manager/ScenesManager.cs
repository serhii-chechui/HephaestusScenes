using System;
using System.Threading;
#if USE_UNITASK
using Cysharp.Threading.Tasks;
#endif
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
        
        /// <inheritdoc cref="IScenesManager"/>
        public int GetCurrentSceneIndex() {
            return SceneManager.GetActiveScene().buildIndex;
        }
        
        /// <inheritdoc cref="IScenesManager"/>
        public void LoadScene(Enum sceneKey, LoadSceneMode loadSceneMode)
        {
            var sceneName = GetSceneNameFromKey(sceneKey);
            SceneManager.LoadScene(sceneName, loadSceneMode);
        }

        /// <inheritdoc cref="IScenesManager"/>
        public AsyncOperation LoadSceneAsync(Enum sceneKey, LoadSceneMode loadSceneMode)
        {
            var sceneName = GetSceneNameFromKey(sceneKey);
            CurrentLoadingOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            return CurrentLoadingOperation;
        }

        /// <inheritdoc cref="IScenesManager"/>
        public AsyncOperation UnloadScene(Enum sceneKey)
        {
            var sceneName = GetSceneNameFromKey(sceneKey);
            return SceneManager.UnloadSceneAsync(sceneName);
        }

        #if USE_UNITASK
        
        /// <inheritdoc cref="IScenesManager"/>
        public UniTask LoadSceneUniTask(Enum sceneKey, LoadSceneMode loadSceneMode, CancellationToken cancellationToken)
        {
            var sceneName = GetSceneNameFromKey(sceneKey);
            var sceneLoadTask = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            return sceneLoadTask.ToUniTask(cancellationToken: cancellationToken);
        }

        /// <inheritdoc cref="IScenesManager"/>
        public UniTask UnloadSceneUniTask(Enum sceneKey, CancellationToken cancellationToken)
        {
            var sceneName = GetSceneNameFromKey(sceneKey);
            var sceneLoadTask = SceneManager.UnloadSceneAsync(sceneName);
            return sceneLoadTask.ToUniTask(cancellationToken: cancellationToken);
        }

        #endif

        private string GetSceneNameFromKey(Enum sceneKey)
        {
            return _scenesManagerConfig.scenesDataList.Find(s => s.sceneKey == Convert.ToInt32(sceneKey)).sceneName;
        }
    }
}