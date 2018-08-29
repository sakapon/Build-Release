using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static UnitTest.TestHelper;

namespace UnitTest.Version1up
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void IncrementForFile_1()
        {
            Program.IncrementForFile(@"..\..\..\UnitTest.csproj");
            Assert.Inconclusive("See the file.");
        }

        [TestMethod]
        public void DetectEncoding_1()
        {
            var Test = CreateAssertion<string, Encoding>(Program.DetectEncoding);

            Test(
                @"..\..\..\TestHelper.cs",
                Encoding.UTF8);
            Test(
                @"..\..\..\UnitTest.csproj",
                Program.UTF8N);
        }

        [TestMethod]
        public void IncrementForLine_1()
        {
            var Test = CreateAssertion<string, string>(Program.IncrementForLine);

            Test(
                "    <Version>1.23.456</Version>",
                "    <Version>1.23.457</Version>");
            Test(
                "    <AssemblyVersion>1.0.0.0</AssemblyVersion>",
                "    <AssemblyVersion>1.0.1.0</AssemblyVersion>");
            Test(
                "    <FileVersion>12.34.0</FileVersion>",
                "    <FileVersion>12.34.1</FileVersion>");
            Test(
                "<Version>1.23.456-beta</Version>",
                "<Version>1.23.457-beta</Version>");
            Test(
                "    <Authors>Xyz Company</Authors>",
                "    <Authors>Xyz Company</Authors>");
        }
    }
}
