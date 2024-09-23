namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public abstract class WeatherBot<T>
    {
        public bool Enabled { get; set; }
        public string Message { get; set; }
        public T Threshold { get; set; }

        public WeatherBot(bool enabled, string message, T threshold)
        {
            Enabled = enabled;
            Threshold = threshold;
            Message = message;
        }

        public abstract void CheckThreshold(T value);
        public abstract string GetWeatherType();
    }
}
