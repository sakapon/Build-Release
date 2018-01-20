using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.VersionIncrement
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void IncrementLine_1()
        {
            void IncrementLine_Test(string line, string expected)
            {
                var actual = Program.IncrementLine(line);
                Assert.AreEqual(expected, actual);
            }

            IncrementLine_Test(
                "[assembly: AssemblyVersion(\"1.0.0.0\")]",
                "[assembly: AssemblyVersion(\"1.0.1.0\")]");
            IncrementLine_Test(
                "[assembly: AssemblyFileVersion(\"1.23.456\")]",
                "[assembly: AssemblyFileVersion(\"1.23.457\")]");
            IncrementLine_Test(
                "// [assembly: AssemblyVersion(\"1.0.*\")]",
                "// [assembly: AssemblyVersion(\"1.0.*\")]");
            IncrementLine_Test(
                "[assembly: AssemblyCompany(\"Xyz Company\")]",
                "[assembly: AssemblyCompany(\"Xyz Company\")]");
        }
    }
}
