using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static UnitTest.TestHelper;

namespace UnitTest.VersionIncrement
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void IncrementForFile_1()
        {
            Program.IncrementForFile(@"..\..\Properties\AssemblyInfo.cs");
            Assert.Inconclusive("See the file.");
        }

        [TestMethod]
        public void DetectEncoding_1()
        {
            var Test = CreateAssertion<string, Encoding>(Program.DetectEncoding);

            Test(
                @"..\..\TestHelper.cs",
                Encoding.UTF8);
        }

        [TestMethod]
        public void IncrementForLine_1()
        {
            var Test = CreateAssertion<string, string>(Program.IncrementForLine);

            Test(
                "[assembly: AssemblyVersion(\"1.0.0.0\")]",
                "[assembly: AssemblyVersion(\"1.0.1.0\")]");
            Test(
                "[assembly: AssemblyFileVersion(\"1.23.456\")]",
                "[assembly: AssemblyFileVersion(\"1.23.457\")]");
            Test(
                "[assembly: AssemblyFileVersion(\"1.23.456-beta\")]",
                "[assembly: AssemblyFileVersion(\"1.23.457-beta\")]");
            Test(
                "[assembly: AssemblyInformationalVersion(\"12.34.0\")]",
                "[assembly: AssemblyInformationalVersion(\"12.34.1\")]");
            Test(
                "// [assembly: AssemblyVersion(\"1.0.0.0\")]",
                "// [assembly: AssemblyVersion(\"1.0.0.0\")]");
            Test(
                " // [assembly: AssemblyVersion(\"1.0.0.0\")]",
                " // [assembly: AssemblyVersion(\"1.0.0.0\")]");
            Test(
                "[assembly: AssemblyCompany(\"Xyz Company\")]",
                "[assembly: AssemblyCompany(\"Xyz Company\")]");
        }
    }
}
