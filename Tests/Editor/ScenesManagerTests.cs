using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace WTFGames.Hephaestus.ScenesSystem.Tests
{
    public class ScenesManagerTests
    {
        private enum TestScenes
        {
            MENU = 0,
            GAME = 1,
            EMPTY = 2,
            UNKNOWN = 42
        }

        private DiContainer _container;
        private ScenesManagerConfig _config;

        [SetUp]
        public void SetUp()
        {
            _config = ScriptableObject.CreateInstance<ScenesManagerConfig>();
            _config.scenesDataList = new List<ScenesManagerConfigData>
            {
                new ScenesManagerConfigData { sceneKey = 0, sceneName = "Menu" },
                new ScenesManagerConfigData { sceneKey = 1, sceneName = "Game" }
            };

            _container = new DiContainer();
            SignalBusInstaller.Install(_container);
            _container.BindInstance(_config);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_config);
        }

        private ScenesManager CreateManager()
        {
            return _container.Instantiate<ScenesManager>();
        }

        [Test]
        public void GetSceneNameFromKey_ReturnsConfiguredName()
        {
            var manager = CreateManager();
            manager.Initialize();

            Assert.AreEqual("Menu", manager.GetSceneNameFromKey(TestScenes.MENU));
            Assert.AreEqual("Game", manager.GetSceneNameFromKey(TestScenes.GAME));
        }

        [Test]
        public void GetSceneNameFromKey_WorksWithoutInitialize()
        {
            var manager = CreateManager();

            Assert.AreEqual("Menu", manager.GetSceneNameFromKey(TestScenes.MENU));
        }

        [Test]
        public void GetSceneNameFromKey_ThrowsKeyNotFound_ForMissingKey()
        {
            var manager = CreateManager();
            manager.Initialize();

            var exception = Assert.Throws<KeyNotFoundException>(
                () => manager.GetSceneNameFromKey(TestScenes.UNKNOWN));

            StringAssert.Contains("UNKNOWN", exception.Message);
        }

        [Test]
        public void GetSceneNameFromKey_ThrowsInvalidOperation_WhenSceneNotAssigned()
        {
            _config.scenesDataList.Add(new ScenesManagerConfigData { sceneKey = 2, sceneName = "" });
            var manager = CreateManager();
            manager.Initialize();

            var exception = Assert.Throws<System.InvalidOperationException>(
                () => manager.GetSceneNameFromKey(TestScenes.EMPTY));

            StringAssert.Contains("EMPTY", exception.Message);
        }

        [Test]
        public void Initialize_WarnsAboutDuplicateKeys_AndLastEntryWins()
        {
            _config.scenesDataList.Add(new ScenesManagerConfigData { sceneKey = 0, sceneName = "MenuOverride" });
            var manager = CreateManager();

            LogAssert.Expect(LogType.Warning, new Regex("more than once"));
            manager.Initialize();

            Assert.AreEqual("MenuOverride", manager.GetSceneNameFromKey(TestScenes.MENU));
        }
    }
}
