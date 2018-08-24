using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
