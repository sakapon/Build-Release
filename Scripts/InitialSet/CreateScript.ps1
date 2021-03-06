﻿$source = @"
using System;
using System.IO;
using System.Text;

public static class Program
{
    public static int Main(string[] args)
    {
        var format = File.ReadAllText("CSharp-Format.ps1.txt", Encoding.UTF8);
        var cs_script = File.ReadAllText(@"..\..\Lab\2018\BuildSample\UnitTest\InitialSetNS\InitialSet.cs", Encoding.UTF8);
        var ps_script = string.Format(format, cs_script);
        File.WriteAllText("KTools.InitialSet.ps1", ps_script, Encoding.UTF8);

        return 0;
    }
}
"@

Add-Type -TypeDefinition $source -Language CSharp
[Program]::Main($Args)
