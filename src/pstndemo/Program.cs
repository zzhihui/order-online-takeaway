using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using log4net.Config;
using Takeway.Mobile.Core.Utilities;

namespace pstndemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {


            var proc = Process.GetCurrentProcess();
            var count = Process.GetProcesses().Count(p => p.ProcessName == proc.ProcessName);
            if (count > 1)
                Application.Exit();
            else
            {
                Constants.ShopIdValue = int.Parse(ConfigurationManager.AppSettings["ShopId"]);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                XmlConfigurator.Configure();
                Application.Run(new Form1());    
            }

          
            
        }
    }
}
