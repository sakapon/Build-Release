using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.ZipRelease
{
    [TestClass]
    public class ZipHelperTest
    {
        [TestMethod]
        public void GetAssemblyFileVersion_1()
        {
            var actual = ZipHelper.GetAssemblyFileVersion(@"..\Release\ZipReleaseConsole.exe");
            Console.WriteLine(actual);
            Assert.Inconclusive("See the console.");
        }

        [TestMethod]
        public void CreateZipFile_1()
        {
            ZipHelper.CreateZipFile(@"..\Release", @"..\zip", "CreateZipFile_1.zip");
            Assert.Inconclusive("See the file.");
        }

        [TestMethod]
        public void CreateZipFile_2()
        {
            ZipHelper.CreateZipFile(@"..\Release", ".", "CreateZipFile_2.zip");
            Assert.Inconclusive("See the file.");
        }

        [TestMethod]
        public void CreateZipFile_3()
        {
            ZipHelper.CreateZipFile(".", @"..\", "CreateZipFile_3.zip");
            Assert.Inconclusive("See the file.");
        }
    }
}
