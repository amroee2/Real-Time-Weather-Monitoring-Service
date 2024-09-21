namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public class RainBot : IWeatherBot
    {
        private bool Enabled { get; set; }
        double HumidityThreshold { get; set; }
        string Message { get; set; }

        public RainBot(bool enabled, double humidityThreshold, string message)
        {
            Enabled = enabled;
            HumidityThreshold = humidityThreshold;
            Message = message;
        }
        public void CheckThreshold(double Humidity)
        {
            if (Humidity > HumidityThreshold)
            {
                Console.WriteLine("RainBot activated");
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
