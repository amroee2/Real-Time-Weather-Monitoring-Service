namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public interface IWeatherBot
    {
        void CheckThreshold(double value);
        void TriggerMessage();
    }
}
