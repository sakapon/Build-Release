﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class Program
{
    public static int Main(string[] args)
    {
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
        var contents = File.ReadLines(filePath, Encoding.UTF8)
            .Select(IncrementForLine)
            .ToArray();
        File.WriteAllLines(filePath, contents, Encoding.UTF8);
    }

    static readonly Regex BuildNumberPattern = new Regex(@"(?<=Assembly(File)?Version\(""\d+\.\d+\.)\d+");

    internal static string IncrementForLine(string line)
    {
        if (line.StartsWith("//")) return line;

        return BuildNumberPattern.Replace(line, m => IncrementNumber(m.Value));
    }

    static string IncrementNumber(string i)
    {
        return (int.Parse(i) + 1).ToString();
    }
}
