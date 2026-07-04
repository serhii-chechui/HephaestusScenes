# Changelog

## [1.2.2] - 2026-07-04

### Changed

- Scene key lookup now uses a dictionary built once on `Initialize` instead of a linear search per call;
- Missing or unassigned scene keys now throw a descriptive exception naming the key and the config instead of a `NullReferenceException`;
- Initialization/dispose logs are emitted only in the Editor and Development builds;
- `ScenesManagerConstants` inspector: the enum export path is stored relative to the project, folders outside the project are rejected;
- README now documents installation, scoped registries and the optional UniTask integration;

### Fixed

- Adding, editing and removing scene keys marks the asset dirty immediately — changes are no longer lost without pressing `Save Config`;
- Removing a scene key no longer mutates the list in the middle of the inspector layout pass;
- `ScenesManagerConfig` inspector no longer throws when no `ScenesManagerConstants` asset is assigned — keys fall back to plain integer fields with a warning;
- Undo support for all config and constants edits;
- Removed the unused `keys` conversion dead code from `ScenesManagerConfigEditor`;
- Replaced the leftover `localdata` keyword in `package.json` with `scenes`;

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
