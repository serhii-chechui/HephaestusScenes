using System;
using System.Threading;
#if USE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hephaestus.Scenes {
    public interface IScenesManager {
        
        AsyncOperation CurrentLoadingOperation { get; }
        
        /// <summary>
        /// Returns the current loaded scene index.
        /// </summary>
        /// <returns>Scene Index.</returns>
        int GetCurrentSceneIndex();

        /// <summary>
        /// Load the unity scene in a synchronous way. 
        /// </summary>
        /// <param name="sceneKey">The related scene key.</param>
        /// <param name="loadSceneMode">Load scene mode.</param>
        void LoadScene(Enum sceneKey, LoadSceneMode loadSceneMode);

        /// <summary>
        /// Load the unity scene in an asynchronous way.
        /// </summary>
        /// <param name="sceneKey">The related scene key.</param>
        /// <param name="loadSceneMode">Load scene mode.</param>
        /// <returns>Returns an AsyncOperation.</returns>
        AsyncOperation LoadSceneAsync(Enum sceneKey, LoadSceneMode loadSceneMode);

        /// <summary>
        /// Unloads the scene by the scene related key.
        /// </summary>
        /// <param name="sceneKey">The related scene key.</param>
        /// <returns>Returns an AsyncOperation.</returns>
        AsyncOperation UnloadScene(Enum sceneKey);
        
        #if USE_UNITASK

        /// <summary>
        /// Load the scene using UniTask.
        /// </summary>
        /// <param name="sceneKey">he related scene key.</param>
        /// <param name="loadSceneMode">Load scene mode.</param>
        /// <param name="cancellationToken">Cancellation token used to cancel the task.</param>
        /// <returns>UniTask</returns>
        UniTask LoadSceneUniTask(Enum sceneKey, LoadSceneMode loadSceneMode, CancellationToken cancellationToken);
        
        UniTask UnloadSceneUniTask(Enum sceneKey, CancellationToken cancellationToken);
        
        #endif
    }
}
