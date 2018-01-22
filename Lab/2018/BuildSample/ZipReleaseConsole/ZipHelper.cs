using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

public static class ZipHelper
{
    public static void CreateZipFileForAssembly(string projDirPath = ".", string outputDirPath = "zip")
    {
        var assemblyName = "*";
        var assemblyFilePath = "*.exe";
        var binDirPath = @"bin\Release";

        var version = GetAssemblyFileVersion(assemblyFilePath);
        var outputZipFileName = string.Format("{0}-{1}.zip", assemblyName, version);

        CreateZipFile(binDirPath, outputDirPath, outputZipFileName);
    }

    static string GetAssemblyFileVersion(string assemblyFilePath)
    {
        var assembly = Assembly.LoadFrom(assemblyFilePath);
        var assemblyFileVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
        return assemblyFileVersion != null ? assemblyFileVersion.Version : null;
    }

    public static void CreateZipFile(string inputDirPath, string outputZipFilePath)
    {
        var outputDirPath = Path.GetDirectoryName(outputZipFilePath);
        Directory.CreateDirectory(outputDirPath);
        File.Delete(outputZipFilePath);
        ZipFile.CreateFromDirectory(inputDirPath, outputZipFilePath);
    }

    public static void CreateZipFile(string inputDirPath, string outputDirPath, string outputZipFileName)
    {
        var outputZipFilePath = Path.Combine(outputDirPath, outputZipFileName);
        Directory.CreateDirectory(outputDirPath);
        File.Delete(outputZipFilePath);
        ZipFile.CreateFromDirectory(inputDirPath, outputZipFilePath);
    }
}
