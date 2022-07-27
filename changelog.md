# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0-preview.1](https://github.com/unity-game-framework/ugf-module-locale/releases/tag/2.0.0-preview.1) - 2022-07-27  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-locale/milestone/5?closed=1)  
    

### Changed

- Change table usage ([#9](https://github.com/unity-game-framework/ugf-module-locale/issues/9))  
    - Update dependencies: `com.ugf.runtimetools` to `2.10.0` version.
    - Change `LocaleTable` class and related to use `Table` class from _UGF.RuntimeTools_ package.

## [2.0.0-preview](https://github.com/unity-game-framework/ugf-module-locale/releases/tag/2.0.0-preview) - 2022-07-14  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-locale/milestone/4?closed=1)  
    

### Changed

- Change string ids to global id ([#7](https://github.com/unity-game-framework/ugf-module-locale/issues/7))  
    - Update dependencies: `com.ugf.module.assets` to `5.0.0-preview` version.
    - Update package _Unity_ version to `2022.1`.
    - Change usage of ids as `GlobalId` structure instead of `string`.

## [1.0.0-preview.2](https://github.com/unity-game-framework/ugf-module-locale/releases/tag/1.0.0-preview.2) - 2022-07-10  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-locale/milestone/3?closed=1)  
    

### Fixed

- Fix locale dropdown min height ([#5](https://github.com/unity-game-framework/ugf-module-locale/issues/5))  
    - Fix `LocaleEntryDropdownAttribute` class property drawer using bigger dropdown height.

## [1.0.0-preview.1](https://github.com/unity-game-framework/ugf-module-locale/releases/tag/1.0.0-preview.1) - 2022-07-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-locale/milestone/2?closed=1)  
    

### Fixed

- Fix locale entry dropdown performance ([#3](https://github.com/unity-game-framework/ugf-module-locale/issues/3))  
    - Update dependencies: `com.ugf.runtimetools` to `2.9.1` version.
    - Add `LocaleEditorUtility.TryGetEntryNameFromCache()` method to get entry name from cache.
    - Fix `LocaleEntryDropdownAttribute` property drawer to use entry name from cache to display.

## [1.0.0-preview](https://github.com/unity-game-framework/ugf-module-locale/releases/tag/1.0.0-preview) - 2022-05-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-locale/milestone/1?closed=1)  
    

### Added

- Add implementation ([#1](https://github.com/unity-game-framework/ugf-module-locale/issues/1))  
    - Add `LocaleModule` and related classes.


