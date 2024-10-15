namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public class BotData
    {
        public bool enabled { get; set; }
        public string message { get; set; }
        public double humidityThreshold { get; set; }
        public double temperatureThreshold { get; set; }
    }
}
