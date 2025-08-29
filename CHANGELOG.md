# Changelog

Notable changes.

## [Unreleased]

## [1.0.0] - 2024-04-07
- Deployed the GODependencyResolver as its own package.

## [1.0.1]
Fixed a bug when calling `GetService<TService>(string sceneName)`. where it would not return the correct response, unless the object was missing.

## [1.0.2] - 8/29/2025
- Updated gitignore file to include a few things we don't need
- Added gitattributes file
- Deleted loose files that shouldn't be in there to console warnings in Unity