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
            var Test = CreateAssertion<string, bool>(s => InitialSet.ReleaseGroupPattern.IsMatch(s));

            Test(
                "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\r\n  <DebugType>pdbonly</DebugType>\r\n  </PropertyGroup>\r\n",
                true);
            Test(
                "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\r\n  <DebugType>full</DebugType>\r\n  </PropertyGroup>\r\n",
                false);
            Test(
                "<PropertyGroup Configuration == 'Release > </PropertyGroup>",
                true);
            Test(
                "<PropertyGroup Configuration == 'Release ><DebugType>pdbonly</DebugType></PropertyGroup><PropertyGroup Configuration == 'Release ><DebugType>full</DebugType></PropertyGroup>",
                true);
        }

        [TestMethod]
        public void AssemblyNamePattern_1()
        {
            var Test = CreateAssertion<string, string>(s => InitialSet.AssemblyNamePattern.Match(s).Value);

            Test(
                "<AssemblyName>App123</AssemblyName>",
                "App123");
            Test(
                "<AssemblyName>App123",
                "");
            Test(
                "<AssemblyTitle>App123</AssemblyTitle>",
                "");
            Test(
                "\r\n<AssemblyName>App123</AssemblyName>\r\n<AssemblyName>App456</AssemblyName>\r\n",
                "App123");
        }
    }
}
