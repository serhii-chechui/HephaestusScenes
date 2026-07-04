using System.Collections.Generic;
using UnityEngine;

namespace WTFGames.Hephaestus.ScenesSystem
{
    [CreateAssetMenu(fileName = "ScenesManagerConstants", menuName = "HephaestusMobile/Core/Scenes/ScenesManagerConstants", order = 1)]
    public class ScenesManagerConstants : ScriptableObject
    {
        [HideInInspector]
        public string enumsPath;
        
        [HideInInspector]
        public List<string> sceneMapKeys = new List<string>();
    }
}