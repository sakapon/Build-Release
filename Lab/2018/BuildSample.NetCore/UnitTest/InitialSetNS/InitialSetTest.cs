using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static UnitTest.TestHelper;

namespace UnitTest.InitialSetNS
{
    [TestClass]
    public class InitialSetTest
    {
        [TestMethod]
        public void Main_1()
        {
            InitialSet.Main(new[] { @"..\..\..\..\" });
            Assert.Inconclusive("See the file.");
        }

        [TestMethod]
        public void ReleaseGroupPattern_1()
        {
            var Test = CreateAssertion<string, string>(s => InitialSet.ReleaseGroupPattern.Replace(s, m => "none"));

            Test(
                "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\r\n  <DebugType>pdbonly</DebugType>\r\n  </PropertyGroup>\r\n",
                "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\r\n  <DebugType>none</DebugType>\r\n  </PropertyGroup>\r\n");
            Test(
                "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\r\n  <DebugType>full</DebugType>\r\n  </PropertyGroup>\r\n",
                "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\r\n  <DebugType>full</DebugType>\r\n  </PropertyGroup>\r\n");
            Test(
                "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \"><DebugType>pdbonly</DebugType></PropertyGroup>",
                "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \"><DebugType>none</DebugType></PropertyGroup>");
            Test(
                "<PropertyGroup Configuration == 'Release ><DebugType>pdbonly</DebugType></PropertyGroup><PropertyGroup Configuration == 'Release ><DebugType>full</DebugType></PropertyGroup>",
                "<PropertyGroup Configuration == 'Release ><DebugType>none</DebugType></PropertyGroup><PropertyGroup Configuration == 'Release ><DebugType>none</DebugType></PropertyGroup>");
        }

        [TestMethod]
        public void AssemblyNamePattern_1()
        {
            var Test = CreateAssertion<string, string>(s => InitialSet.AssemblyNamePattern.Replace(s, m => ""));

            Test(
                "[assembly: AssemblyVersion(\"1.0.0.0\")]",
                "[assembly: AssemblyVersion(\"1.0.0.0\")]");
            Test(
                "[assembly: AssemblyFileVersion(\"1.23.456.0\")]",
                "[assembly: AssemblyFileVersion(\"1.23.456\")]");
            Test(
                "\r\n  [assembly: AssemblyInformationalVersion(\"12.34.56.78\")]  \r\n",
                "\r\n  [assembly: AssemblyInformationalVersion(\"12.34.56\")]  \r\n");
            Test(
                "[assembly: AssemblyInformationalVersion(\"1.2.3.4-alpha\")]",
                "[assembly: AssemblyInformationalVersion(\"1.2.3\")]");
            Test(
                "// [assembly: AssemblyFileVersion(\"1.0.0.0\")]",
                "// [assembly: AssemblyFileVersion(\"1.0.0.0\")]");
            Test(
                " // [assembly: AssemblyFileVersion(\"1.0.0.0\")]",
                " // [assembly: AssemblyFileVersion(\"1.0.0.0\")]");
            Test(
                "[assembly: AssemblyCompany(\"Xyz Company\")]",
                "[assembly: AssemblyCompany(\"Xyz Company\")]");
            Test(
                "\r\nAssemblyFileVersion(\"1.0.0.0\")\r\nAssemblyInformationalVersion(\"1.0.0.0\")\r\n",
                "\r\nAssemblyFileVersion(\"1.0.0\")\r\nAssemblyInformationalVersion(\"1.0.0\")\r\n");
        }
    }
}
