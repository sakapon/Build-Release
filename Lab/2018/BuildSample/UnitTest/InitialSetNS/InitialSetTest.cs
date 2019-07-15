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
            InitialSet.Main(new[] { @"..\..\" });
            Assert.Inconclusive("See the file.");
        }

        [TestMethod]
        public void DebugTypePattern_1()
        {
            var Test = CreateAssertion<string, string>(s => InitialSet.DebugTypePattern.Replace(s, m => "none"));

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
                "<PropertyGroup Configuration == Release ><DebugType>pdbonly</DebugType></PropertyGroup><PropertyGroup Configuration == Release ><DebugType>full</DebugType></PropertyGroup>",
                "<PropertyGroup Configuration == Release ><DebugType>none</DebugType></PropertyGroup><PropertyGroup Configuration == Release ><DebugType>none</DebugType></PropertyGroup>");
        }
    }
}
