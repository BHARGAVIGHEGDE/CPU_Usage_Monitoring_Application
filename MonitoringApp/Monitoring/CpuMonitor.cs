using System.Diagnostics;

namespace MonitoringApp.Monitoring
{
    public class CpuMonitor
    {
        private TimeSpan _lastTotalProcessorTime;
        private DateTime _lastCheckTime;

        public CpuMonitor()
        {
            _lastTotalProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
            _lastCheckTime = DateTime.UtcNow;
        }

        public double GetUsage()
        {
            var process = Process.GetCurrentProcess();

            var currentTotalProcessorTime = process.TotalProcessorTime;
            var currentTime = DateTime.UtcNow;

            var cpuUsedMs =
                (currentTotalProcessorTime - _lastTotalProcessorTime).TotalMilliseconds;

            var elapsedMs =
                (currentTime - _lastCheckTime).TotalMilliseconds;

            _lastTotalProcessorTime = currentTotalProcessorTime;
            _lastCheckTime = currentTime;

            if (elapsedMs <= 0)
                return 0;

            var cpuUsageTotal =
                cpuUsedMs / (Environment.ProcessorCount * elapsedMs) * 100;

            return Math.Round(cpuUsageTotal, 2);
        }
    }
}
