using System;

namespace WTFGames.Hephaestus.ScenesSystem
{
    public interface ISceneChangeSignal
    {
        Enum SceneKey { get; set; }
    }
}