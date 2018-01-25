# Sets the alias of MSBuild.exe.
# sal msbuild "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
sal msbuild "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"

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
    public static string GetMSBuildPath()
    {
        var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        var msvs = Path.Combine(programFiles, "Microsoft Visual Studio");
        var msb = Path.Combine(programFiles, "MSBuild");
        var msbuilds = GetMSBuildPaths(msvs).Concat(GetMSBuildPaths(msb)).ToArray();

        var msbuildVersionPattern = new Regex(@"(?<=MSBuild\\).+?(?=\\)");
        var msbuild = msbuilds
            .Where(p => !p.Contains("amd64"))
            .OrderByDescending(p => double.Parse(msbuildVersionPattern.Match(p).Value))
            .FirstOrDefault();

        return msbuild ?? GetMSBuildPathFromNetFW();
    }

    internal static string GetMSBuildPathFromNetFW()
    {
        var windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        var netfw = Path.Combine(windows, @"Microsoft.NET\Framework");
        var msbuilds = GetMSBuildPaths(netfw).ToArray();

        var netfwVersionPattern = new Regex(@"(?<=v)\d+(?=\.)");
        return msbuilds
            .OrderByDescending(p => int.Parse(netfwVersionPattern.Match(p).Value))
            .FirstOrDefault();
    }

    static IEnumerable<string> GetMSBuildPaths(string dirPath)
    {
        return Directory.Exists(dirPath) ? Directory.EnumerateFiles(dirPath, "MSBuild.exe", SearchOption.AllDirectories) : Enumerable.Empty<string>();
    }

    public static void CreateZipFileForAssembly(string projDirPath = ".", string outputDirPath = "zip")
    {
        var projFilePath = GetProjFilePath(projDirPath);
        var assemblyInfoFilePath = GetAssemblyInfoFilePath(projDirPath);

        var projXml = new XmlDocument();
        projXml.Load(projFilePath);
        var nsm = new XmlNamespaceManager(projXml.NameTable);
        nsm.AddNamespace("p", "http://schemas.microsoft.com/developer/msbuild/2003");

        var assemblyName = projXml.DocumentElement.SelectSingleNode("./p:PropertyGroup/p:AssemblyName", nsm).InnerText;
        var outputPath = projXml.DocumentElement.SelectNodes("./p:PropertyGroup/p:OutputPath", nsm)
            .OfType<XmlElement>()
            .Single(xe => xe.ParentNode.Attributes["Condition"].Value.Contains("Release"))
            .InnerText;

        var binDirPath = Path.Combine(projDirPath, outputPath);
        var version = GetAssemblyFileVersion(assemblyInfoFilePath);
        var outputZipFileName = string.Format("{0}-{1}.zip", assemblyName, version);

        Console.WriteLine("Zipping: {0} >> {1}", binDirPath, Path.Combine(outputDirPath, outputZipFileName));
        CreateZipFile(binDirPath, outputDirPath, outputZipFileName);
    }

    static string GetProjFilePath(string dirPath)
    {
        return Directory.EnumerateFiles(dirPath, "*.csproj", SearchOption.AllDirectories).Single();
    }

    static string GetAssemblyInfoFilePath(string dirPath)
    {
        return Directory.EnumerateFiles(dirPath, "AssemblyInfo.cs", SearchOption.AllDirectories).Single();
    }

    // (?<!) Zero-width negative lookbehind assertion.
    // (?<=) Zero-width positive lookbehind assertion.
    // (?!)  Zero-width negative lookahead assertion.
    // (?=)  Zero-width positive lookahead assertion.
    internal static string GetAssemblyFileVersion(string assemblyInfoFilePath)
    {
        var contents = File.ReadAllText(assemblyInfoFilePath, Encoding.UTF8);
        var match = Regex.Match(contents, @"(?<!^\s*//.*)(?<=AssemblyFileVersion\("").+?(?=""\))", RegexOptions.Multiline);
        return match.Value;
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


.\KTools.VersionIncrement.ps1

msbuild /p:Configuration=Release /t:Clean
msbuild /p:Configuration=Release /t:Rebuild
if ($LASTEXITCODE -ne 0) { exit 100 }

Add-Type -TypeDefinition $source -Language CSharp -ReferencedAssemblies $references
[ZipHelper]::CreateZipFileForAssembly()
