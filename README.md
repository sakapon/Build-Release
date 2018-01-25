# ZIP Release
A set of tools to build a Visual Studio project and create a ZIP file.  
You can add the PowerShell script files to your project by NuGet.

## ZIP Release
The PowerShell script to build the project and create a ZIP file.  
This tool increments the assembly version before the build, so this depends on the **Version Increment** (shown below).  
[NuGet Gallery | KTools.ZipRelease](https://www.nuget.org/packages/KTools.ZipRelease/)

### Specification
- Increment the version
  - build number (z of "x.y.z")
- Build a release by MSBuild
- Create a ZIP file from the build result

You can customize the PowerShell script to meet your needs.

## Version Increment
The PowerShell script to increment the assembly version on AssemblyInfo.  
[NuGet Gallery | KTools.VersionIncrement](https://www.nuget.org/packages/KTools.VersionIncrement/)

## References
- [.NET Regular Expressions](https://msdn.microsoft.com/library/hs600312.aspx)
- [.nuspec File Reference for NuGet](https://docs.microsoft.com/en-us/nuget/schema/nuspec)
- [NuGet Package Version Reference](https://docs.microsoft.com/en-us/nuget/reference/package-versioning)
- [.NET ビルド小技集 (4)](https://sakapon.wordpress.com/2015/10/23/dotnet-build-4/) (my blog)
