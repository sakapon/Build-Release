# Specification
The following tools are contained:
- [**Initial Set**](#initial-set)
- [**Version 1up**](#version-1up)
- [**Zip Release**](#zip-release)
- [**NuGet Packup**](#nuget-packup)

## Initial Set
The PowerShell script to add commonly used initial settings to the project.
- For the .NET Framework project format
- For the .NET Core project format

### Specification
- Add the setting for the assembly version
- Set `Debug Information` (`DebugType`) to none
- Add the setting for the XML Documentation file in case of DLL (.NET Core project format only)

## Version 1up
The PowerShell script to increment the assembly version.
- For the .NET Framework project format
- For the .NET Core project format

[![NuGet](https://img.shields.io/nuget/v/KTools.Version1up.svg)](https://www.nuget.org/packages/KTools.Version1up/)
[![NuGet](https://img.shields.io/nuget/dt/KTools.Version1up.svg)](https://www.nuget.org/packages/KTools.Version1up/)  
[NuGet Gallery | KTools.Version1up](https://www.nuget.org/packages/KTools.Version1up/) (for the .NET Framework project format)

### Specification
- Increment the build number of the assembly version (`z` of `x.y.z`)
  - AssemblyInfo.cs for .NET Framework
  - Project files for .NET Core

## Zip Release
The PowerShell script to build the project and create a ZIP file.
- For the .NET Framework project format
- For the .NET Core project format

[![NuGet](https://img.shields.io/nuget/v/KTools.ZipRelease.svg)](https://www.nuget.org/packages/KTools.ZipRelease/)
[![NuGet](https://img.shields.io/nuget/dt/KTools.ZipRelease.svg)](https://www.nuget.org/packages/KTools.ZipRelease/)  
[NuGet Gallery | KTools.ZipRelease](https://www.nuget.org/packages/KTools.ZipRelease/) (for the .NET Framework project format)

### Specification
- Increment the assembly version (call the [**Version 1up**](#version-1up))
- Build a release by the MSBuild
- Create a ZIP file from the build result

## NuGet Packup
The PowerShell script to build the project and create a NuGet package.
- For the .NET Core project format

### Specification
- Increment the assembly version (call the [**Version 1up**](#version-1up))
- Build a release by the MSBuild
- Create a NuGet package from the build result
