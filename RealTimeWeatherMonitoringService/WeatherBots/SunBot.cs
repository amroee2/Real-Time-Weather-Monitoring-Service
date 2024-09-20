namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public class SunBot : IWeatherBot
    {
        bool Enabled { get; set; }
        double TemperatureThreshold { get; set; }
        string Message { get; set; }

        public SunBot(bool enabled, double temperatureThreshold, string message)
        {
            Enabled = enabled;
            TemperatureThreshold = temperatureThreshold;
            Message = message;
        }
        public void CheckThreshold(double Temperature)
        {
            if (Temperature > TemperatureThreshold)
            {
                TriggerMessage();
            }
        }

        public void TriggerMessage()
        {
            if (Enabled)
            {
                Console.WriteLine(Message);
            }
        }
    }
}
