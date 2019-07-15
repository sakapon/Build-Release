using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class InitialSet
{
    public static int Main(string[] args)
    {
        // args[0]: The target directory path (optional).
        var dirPath = args.Length > 0 ? args[0] : ".";

        foreach (var filePath in Directory.EnumerateFiles(dirPath, "*.csproj", SearchOption.AllDirectories))
            UpdateFile(filePath, DebugTypePattern, m => "none");

        foreach (var filePath in Directory.EnumerateFiles(dirPath, "AssemblyInfo.cs", SearchOption.AllDirectories))
            UpdateFile(filePath, RevisionPattern, m => "");

        return 0;
    }

    // (?<!) Zero-width negative lookbehind assertion.
    // (?<=) Zero-width positive lookbehind assertion.
    // (?!)  Zero-width negative lookahead assertion.
    // (?=)  Zero-width positive lookahead assertion.
    internal static readonly Regex DebugTypePattern = new Regex(@"(?<=<PropertyGroup.+?Configuration.+?==.+?Release.+?>.*?<DebugType>).+?(?=</DebugType>.*?</PropertyGroup>)", RegexOptions.Singleline);
    internal static readonly Regex RevisionPattern = new Regex(@"(?<!^\s*//.*)(?<=Assembly((File)|(Informational))Version\(""\d+\.\d+\.\d+)\.\d+", RegexOptions.Multiline);

    static void UpdateFile(string filePath, Regex regex, MatchEvaluator evaluator)
    {
        var encoding = DetectEncoding(filePath);
        var content = File.ReadAllText(filePath, encoding);
        var newContent = regex.Replace(content, evaluator);

        if (newContent == content)
        {
            Console.WriteLine("Unchanged: {0}", filePath);
        }
        else
        {
            File.WriteAllText(filePath, newContent, encoding);
            Console.WriteLine("Changed: {0}", filePath);
        }
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
}
