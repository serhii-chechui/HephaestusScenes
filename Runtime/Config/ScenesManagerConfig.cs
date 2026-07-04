using System.Collections.Generic;
using UnityEngine;

namespace WTFGames.Hephaestus.ScenesSystem
{
    [CreateAssetMenu(fileName = "ScenesManagerConfig", menuName = "HephaestusMobile/Core/Scenes/ScenesManagerConfig", order = 0)]
    public class ScenesManagerConfig : ScriptableObject
    {
        public ScenesManagerConstants scenesManagerConstants;

        [HideInInspector]
        public List<ScenesManagerConfigData> scenesDataList = new List<ScenesManagerConfigData>();
    }
}