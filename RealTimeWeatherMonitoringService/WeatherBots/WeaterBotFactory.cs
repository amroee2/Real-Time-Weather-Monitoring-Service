using RealTimeWeatherMonitoringService.WeatherBots;

public class WeatherBotFactory
{
    private static readonly Dictionary<string, Func<dynamic, WeatherBot<double>>> _botRegistry =
        new Dictionary<string, Func<dynamic, WeatherBot<double>>>()
        {
            { "RainBot", botData => new RainBot((bool)botData.enabled, (string)botData.message, (double)botData.humidityThreshold) },
            { "SunBot", botData => new SunBot((bool)botData.enabled, (string)botData.message, (double)botData.temperatureThreshold) },
            { "SnowBot", botData => new SnowBot((bool)botData.enabled, (string)botData.message, (double)botData.temperatureThreshold) }
        };

    public static void RegisterBot(string botType, Func<dynamic, WeatherBot<double>> creator)
    {
        _botRegistry[botType] = creator;
    }

    public static WeatherBot<double> CreateBot(string botType, dynamic botData)
    {
        if (_botRegistry.TryGetValue(botType, out var creator))
            return creator(botData);

        throw new NotSupportedException($"Bot type {botType} is not supported.");
    }
}
