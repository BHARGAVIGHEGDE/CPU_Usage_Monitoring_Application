using System.IO;

namespace MonitoringApp.Monitoring
{
    public class DiskMonitor
    {
        public double GetUsagePercentage()
        {
            var drive = new DriveInfo(
                Path.GetPathRoot(Environment.SystemDirectory));

            double total = drive.TotalSize;
            double free = drive.TotalFreeSpace;

            return ((total - free) / total) * 100;
        }
    }
}
