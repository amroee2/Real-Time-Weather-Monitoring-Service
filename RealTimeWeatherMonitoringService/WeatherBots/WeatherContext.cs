namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public class WeatherContext
    {
        public WeatherBot<double> _weatherBot;

        public void SetStrategy(WeatherBot<double> strategy)
        {
            _weatherBot = strategy;
        }
        public void CheckThreshold(double value)
        {
            _weatherBot.CheckThreshold(value);
        }
    }
}
