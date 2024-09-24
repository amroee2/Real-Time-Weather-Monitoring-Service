using Newtonsoft.Json;
using RealTimeWeatherMonitoringService.WeatherBots;

namespace RealTimeWeatherMonitoringService.Settings
{
    public class BotsSettings
    {
        public List<WeatherBot<double>> Bots { get; private set; }
        public IBotFactory WeatherBotFactory { get; set; }

        public BotsSettings(IBotFactory weatherBotFactory)
        {
            Bots = new List<WeatherBot<double>>();
            WeatherBotFactory = weatherBotFactory;
        }

        public async Task<List<WeatherBot<double>>> ReadSettings()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Settings", "config.json");
            string json = await File.ReadAllTextAsync(filePath);

            var botsData = JsonConvert.DeserializeObject<Dictionary<string, BotData>>(json);

            foreach (var bot in botsData)
            {
                Bots.Add(WeatherBotFactory.CreateBot(bot.Key, bot.Value));
            }

            return Bots;
        }
    }
}
