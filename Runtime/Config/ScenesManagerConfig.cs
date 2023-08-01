using System.Collections.Generic;
using UnityEngine;

namespace Hephaestus.Scenes {
    [CreateAssetMenu(fileName = "ScenesManagerConfig", menuName = "HephaestusMobile/Core/Scenes/ScenesManagerConfig", order = 0)]
    public class ScenesManagerConfig : ScriptableObject
    {
        public ScenesManagerConfigConstants scenesManagerConfigConstants;
        
        [HideInInspector]
        public List<ScenesManagerConfigData> scenesDataList = new List<ScenesManagerConfigData>();
    }
}