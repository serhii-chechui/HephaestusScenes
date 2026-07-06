# Hephaestus Scenes

Part of the Hephaestus framework. A thin, Zenject-friendly wrapper around Unity `SceneManager` that lets you load, unload, and switch scenes by strongly-named keys instead of raw strings.

## Features

- Scene keys defined once in a `ScenesManagerConstants` asset and exported to a C# enum.
- Scene key → scene asset mapping in a `ScenesManagerConfig` asset with a custom inspector.
- `IScenesManager` service bound via Zenject: sync, `AsyncOperation`, and optional UniTask loading APIs.
- Optional UniTask integration — enabled automatically when the UniTask package is present.

## Requirements

- Unity 2019.2+
- [Extenject (Zenject)](https://github.com/Mathijs-Bakker/Extenject) — hard dependency, resolved from OpenUPM.
- [UniTask](https://github.com/Cysharp/UniTask) `>= 2.3.0` — **optional**. When `com.cysharp.unitask` is installed in the consuming project, the `USE_UNITASK` define activates and `IScenesManager` gains `LoadSceneUniTask` / `UnloadSceneUniTask` methods. Without UniTask the package compiles and works, just without these methods.

## Installation

Add the scoped registries to your project `Packages/manifest.json`:

```json
"scopedRegistries": [
  {
    "name": "package.openupm.com",
    "url": "https://package.openupm.com",
    "scopes": ["com.svermeulen.extenject", "com.cysharp"]
  },
  {
    "name": "WTFGames",
    "url": "http://18.195.169.73:4873/",
    "scopes": ["com.wtfgames"]
  }
]
```

Then add the dependency:

```json
"com.wtfgames.hephaestus.scenes": "1.2.3"
```

## Usage

1. Create a `ScenesManagerConstants` asset (`Create → HephaestusMobile → Core → Scenes → ScenesManagerConstants`), add your scene keys, and export them to an enum (`Export to enum` button).
2. Create a `ScenesManagerConfig` asset, assign the constants asset, and map each key to a scene asset.
3. Create a `HephaestusScenesManagerSOInstaller` asset, assign the config, and add it to your `ProjectContext`/`SceneContext` scriptable object installers.
4. Add `HephaestusScenesManagerInstaller.Install(Container)` to one of your installers.

```csharp
public class GameFlow
{
    [Inject] private IScenesManager _scenesManager;

    public void StartGame()
    {
        _scenesManager.LoadSceneAsync(ScenesManagerConfigConstants.GAME, LoadSceneMode.Single);
    }
}
```

Scenes referenced in the config must also be added to **Build Settings**, otherwise Unity fails to load them at runtime.

## Known limitations

- The public API is coupled to the built-in `SceneManager` (`AsyncOperation` return types); Addressables-based loading is not supported.
- Enum values are generated from the key list order — do not remove or reorder keys after configs and code depend on them.
