using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace ZipReleaseConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ZipHelper.CreateZipFileForAssembly(@"..\..\");
            ZipHelper.CreateZipFileForAssembly(@"..\..\..\UnitTest");
        }

        internal static string GetAssemblyFileVersion(string assemblyFilePath)
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
    }
}
