using System.Xml;

namespace RealTimeWeatherMonitoringService.DataParsing
{
    public class XmlParser : IDataParser
    {
        public double ReadTemperature(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return double.Parse(doc.SelectSingleNode("/WeatherData/Temperature")?.InnerText);
        }

        public double ReadHumidity(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return double.Parse(doc.SelectSingleNode("/WeatherData/Humidity")?.InnerText);
        }
    }
}
