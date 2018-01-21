using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

public static class ZipHelper
{
    public static void CreateZipForAssembly(string sourceAssemblyFilePath, string targetDirPath)
    {
        var assemblyName = Path.GetFileNameWithoutExtension(sourceAssemblyFilePath);
        var assembly = Assembly.LoadFrom(sourceAssemblyFilePath);
        var assemblyFileVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
        if (assemblyFileVersion == null) return;

        var sourceDirPath = Path.GetDirectoryName(sourceAssemblyFilePath);
        var targetZipFileName = string.Format("{0}-{1}.zip", assemblyName, assemblyFileVersion.Version);

        CreateZipFile(sourceDirPath, targetDirPath, targetZipFileName);
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
