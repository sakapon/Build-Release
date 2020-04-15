$source = @'
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

        foreach (var filePath in GetProjFilePaths(dirPath))
            UpdateFile(filePath);

        return 0;
    }

    static IEnumerable<string> GetProjFilePaths(string dirPath)
    {
        return Directory.EnumerateFiles(dirPath, "*.csproj", SearchOption.AllDirectories)
            .Concat(Directory.EnumerateFiles(dirPath, "*.vbproj", SearchOption.AllDirectories))
            .Concat(Directory.EnumerateFiles(dirPath, "*.fsproj", SearchOption.AllDirectories));
    }

    static void UpdateFile(string filePath)
    {
        Console.Write("{0}: ", filePath);
        var encoding = DetectEncoding(filePath);
        var content = File.ReadAllText(filePath, encoding);
        var newContent = UpdateContent(content, Path.GetFileNameWithoutExtension(filePath));
        if (newContent == content)
        {
            Console.WriteLine("Not Updated");
        }
        else
        {
            File.WriteAllText(filePath, newContent, encoding);
            Console.WriteLine("Updated");
        }
    }

    // (?<!) Zero-width negative lookbehind assertion.
    // (?<=) Zero-width positive lookbehind assertion.
    // (?!)  Zero-width negative lookahead assertion.
    // (?=)  Zero-width positive lookahead assertion.
    internal static readonly Regex ReleaseGroupPattern = new Regex(@"(?<=<PropertyGroup.+?Configuration.+?==.*?'Release.+?>).+?(?=</PropertyGroup>)", RegexOptions.Singleline);
    internal static readonly Regex AssemblyNamePattern = new Regex(@"(?<=<AssemblyName>).+?(?=</AssemblyName>)", RegexOptions.Multiline);

    internal static string UpdateContent(string content, string projName)
    {
        if (!content.Contains("<Version>"))
        {
            var match = Regex.Match(content, @"\s*</PropertyGroup>", RegexOptions.Singleline);
            if (match.Success)
                content = content.Insert(match.Index, "\r\n    <Version>1.0.0</Version>");
        }

        if (!ReleaseGroupPattern.IsMatch(content))
        {
            var releaseGroup = content.Contains("<OutputType>") ? ReleaseGroupForExe : string.Format(ReleaseGroupForDllFormat, GetAssemblyName(content) ?? projName);
            var match = Regex.Match(content, @"\s*</Project>", RegexOptions.Singleline);
            if (match.Success)
                content = content.Insert(match.Index, releaseGroup);
        }

        return content;
    }

    static string GetAssemblyName(string content)
    {
        var match = AssemblyNamePattern.Match(content);
        return string.IsNullOrWhiteSpace(match.Value) ? null : match.Value;
    }

    const string ReleaseGroupForExe = @"

  <PropertyGroup Condition=""'$(Configuration)|$(Platform)'=='Release|AnyCPU'"">
    <DebugType>none</DebugType>
    <DebugSymbols>false</DebugSymbols>
  </PropertyGroup>";

    const string ReleaseGroupForDllFormat = @"

  <PropertyGroup Condition=""'$(Configuration)|$(Platform)'=='Release|AnyCPU'"">
    <DebugType>none</DebugType>
    <DebugSymbols>false</DebugSymbols>
    <DocumentationFile>bin\Release\{0}.xml</DocumentationFile>
  </PropertyGroup>";

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
'@


"Initial Set for .NET Core"

Add-Type -TypeDefinition $source -Language CSharp
[InitialSet]::Main($Args)

"Initial Set: Completed"
