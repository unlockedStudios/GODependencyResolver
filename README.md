# GO Dependency Resolver (Package)

The GO Dependency Resolver is a simple Dependency Injector for referencing and using separate class objects throughout the game. First initialized in the loading screen and reused throughout the lifetime of the project.

## Prerequisites
Unity 2021 Although it may be usable in earlier versions.

## Versions
### [1.0.1]
Fixed a bug when calling `GetService<TService>(string sceneName)`. where it would not return the correct response, unless the object was missing.

## [1.0.2] - 8/29/2025
- Updated gitignore file to include a few things we don't need
- Added gitattributes file
- Deleted loose files that shouldn't be in there to console warnings in Unity