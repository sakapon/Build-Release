$references = @("System.IO.Compression", "System.IO.Compression.FileSystem")
$source = @"
using System;
using System.IO;
using System.IO.Compression;

public static class ZipHelper
{
    public static void CreateZipFile(string outputDirPath, string outputZipFileName, string[] inputFilePaths)
    {
        var outputZipFilePath = Path.Combine(outputDirPath, outputZipFileName);
        Directory.CreateDirectory(outputDirPath);
        File.Delete(outputZipFilePath);

        using (var zip = ZipFile.Open(outputZipFilePath, ZipArchiveMode.Create))
        {
            foreach (var filePath in inputFilePaths)
            {
                var entry = zip.CreateEntry(Path.GetFileName(filePath));

                using (var input = File.OpenRead(filePath))
                using (var output = entry.Open())
                {
                    input.CopyTo(output);
                }
            }
        }
    }
}
"@
Add-Type -TypeDefinition $source -Language CSharp -ReferencedAssemblies $references

$filePaths = @(
    "Version1up\KTools.Version1up.ps1",
    "ZipRelease\KTools.ZipRelease.ps1",
    "Version1up.NetCore\KTools.Version1up.NetCore.ps1",
    "ZipRelease.NetCore\KTools.ZipRelease.NetCore.ps1",
    "NuGetPackup.NetCore\KTools.NuGetPackup.NetCore.ps1")
[ZipHelper]::CreateZipFile("..\Downloads", "BuildRelease-.zip", $filePaths)
