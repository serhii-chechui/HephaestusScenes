using HephaestusMobile.ScenesSystem.Config;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "HephaestusScenesManagerSOInstaller", menuName = "HephaestusMobile/Core/Scenes/HephaestusScenesManagerSOInstaller")]
    public class HephaestusScenesManagerSOInstaller : ScriptableObjectInstaller<HephaestusScenesManagerSOInstaller>
    {
        [SerializeField]
        private ScenesManagerConfig _scenesManagerConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_scenesManagerConfig);
        }
    }
}