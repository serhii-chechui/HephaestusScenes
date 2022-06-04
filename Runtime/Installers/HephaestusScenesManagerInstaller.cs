using HephaestusMobile.ScenesSystem;
using HephaestusMobile.ScenesSystem.Signals;
using Zenject;

namespace Installers
{
    public class HephaestusScenesManagerInstaller : Installer<HephaestusScenesManagerInstaller>
    {
        public override void InstallBindings()
        {
            // Declare signals
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ISceneChangeSignal>();
            
            // Bind entities
            Container.BindInterfacesTo<ScenesManager>().AsSingle();
        }
    }
}