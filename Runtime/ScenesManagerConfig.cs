using System.Collections.Generic;
using HephaestusMobile.ScenesSystem.Data;
using UnityEngine;

namespace HephaestusMobile.ScenesSystem.Config {
    [CreateAssetMenu(fileName = "ScenesManagerConfig", menuName = "HephaestusMobile/Core/Scenes/ScenesManagerConfig", order = 0)]
    public class ScenesManagerConfig : ScriptableObject {
        [HideInInspector] public List<ScenesManagerConfigData> scenesDataList = new List<ScenesManagerConfigData>();
    }
}