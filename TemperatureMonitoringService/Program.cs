using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace TemperatureMonitoringService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(hostContext.Configuration).CreateLogger();
                    services.AddHostedService<Worker>();
                });
    }
}
