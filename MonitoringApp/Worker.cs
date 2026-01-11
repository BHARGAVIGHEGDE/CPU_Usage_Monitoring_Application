using Microsoft.Extensions.Options;
using MonitoringApp.Logs;
using MonitoringApp.Monitoring;
using MonitoringApp.Notifications;
using MonitoringApp.Options;
using MonitoringApp.Services;

public class Worker : BackgroundService
{
    private readonly EmailNotification _emailnotify;
    private readonly CpuMonitor _cpu;
    private readonly MemoryMonitor _memory;
    private readonly DiskMonitor _disk;
    private readonly MonitoringOptions _options;
    private readonly CPULogs _logs;
    private readonly AiService _aireport;

    private DateTime? _lastSentMail = null;

    public Worker(
        EmailNotification emailnotify,
        CpuMonitor cpu,
        MemoryMonitor memory,
        DiskMonitor disk,
        IOptions<MonitoringOptions> options,
        CPULogs logs,
        AiService aireport)
    {
        _emailnotify = emailnotify;
        _cpu = cpu;
        _memory = memory;
        _disk = disk;
        _options = options.Value;
        _logs = logs;
        _aireport = aireport;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                double cpuUsage = _cpu.GetUsage();
                double memoryUsage = _memory.GetUsagePercentage();
                double diskUsage = _disk.GetUsagePercentage();

                bool thresholdCrossed =
                    cpuUsage > _options.CpuThreshold ||
                    memoryUsage > _options.MemoryThreshold ||
                    diskUsage > _options.DiskThreshold;

                if (thresholdCrossed)
                {
                    string logsJson = _logs.LogData(5);

                    string prompt = $"""
                    Generate a system incident report.

                    CPU Usage: {cpuUsage:F2}%
                    Memory Usage: {memoryUsage:F2}%
                    Disk Usage: {diskUsage:F2}%

                    Windows Application Error Logs (JSON):
                    {logsJson}

                    Include:
                    - Summary
                    - Probable cause
                    - Impact
                    - Recommended next steps
                    """;

                    string report = await _aireport.GenerateReportAsync(prompt);

                    _emailnotify.SendTestEmail(
                        "System Incident Report",
                        report
                    );
                }
            }
            catch (Exception ex)
            {
             
            }

            await Task.Delay(
                TimeSpan.FromSeconds(_options.CheckIntervalSeconds),
                stoppingToken);
        }
    }
}
