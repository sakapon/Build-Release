using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipReleaseConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ZipHelper.CreateZipFileForAssembly(@"..\..\");
            ZipHelper.CreateZipFileForAssembly(@"..\..\..\UnitTest");
        }
    }
}
