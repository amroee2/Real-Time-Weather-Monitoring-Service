namespace RealTimeWeatherMonitoringService.DataParsing
{
    public class DataParserFactory
    {
        public IDataParser GetParser(Formats format)
        {
            return format switch
            {
                Formats.Json => new JsonParser(),
                Formats.Xml => new XmlParser(),
                _ => throw new ArgumentException("Unsupported format")
            };
        }
    }

}
