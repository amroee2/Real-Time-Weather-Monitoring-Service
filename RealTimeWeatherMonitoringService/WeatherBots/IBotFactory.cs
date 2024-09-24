namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public interface IBotFactory
    {
        public void RegisterBot(string botType, Func<BotData, WeatherBot<double>> creator);
        public WeatherBot<double> CreateBot(string botType, BotData botData);
    }
}
