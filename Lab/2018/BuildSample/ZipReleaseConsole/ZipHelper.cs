using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

public static class ZipHelper
{
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
