using MonitoringApp.Notifications;
using MonitoringApp.Options;
using Microsoft.Extensions.Hosting.WindowsServices;
using MonitoringApp;
using Microsoft.Extensions.Hosting;
using MonitoringApp.Monitoring;
using MonitoringApp.Logs;
using MonitoringApp.Services;

var builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService() //this application will run as window service
    .ConfigureServices((context, services) =>
    {

        services.Configure<EmailOptions>(
            context.Configuration.GetSection("Email"));//reads email section from appsettings.json
        services.Configure<MonitoringOptions>(
            context.Configuration.GetSection("Monitoring"));//reads monitoring from appsettings.json
        services.AddSingleton<CpuMonitor>();
        services.AddSingleton<MemoryMonitor>();
        services.AddSingleton<DiskMonitor>();
        services.AddSingleton<CPULogs>();
        services.AddHttpClient<AiService>();



        //If a BackgroundService throws an exception, do NOT stop the host.
        services.Configure<HostOptions>(options =>
        {
            options.BackgroundServiceExceptionBehavior =
                BackgroundServiceExceptionBehavior.Ignore;
        });

        services.AddSingleton<EmailNotification>();//register Email service
        services.AddHostedService<Worker>();//this is a background job
    });

await builder.Build().RunAsync();
