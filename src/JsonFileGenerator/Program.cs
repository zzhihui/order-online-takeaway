using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonFileGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            //Setup Dependency
            new BootStrap().Run();

            //Run Generator
            new Generator().Run();


        }
    }
}
