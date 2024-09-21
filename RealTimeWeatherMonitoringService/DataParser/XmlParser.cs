using System.Xml;

namespace RealTimeWeatherMonitoringService.DataParser
{
    public class XmlParser : IDataParser
    {
        public double? readTemperature(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return double.Parse(doc.SelectSingleNode("/Temperature")?.InnerText);
        }

        public double? readHumidity(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return double.Parse(doc.SelectSingleNode("/Humidity")?.InnerText);
        }
    }
}
