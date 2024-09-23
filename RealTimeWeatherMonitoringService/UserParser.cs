using RealTimeWeatherMonitoringService.DataParsing;

namespace RealTimeWeatherMonitoringService
{
    public class UserParser
    {
        private readonly IDataParserFactory _factory;

        public UserParser(IDataParserFactory factory)
        {
            _factory = factory;
        }

        public AdapterDataParser InitializeParser(int format)
        {
            IDataParser parser = _factory.GetParser((Formats)format);
            AdapterDataParser dataParser = new AdapterDataParser(parser);
            return dataParser;
        }
    }
}
