using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Takeaway.Service.IntegrationTests
{
    public class TestBase
    {
        static public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }


        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            System.Environment.CurrentDirectory = AssemblyDirectory;
        }


    }
}
