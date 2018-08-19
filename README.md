# Build Release
A set of tools to build a Visual Studio project.

[![license](https://img.shields.io/github/license/sakapon/Build-Release.svg)](https://github.com/sakapon/Build-Release/blob/master/LICENSE)

There are options to use it:
- Add the PowerShell script files to Visual Studio projects by NuGet
- Download and extract [the PowerShell script files](https://github.com/sakapon/Build-Release/raw/master/Downloads/BuildRelease-1.2.6.zip) to any folder
- Add the PowerShell scripts above to `External Tools` in Visual Studio (recommended)

See [Usage](#usage) for details.

## Version Increment
The PowerShell script to increment the assembly version on AssemblyInfo.

[![NuGet](https://img.shields.io/nuget/v/KTools.VersionIncrement.svg)](https://www.nuget.org/packages/KTools.VersionIncrement/)
[![NuGet](https://img.shields.io/nuget/dt/KTools.VersionIncrement.svg)](https://www.nuget.org/packages/KTools.VersionIncrement/)  
[NuGet Gallery | KTools.VersionIncrement](https://www.nuget.org/packages/KTools.VersionIncrement/)

### Specification
- Increment the assembly version on AssemblyInfo.cs
  - build number (z of "x.y.z")

## Zip Release
The PowerShell script to build the project and create a ZIP file.

[![NuGet](https://img.shields.io/nuget/v/KTools.ZipRelease.svg)](https://www.nuget.org/packages/KTools.ZipRelease/)
[![NuGet](https://img.shields.io/nuget/dt/KTools.ZipRelease.svg)](https://www.nuget.org/packages/KTools.ZipRelease/)  
[NuGet Gallery | KTools.ZipRelease](https://www.nuget.org/packages/KTools.ZipRelease/)

### Specification
- Increment the assembly version (Call the [**Version Increment**](#version-increment))
- Build a release by MSBuild
- Create a ZIP file from the build result

## Usage
T.B.D.

You can customize the PowerShell script to meet your needs.

## Release Notes
- **v1.2.6** Find the path to KTools.VersionIncrement.ps1, regardless of the current directory.
- **v1.2.5** Find the path to MSBuild.exe.
- **v1.2.4** Rename .ps1 file.
- **v1.1.3** The first release.

## References
- [.NET Regular Expressions](https://msdn.microsoft.com/library/hs600312.aspx)
- [.nuspec File Reference for NuGet](https://docs.microsoft.com/en-us/nuget/schema/nuspec)
- [NuGet Package Version Reference](https://docs.microsoft.com/en-us/nuget/reference/package-versioning)
- [.NET ビルド小技集 (4)](https://sakapon.wordpress.com/2015/10/23/dotnet-build-4/) (my blog)
- [ビルドして ZIP にする PowerShell スクリプト](https://sakapon.wordpress.com/2018/02/06/zip-release/) (my blog)
