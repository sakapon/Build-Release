..\..\Tools\NuGet.CommandLine.4.4.1\NuGet.exe pack Package.nuspec

ni ..\..\Published -type directory -Force
move *.nupkg ..\..\Published -Force
explorer ..\..\Published
