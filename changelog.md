# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.3.0](https://github.com/unity-game-framework/ugf-module-locale/releases/tag/2.3.0) - 2023-06-17  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-locale/milestone/10?closed=1)  
    

### Added

- Add csv import and export ([#14](https://github.com/unity-game-framework/ugf-module-locale/issues/14))  
    - Update dependencies: `com.ugf.runtimetools` to `2.19.0` and add `com.ugf.csv` of  `1.0.2` version.
    - Update package _Unity_ version to `2022.3`.
    - Add _Locale Table_ import and export from _Locale_ project settings.
    - Add `ILocaleTableEntry.TryGet()` method and overload used to get value by locale id.
    - Add `LocaleEditorUtility.CsvImport()` and `CsvExport()` methods to import or export locale table from csv files.

## [2.2.0](https://github.com/unity-game-framework/ugf-module-locale/releases/tag/2.2.0) - 2023-05-03  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-locale/milestone/9?closed=1)  
    

### Added

- Add locale table description collection asset ([#20](https://github.com/unity-game-framework/ugf-module-locale/issues/20))  
    - Add `LocaleTableDescriptionCollectionAsset` abstract class as collection of `LocaleTableDescription` assets.
    - Add `LocaleTableDescriptionCollectionListAsset` class as default implementation of `LocaleTableDescriptionCollectionAsset` used to store collection of `LocaleTableDescriptionAsset` assets.
    - Add `LocaleModuleAsset.Collections` property as collections of `LocaleTableDescriptionCollectionAsset` assets.
    - Change `LocaleModuleAssetEditor` class editor to support _Replacement_ feature for _Locales_ and _Tables_ collections.
- Add locale table description update button ([#18](https://github.com/unity-game-framework/ugf-module-locale/issues/18))  
    - Update dependencies: `com.ugf.module.assets` to `5.1.0` version.
    - Add `LocaleTableDescriptionAsset` class inspector button to update entries from table registered at _Locale_ project settings.

## [2.1.0](https://github.com/unity-game-framework/ugf-module-locale/releases/tag/2.1.0) - 2023-04-08  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-locale/milestone/8?closed=1)  
    

### Added

- Add set system locale on initialize ([#16](https://github.com/unity-game-framework/ugf-module-locale/issues/16))  
    - Update dependencies: `com.ugf.application` to `8.5.0` version.
    - Add `LocaleModuleDescription.SelectLocaleBySystemLanguageOnInitialize` property used to determine whether to setup locale on initialize based on system language.
    - Add `LocaleModule.TryGetLocaleBySystemLanguage()` method used to get locale id and description by system language.
    - Add `LocaleModule.TryGetLocaleByCultureInfo()` method used to get locale id and description by culture info.
    - Change `LocaleModuleDescription` class to use constructor instead of read-write properties.

## [2.0.0](https://github.com/unity-game-framework/ugf-module-locale/releases/tag/2.0.0) - 2023-01-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-locale/milestone/7?closed=1)  
    

### Changed

- Update project ([#13](https://github.com/unity-game-framework/ugf-module-locale/issues/13))  
    - Update dependencies: `com.ugf.module.assets` to `5.0.0` version.
    - Change `LocaleTableDrawer` class to display selection preview for locale.

## [2.0.0-preview.2](https://github.com/unity-game-framework/ugf-module-locale/releases/tag/2.0.0-preview.2) - 2022-12-16  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-locale/milestone/6?closed=1)  
    

### Added

- Add inspector selection preview support ([#11](https://github.com/unity-game-framework/ugf-module-locale/issues/11))  
    - Update dependencies: `com.ugf.module.assets` to `5.0.0-preview.2`, `com.ugf.editortools` to `2.14.0` and `"com.ugf.runtimetools` to `2.18.0` versions.
    - Update package _Unity_ version to `2022.2`.
    - Add missing selection preview for collections.

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


