namespace RealTimeWeatherMonitoringService.DataParsing
{
    public class AdapterDataParser
    {
        private readonly IDataParser _parser;

        public AdapterDataParser(IDataParser parser)
        {
            _parser = parser;
        }

        public double readTemperature(string data)
        {
            return _parser.readTemperature(data);
        }

        public double readHumidity(string data)
        {
            return _parser.readHumidity(data);
        }
    }
}
