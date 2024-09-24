namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public class WeatherBotFactory : IBotFactory
    {
        public static readonly Dictionary<string, Func<BotData, WeatherBot<double>>> _botRegistry =
            new Dictionary<string, Func<BotData, WeatherBot<double>>>()
            {
                { "RainBot", botData => new RainBot(botData.enabled, botData.message, botData.humidityThreshold) },
                { "SunBot", botData => new SunBot(botData.enabled, botData.message, botData.temperatureThreshold) },
                { "SnowBot", botData => new SnowBot(botData.enabled, botData.message, botData.temperatureThreshold) }
            };

        public void RegisterBot(string botType, Func<BotData, WeatherBot<double>> creator)
        {
            _botRegistry[botType] = creator;
        }

        public WeatherBot<double> CreateBot(string botType, BotData botData)
        {
            if (_botRegistry.TryGetValue(botType, out var creator))
                return creator(botData);

            throw new NotSupportedException($"Bot type {botType} is not supported.");
        }
    }
}
