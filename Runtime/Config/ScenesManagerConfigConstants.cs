using System.Collections.Generic;
using UnityEngine;

namespace Hephaestus.Scenes
{
    [CreateAssetMenu(fileName = "ScenesManagerConfigConstants", menuName = "HephaestusMobile/Core/Scenes/ScenesManagerConfigConstants", order = 1)]
    public class ScenesManagerConfigConstants : ScriptableObject
    {
        [HideInInspector]
        public List<string> sceneMapKeys = new List<string>();
    }
}