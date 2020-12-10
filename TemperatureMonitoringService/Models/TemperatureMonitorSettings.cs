namespace TemperatureMonitoringService.Models
{
    public class TemperatureMonitorSettings
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int PollTimeInMinutes { get; set; }
        public double MinimumTemperatureThreshold { get; set; }
        public double MaximumTemperatureThreshold { get; set; }
        public string EmailAlertsSubject { get; set; }
        public string EmailAlertsFrom { get; set; }
        public string EmailAlertsTo { get; set; }
    }
}
