using Newtonsoft.Json.Linq;

namespace RealTimeWeatherMonitoringService.DataParsing
{
    public class JsonParser : IDataParser
    {

        public double readTemperature(string json)
        {
            JObject jsonObject = JObject.Parse(json);
            return jsonObject["Temperature"].Value<double>();
        }

        public double readHumidity(string json)
        {
            JObject jsonObject = JObject.Parse(json);
            return jsonObject["Humidity"].Value<double>();
        }
    }
}
