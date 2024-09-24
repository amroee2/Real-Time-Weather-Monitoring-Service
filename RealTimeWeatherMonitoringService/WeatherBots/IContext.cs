namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public interface IContext
    {
        void SetStrategy(WeatherBot<double> strategy);
        void CheckThreshold(double value);
    }
}
