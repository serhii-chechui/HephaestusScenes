using System;
using UnityEngine;

namespace Hephaestus.Scenes {
    public interface IScenesManager {
        AsyncOperation CurrentLoadingOperation { get; }
        
        /// <summary>
        /// Load the unity scene in a synchronous way. 
        /// </summary>
        /// <param name="sceneKey">The related scene key.</param>
        void LoadScene(Enum sceneKey);
        
        /// <summary>
        /// Load the unity scene in an asynchronous way.
        /// </summary>
        /// <param name="sceneKey">The related scene key.</param>
        /// <returns>Returns an AsyncOperation.</returns>
        AsyncOperation LoadSceneAsync(Enum sceneKey);
        
        /// <summary>
        /// Returns the current loaded scene index.
        /// </summary>
        /// <returns>Scene Index.</returns>
        int GetCurrentSceneIndex();
    }
}
