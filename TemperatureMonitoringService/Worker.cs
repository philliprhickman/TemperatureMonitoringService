using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using TemperatureMonitoringService.Models;

namespace TemperatureMonitoringService
{
    public class Worker : BackgroundService
    {
        private readonly int _commandTimeout;
        private readonly string _connectionString;
        private readonly ILogger<Worker> _logger;
        private readonly TemperatureMonitorSettings _temperatureMonitorSettings = new TemperatureMonitorSettings();

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _commandTimeout = config.GetValue<int>("CommandTimeout");
            _connectionString = config.GetConnectionString("DefaultConnection");
            _logger = logger;

            config.GetSection("TemperatureMonitorSettings").Bind(_temperatureMonitorSettings);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await PollTemperatureMonitor();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An Error Occurred in ExecuteAsync Method!");
                }
                finally
                {
                    await Task.Delay((int)TimeSpan.FromMinutes(_temperatureMonitorSettings.PollTimeInMinutes).TotalMilliseconds, stoppingToken);
                }
            }
        }

        #region Polling Methods

        private async Task PollTemperatureMonitor()
        {
            try
            {
                await Task.Run(async () =>
                {
                    _logger.LogInformation("Polling Temperature Sensors...");

                    // Log some dummy data...for now.
                    await InsertTemperatureReading(75.5m, 75.3m);
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "An Error Occurred in PollTemperatureMonitor Method!");
            }
        }

        #endregion

        #region Database Methods

        private async Task InsertTemperatureReading(decimal probe1Temperature, decimal probe2Temperature)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cm = new SqlCommand("tmpmntr_TemperatureReadings_Insert", cn))
                    {
                        cm.CommandTimeout = _commandTimeout;
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.AddWithValue("@Probe1Temperature", probe1Temperature);
                        cm.Parameters.AddWithValue("@Probe2Temperature", probe2Temperature);
                        cn.Open();

                        await cm.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occurred in the InsertTemperatureReading Method!");
            }
        }

        #endregion
    }
}
