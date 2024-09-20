namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public class WeatherContext
    {
        public List<IWeatherBot> _weatherBots;

        public WeatherContext(List<IWeatherBot> weatherBots)
        {
            _weatherBots = weatherBots;
        }

        public void CheckThreshold(double value)
        {
            foreach(var weatherBot in _weatherBots)
            {
                weatherBot.CheckThreshold(value);
            }
        }
    }
}
