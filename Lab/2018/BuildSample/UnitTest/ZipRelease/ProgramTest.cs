using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.ZipRelease
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void GetAssemblyFileVersion_1()
        {
            var actual = ZipReleaseConsole.Program.GetAssemblyFileVersion(@"..\Release\ZipReleaseConsole.exe");
            Console.WriteLine(actual);
            Assert.Inconclusive("See the console.");
        }
    }
}
