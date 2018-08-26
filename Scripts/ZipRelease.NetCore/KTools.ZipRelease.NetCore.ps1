$references = @("System.IO.Compression.FileSystem", "System.Xml")
$source = @"
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

public static class ZipHelper
{
    public static string[] GetTargetFrameworks(string projDirPath = ".")
    {
        var projFilePath = GetProjFilePath(projDirPath);
        return GetTargetFrameworksForFile(projFilePath);
    }

    // (?<!) Zero-width negative lookbehind assertion.
    // (?<=) Zero-width positive lookbehind assertion.
    // (?!)  Zero-width negative lookahead assertion.
    // (?=)  Zero-width positive lookahead assertion.
    static string[] GetTargetFrameworksForFile(string projFilePath)
    {
        var contents = File.ReadAllText(projFilePath, Encoding.UTF8);
        var match = Regex.Match(contents, @"(?<=<TargetFrameworks?>).+?(?=</TargetFrameworks?>)", RegexOptions.Multiline);
        return match.Value.Split(';');
    }

    public static void CreateZipFileForAssembly(string projDirPath = ".", string outputDirPath = "zip")
    {
        var projFilePath = GetProjFilePath(projDirPath);
        var outputPath = @"bin\publish";
        var binDirPath = Path.Combine(projDirPath, outputPath);

        var projXml = new XmlDocument();
        projXml.Load(projFilePath);

        var assemblyName = GetNodeValue(projXml, "./PropertyGroup/AssemblyName", Path.GetFileNameWithoutExtension(projFilePath));
        var version = GetNodeValue(projXml, "./PropertyGroup/Version", "1.0.0");
        var outputZipFileName = string.Format("{0}-{1}.zip", assemblyName, version);

        Console.WriteLine("Zipping: {0} >> {1}", binDirPath, Path.Combine(outputDirPath, outputZipFileName));
        CreateZipFile(binDirPath, outputDirPath, outputZipFileName);
    }

    static string GetProjFilePath(string dirPath)
    {
        return Directory.EnumerateFiles(dirPath, "*.csproj", SearchOption.AllDirectories)
            .Concat(Directory.EnumerateFiles(dirPath, "*.vbproj", SearchOption.AllDirectories))
            .Concat(Directory.EnumerateFiles(dirPath, "*.fsproj", SearchOption.AllDirectories))
            .Single();
    }

    static string GetNodeValue(XmlDocument xml, string xpath, string defaultValue)
    {
        var node = xml.DocumentElement.SelectSingleNode(xpath);
        return node != null ? node.InnerText : defaultValue;
    }

    public static void CreateZipFile(string inputDirPath, string outputDirPath, string outputZipFileName)
    {
        var outputZipFilePath = Path.Combine(outputDirPath, outputZipFileName);
        Directory.CreateDirectory(outputDirPath);
        File.Delete(outputZipFilePath);
        ZipFile.CreateFromDirectory(inputDirPath, outputZipFilePath);
    }
}
"@
Add-Type -TypeDefinition $source -Language CSharp -ReferencedAssemblies $references


$scriptDir = Split-Path $MyInvocation.MyCommand.Path -Parent
$version1upPath = Join-Path $scriptDir KTools.Version1up.NetCore.ps1 -Resolve
echo $version1upPath
. $version1upPath

dotnet clean -c Release
[ZipHelper]::GetTargetFrameworks() | % {
    $_
    dotnet publish -c Release -f $_ -o bin\publish\$_
}
if ($LASTEXITCODE -ne 0) { exit 101 }

[ZipHelper]::CreateZipFileForAssembly()
