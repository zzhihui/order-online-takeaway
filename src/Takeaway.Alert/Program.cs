using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takeaway.Alert.Job;

namespace Takeaway.Alert
{
    class Program
    {
        static void Main(string[] args)
        {
            var job = new EmailStatusJob();
            job.EmailStatusReport();
        }
    }
}
