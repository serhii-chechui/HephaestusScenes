using System;

namespace Hephaestus.Scenes
{
    public interface ISceneChangeSignal
    {
        Enum SceneKey { get; set; }
    }
}