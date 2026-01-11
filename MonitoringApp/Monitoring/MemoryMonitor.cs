using System.Diagnostics;

namespace MonitoringApp.Monitoring
{
    public class MemoryMonitor
    {
        public double GetUsagePercentage()
        {
            var process = Process.GetCurrentProcess();

            // Working set = memory used by process
            double usedMB = process.WorkingSet64 / (1024.0 * 1024.0);

            // Total physical memory (approx)
            double totalMB = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes
                             / (1024.0 * 1024.0);

            return (usedMB / totalMB) * 100;
        }
    }
}