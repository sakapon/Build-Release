using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class Program
{
    public static int Main(string[] args)
    {
        // args[0]: The target directory path (optional).
        var dirPath = args.Length > 0 ? args[0] : ".";

        foreach (var filePath in GetAssemblyInfoPaths(dirPath))
            IncrementForFile(filePath);

        return 0;
    }

    static IEnumerable<string> GetAssemblyInfoPaths(string dirPath)
    {
        return Directory.EnumerateFiles(dirPath, "AssemblyInfo.cs", SearchOption.AllDirectories);
    }

    internal static void IncrementForFile(string filePath)
    {
        Console.WriteLine(filePath);
        var encoding = DetectEncoding(filePath);
        var contents = File.ReadLines(filePath, encoding)
            .Select(IncrementForLine)
            .ToArray();
        File.WriteAllLines(filePath, contents, encoding);
    }

    internal static readonly Encoding UTF8N = new UTF8Encoding();

    internal static Encoding DetectEncoding(string filePath)
    {
        var preamble = Encoding.UTF8.GetPreamble();
        var headBytes = new byte[preamble.Length];

        using (var stream = File.OpenRead(filePath))
        {
            stream.Read(headBytes, 0, headBytes.Length);
        }
        return headBytes.SequenceEqual(preamble) ? Encoding.UTF8 : UTF8N;
    }

    // (?<!) Zero-width negative lookbehind assertion.
    // (?<=) Zero-width positive lookbehind assertion.
    static readonly Regex BuildNumberPattern = new Regex(@"(?<!^\s*//.*)(?<=Assembly(File)?(Informational)?Version\(""\d+\.\d+\.)\d+");

    internal static string IncrementForLine(string line)
    {
        var newLine = BuildNumberPattern.Replace(line, m => IncrementNumber(m.Value));
        if (newLine != line)
        {
            Console.WriteLine("<< {0}", line);
            Console.WriteLine(">> {0}", newLine);
        }
        return newLine;
    }

    static string IncrementNumber(string i)
    {
        return (int.Parse(i) + 1).ToString();
    }
}
