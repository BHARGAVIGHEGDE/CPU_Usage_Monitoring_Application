using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Options
{
    public class LogsOptions
    {
        public DateTime? TimeCreated { get; set; }
        public string Level { get; set; }
        public string Provider { get; set; }
        public int? EventID { get; set; }
        public string Message { get; set; }
        public string LogName { get; set; }
    }
}
