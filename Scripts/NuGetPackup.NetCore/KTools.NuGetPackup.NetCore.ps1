"NuGet Packup for .NET Core"

$scriptDir = Split-Path $MyInvocation.MyCommand.Path -Parent
$version1upPath = Join-Path $scriptDir KTools.Version1up.NetCore.ps1 -Resolve
if (-not $version1upPath) { exit 100 }
$version1upPath
. $version1upPath

dotnet clean -c Release
if ($LASTEXITCODE -ne 0) { exit 101 }
dotnet build -c Release --no-incremental
if ($LASTEXITCODE -ne 0) { exit 102 }
dotnet pack -c Release -o pkg
if ($LASTEXITCODE -ne 0) { exit 103 }

"NuGet Packup: Completed"
