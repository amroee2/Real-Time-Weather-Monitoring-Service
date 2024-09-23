using Newtonsoft.Json;
using RealTimeWeatherMonitoringService.WeatherBots;

namespace RealTimeWeatherMonitoringService.Settings
{
    public class BotsSettings
    {
        public List<WeatherBot<double>> Bots { get; private set; }

        public BotsSettings()
        {
            Bots = new List<WeatherBot<double>>();
        }

        public async Task<List<WeatherBot<double>>> ReadSettings()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Settings", "config.json");
            string json = await File.ReadAllTextAsync(filePath);
            dynamic? weatherData = JsonConvert.DeserializeObject(json);

            WeatherBot<double> rainBot = new RainBot(
                (bool) weatherData.RainBot.enabled,
                (string) weatherData.RainBot.message,
                (double)weatherData.RainBot.humidityThreshold
            );
            Bots.Add(rainBot);

            WeatherBot<double> sunBot = new SunBot(
                (bool) weatherData.SunBot.enabled,
                (string) weatherData.SunBot.message,
                (double)weatherData.SunBot.temperatureThreshold
            );
            Bots.Add(sunBot);

            WeatherBot<double> snowBot = new SnowBot(
                (bool) weatherData.SnowBot.enabled,
                (string) weatherData.SnowBot.message,
                (double)weatherData.SnowBot.temperatureThreshold
            );
            Bots.Add(snowBot);

            return Bots;
        }
    }
}
