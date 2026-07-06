using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace WTFGames.Hephaestus.ScenesSystem.Tests
{
    public class HephaestusScenesManagerInstallerTests
    {
        private DiContainer _container;
        private ScenesManagerConfig _config;

        [SetUp]
        public void SetUp()
        {
            _config = ScriptableObject.CreateInstance<ScenesManagerConfig>();

            _container = new DiContainer();
            SignalBusInstaller.Install(_container);
            _container.BindInstance(_config);

            HephaestusScenesManagerInstaller.Install(_container);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_config);
        }

        [Test]
        public void InstallBindings_BindsIScenesManager()
        {
            var scenesManager = _container.Resolve<IScenesManager>();

            Assert.IsInstanceOf<ScenesManager>(scenesManager);
        }

        [Test]
        public void InstallBindings_BindsScenesManagerAsSingle()
        {
            var first = _container.Resolve<IScenesManager>();
            var second = _container.Resolve<IScenesManager>();

            Assert.AreSame(first, second);
        }

        [Test]
        public void InstallBindings_BindsLifecycleInterfacesToSameInstance()
        {
            var scenesManager = _container.Resolve<IScenesManager>();
            var initializables = _container.ResolveAll<IInitializable>();

            CollectionAssert.Contains(initializables, scenesManager);
        }

        [Test]
        public void InstallBindings_DeclaresSceneChangeSignal()
        {
            var signalBus = _container.Resolve<SignalBus>();

            Assert.DoesNotThrow(() => signalBus.Fire<ISceneChangeSignal>(new SceneChangeSignal()));
        }
    }
}
