$source = @"
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

        foreach (var filePath in GetProjFilePaths(dirPath))
            IncrementForFile(filePath);

        return 0;
    }

    static IEnumerable<string> GetProjFilePaths(string dirPath)
    {
        return Directory.EnumerateFiles(dirPath, "*.csproj", SearchOption.AllDirectories)
            .Concat(Directory.EnumerateFiles(dirPath, "*.vbproj", SearchOption.AllDirectories))
            .Concat(Directory.EnumerateFiles(dirPath, "*.fsproj", SearchOption.AllDirectories));
    }

    static readonly Encoding UTF8N = new UTF8Encoding();

    internal static void IncrementForFile(string filePath)
    {
        Console.WriteLine(filePath);
        var contents = File.ReadLines(filePath, UTF8N)
            .Select(IncrementForLine)
            .ToArray();
        File.WriteAllLines(filePath, contents, UTF8N);
    }

    // (?<!) Zero-width negative lookbehind assertion.
    // (?<=) Zero-width positive lookbehind assertion.
    static readonly Regex BuildNumberPattern = new Regex(@"(?<=<(Assembly)?(File)?Version>\d+\.\d+\.)\d+");

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
"@

Add-Type -TypeDefinition $source -Language CSharp
exit [Program]::Main($Args)
