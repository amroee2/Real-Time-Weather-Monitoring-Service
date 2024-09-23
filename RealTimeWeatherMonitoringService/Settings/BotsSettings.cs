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
            string json =await File.ReadAllTextAsync(filePath);
            var botsData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);

            foreach (var bot in botsData)
            {
                Bots.Add(WeatherBotFactory.CreateBot(bot.Key, bot.Value));
            }

            return Bots;
        }
    }
}
