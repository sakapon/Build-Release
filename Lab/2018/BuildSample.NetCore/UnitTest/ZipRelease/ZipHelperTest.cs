using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static UnitTest.TestHelper;

namespace UnitTest.ZipRelease
{
    [TestClass]
    public class ZipHelperTest
    {
        [TestMethod]
        public void CreateZipFileForAssembly_1()
        {
            ZipHelper.CreateZipFileForAssembly(@"..\..\..\..\ZipReleaseConsole");
            Assert.Inconclusive("See the file.");
        }

        [TestMethod]
        public void GetVersion_1()
        {
            var Test = CreateAssertion<string, string>(ZipHelper.GetVersion);

            Test(
                @"..\..\..\UnitTest.csproj",
                "1.2.3-beta");
        }

        [TestMethod]
        public void CreateZipFile_1()
        {
            ZipHelper.CreateZipFile(@"..\..\Release", @"..\zip", "CreateZipFile_1.zip");
            Assert.Inconclusive("See the file.");
        }

        [TestMethod]
        public void CreateZipFile_2()
        {
            ZipHelper.CreateZipFile(@"..\..\Release", ".", "CreateZipFile_2.zip");
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
