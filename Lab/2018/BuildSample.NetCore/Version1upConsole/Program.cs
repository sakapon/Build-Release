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

        return 0;
    }

    internal static void IncrementForFile(string filePath)
    {
        throw new NotImplementedException();
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
