using System.Diagnostics.Eventing.Reader;
using System.Text.Json;
using MonitoringApp.Options;

namespace MonitoringApp.Logs
{
    public class CPULogs
    {
        public string LogData(int maxEntries)
        {
            var logs = new List<LogsOptions>();

            try
            {
                using var reader = new EventLogReader("Application");

                for (EventRecord record = reader.ReadEvent();
                     record != null && logs.Count < maxEntries;
                     record = reader.ReadEvent())
                {
                    if (record.Level != 2) // Error only
                        continue;

                    logs.Add(new LogsOptions
                    {
                        TimeCreated = record.TimeCreated,
                        Level = record.LevelDisplayName,
                        Provider = record.ProviderName,
                        EventID = record.Id,
                        Message = Truncate(SafeFormat(record), 300),
                        LogName = record.LogName
                    });
                }
            }
            catch (Exception ex)
            {
                logs.Add(new LogsOptions
                {
                    TimeCreated = DateTime.UtcNow,
                    Level = "Error",
                    Provider = "CPULogs",
                    EventID = -1,
                    Message = "Event log access failed: " + ex.Message,
                    LogName = "Application"
                });
            }

            return JsonSerializer.Serialize(
                logs,
                new JsonSerializerOptions { WriteIndented = true });
        }

        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }


        private static string SafeFormat(EventRecord record)
        {
            try
            {
                return record.FormatDescription() ?? "No description";
            }
            catch
            {
                return "Unable to format description";
            }
        }
    }
}
