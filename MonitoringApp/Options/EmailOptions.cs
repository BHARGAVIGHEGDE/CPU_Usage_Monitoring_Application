using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Options
{
    public class EmailOptions
    {
        public string SmtpServer { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Port { get; set; }

    }
}
