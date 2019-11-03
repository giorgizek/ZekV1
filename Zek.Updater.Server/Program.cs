using System;
using System.Windows.Forms;

namespace Zek.Updater.Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //SevenZipHelper.CompressFileLZMA(@"d:\gzekalashvili\Documents\Visual Studio 2012\Projects\Home\Zek\bin\Release\Zek.dll", @"d:\gzekalashvili\Documents\Visual Studio 2012\Projects\Home\Zek\bin\Release\Zek.dll.7z");
            //SevenZipHelper.DecompressFileLZMA(@"d:\gzekalashvili\Documents\Visual Studio 2012\Projects\Home\Zek\bin\Release\Zek.dll.7z", @"d:\gzekalashvili\Documents\Visual Studio 2012\Projects\Home\Zek\bin\Release\Zek2.dll");
            //return;

            var deploy = false;
            var close = false;
            if (args != null)
            {
                foreach (var arg in args)
                {
                    switch (arg.ToLowerInvariant())
                    {
                        case "deploy":
                            deploy = true;
                            break;

                        case "close":
                            close = true;
                            break;
                    }
                }
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(deploy, close));
        }


    }
}
