using Zenject;

namespace WTFGames.Hephaestus.ScenesSystem
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