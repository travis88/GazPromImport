using GazPromImport.Core;
using System;

namespace GazPromImport.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportLauncher launcher = new ImportLauncher();
            launcher.Start(@"D:\www\gmch.ru\gmch.ru\ImportFiles\last_update\TESTOUTPOSTS.xml");
            Console.ReadLine();
        }
    }
}
