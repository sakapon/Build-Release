using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static UnitTest.TestHelper;

namespace UnitTest.VersionIncrement
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void IncrementLine_1()
        {
            var Test = CreateAssertion<string, string>(Program.IncrementLine);

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
