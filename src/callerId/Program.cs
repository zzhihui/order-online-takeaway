using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using callerId.Telmessage;
using log4net;
using log4net.Config;
using Takeway.Mobile.Core.Utilities;

namespace callerId
{
    class Program
    {
        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var proc = Process.GetCurrentProcess();
            var count = Process.GetProcesses().Count(p => p.ProcessName == proc.ProcessName);
            if (count > 1)
                Environment.Exit(0);
            Constants.ShopIdValue = int.Parse(ConfigurationManager.AppSettings["ShopId"]);

            new CallerIdProcessor().ProcessCallerId();

        }
    }
}
