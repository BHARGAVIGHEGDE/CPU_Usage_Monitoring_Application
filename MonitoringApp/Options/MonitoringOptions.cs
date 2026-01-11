using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Options
{
    public class MonitoringOptions
    {
        public int CpuThreshold { get; set; }
        public int DiskThreshold { get; set; }
        public int MemoryThreshold { get; set; }
        public int LogLookbackMinutes { get; set; }// how old data shoudld be looked back
        public int CheckIntervalSeconds { get; set; }// the check should happen for ex: every 60 seconds
    }
}
