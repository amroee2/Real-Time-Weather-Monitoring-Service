using System.Xml;

namespace RealTimeWeatherMonitoringService.DataParsing
{
    public class XmlParser : IDataParser
    {
        public double readTemperature(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return double.Parse(doc.SelectSingleNode("/WeatherData/Temperature")?.InnerText);
        }

        public double readHumidity(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return double.Parse(doc.SelectSingleNode("/WeatherData/Humidity")?.InnerText);
        }
    }
}
