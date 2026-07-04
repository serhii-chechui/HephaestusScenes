using System;
using System.Collections.Generic;
using System.Threading;
#if USE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace WTFGames.Hephaestus.ScenesSystem
{
    public class ScenesManager : IInitializable, IDisposable, IScenesManager
    {
        [Inject]
        readonly SignalBus _signalBus;

        [Inject]
        private ScenesManagerConfig _scenesManagerConfig;

        private Dictionary<int, string> _sceneNamesByKey;

        public AsyncOperation CurrentLoadingOperation { get; private set; }

        public void Initialize()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log("Hephaestus Scenes Manager Initialization.");
#endif
            BuildSceneNamesLookup();
        }

        public void Dispose()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log("Hephaestus Scenes Manager Dispose.");
#endif
        }

        /// <inheritdoc cref="IScenesManager"/>
        public int GetCurrentSceneIndex()
        {
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

        private void BuildSceneNamesLookup()
        {
            _sceneNamesByKey = new Dictionary<int, string>(_scenesManagerConfig.scenesDataList.Count);

            foreach (var sceneData in _scenesManagerConfig.scenesDataList)
            {
                if (_sceneNamesByKey.ContainsKey(sceneData.sceneKey))
                {
                    Debug.LogWarning($"Scene key '{sceneData.sceneKey}' is defined more than once in the '{_scenesManagerConfig.name}' config. The entry with scene name '{sceneData.sceneName}' overrides the previous one.");
                }

                _sceneNamesByKey[sceneData.sceneKey] = sceneData.sceneName;
            }
        }

        private string GetSceneNameFromKey(Enum sceneKey)
        {
            if (_sceneNamesByKey == null)
            {
                BuildSceneNamesLookup();
            }

            var key = Convert.ToInt32(sceneKey);

            if (!_sceneNamesByKey.TryGetValue(key, out var sceneName))
            {
                throw new KeyNotFoundException($"Scene key '{sceneKey}' ({key}) is not present in the '{_scenesManagerConfig.name}' config. Add the scene to the config scenes list.");
            }

            if (string.IsNullOrEmpty(sceneName))
            {
                throw new InvalidOperationException($"Scene key '{sceneKey}' ({key}) has no scene assigned in the '{_scenesManagerConfig.name}' config. Assign a scene asset to this key.");
            }

            return sceneName;
        }
    }
}
