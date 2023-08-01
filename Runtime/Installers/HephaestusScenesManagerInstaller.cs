using Zenject;

namespace Hephaestus.Scenes
{
    public class HephaestusScenesManagerInstaller : Installer<HephaestusScenesManagerInstaller>
    {
        public override void InstallBindings()
        {
            // Declare signals
            Container.DeclareSignal<ISceneChangeSignal>();
            
            // Bind entities
            Container.BindInterfacesTo<ScenesManager>().AsSingle();
        }
    }
}