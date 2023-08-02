# Changelog

## [1.2.0] - 2023-08-02

### Changed
 
- Implemented ability to load and unload the scenes using ```UniTask```;
- Changed the signatures for methods using for loading scenes by adding `LoadingMode` parameter;  

## [1.1.1] - 2023-08-01

### Changed

- Changed the dependencies and ```publishConfig```;

## [1.1.0] - 2023-08-01

### Added

- New ```ScenesManagerConfigConstants``` to keep the scenes keys as enums;
- Editor for ```ScenesManagerConfigConstants```;

### Changed

- Left only ```LoadScene(Enum sceneKey)``` and ```AsyncOperation LoadSceneAsync(Enum sceneKey)```;

### Removed

- Flag ```async``` from the ```ScenesManagerConfigData```;

## [0.0.2] - 2021-04-24
- Test verdaccio Slack notification.

## [0.0.1] - 2021-02-18
- Created HephaestusCore Scenes as UnityPackage.
