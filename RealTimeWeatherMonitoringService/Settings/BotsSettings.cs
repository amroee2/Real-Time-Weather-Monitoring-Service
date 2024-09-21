using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealTimeWeatherMonitoringService.WeatherBots;

namespace RealTimeWeatherMonitoringService.Settings
{
    public class BotsSettings
    {
        List<IWeatherBot> _bots;

        public BotsSettings(List<IWeatherBot> bots)
        {
            _bots = bots;
        }

        public void readSettings()
        {
            try
            {
                string baseDirectory = AppContext.BaseDirectory;
                string filePath = Path.Combine(baseDirectory, "Settings", "config.json");
                string json = File.ReadAllText(filePath);
                JObject jsonObject = JObject.Parse(json);
                AddBots(jsonObject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void AddBots(JObject jsonObject)
        {
            RainBot? rainBot = jsonObject["RainBot"]?.ToObject<RainBot>();
            SunBot? sunBot = jsonObject["SunBot"]?.ToObject<SunBot>();
            SnowBot? snowBot = jsonObject["SnowBot"]?.ToObject<SnowBot>();
            _bots.Add(rainBot);
            _bots.Add(sunBot);
            _bots.Add(snowBot);
        }
    }
}
