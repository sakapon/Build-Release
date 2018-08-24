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
    public static void CreateZipFileForAssembly(string projDirPath = ".", string outputDirPath = "zip")
    {
        var projFilePath = GetProjFilePath(projDirPath);
        var outputPath = @"bin\Release";
        var binDirPath = Path.Combine(projDirPath, outputPath);

        var projXml = new XmlDocument();
        projXml.Load(projFilePath);

        var assemblyName = projXml.DocumentElement.SelectSingleNode("./PropertyGroup/AssemblyName").InnerText;
        var version = projXml.DocumentElement.SelectSingleNode("./PropertyGroup/Version").InnerText;
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

    // (?<!) Zero-width negative lookbehind assertion.
    // (?<=) Zero-width positive lookbehind assertion.
    // (?!)  Zero-width negative lookahead assertion.
    // (?=)  Zero-width positive lookahead assertion.
    internal static string GetVersion(string projFilePath)
    {
        var contents = File.ReadAllText(projFilePath, Encoding.UTF8);
        var match = Regex.Match(contents, @"(?<=<Version>).+?(?=</Version>)", RegexOptions.Multiline);
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
