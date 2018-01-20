using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length < 1) return 100;

        IncrementVersion(args[0]);
        return 0;
    }

    public static void IncrementVersion(string projDirPath)
    {
        var assemblyInfoPath = Directory.EnumerateFiles(projDirPath, "AssemblyInfo.cs", SearchOption.AllDirectories).First();
        var contents = File.ReadLines(assemblyInfoPath, Encoding.UTF8)
            .Select(IncrementLine)
            .ToArray();
        File.WriteAllLines(assemblyInfoPath, contents, Encoding.UTF8);
    }

    static readonly Regex BuildNumberPattern = new Regex(@"(?<=Assembly(File)?Version\(""\d+\.\d+\.)\d+");

    internal static string IncrementLine(string line)
    {
        if (line.StartsWith("//")) return line;

        return BuildNumberPattern.Replace(line, m => IncrementNumber(m.Value));
    }

    static string IncrementNumber(string i)
    {
        return (int.Parse(i) + 1).ToString();
    }
}
