namespace RealTimeWeatherMonitoringService.DataParsing
{
    public class AdapterDataParser
    {
        private readonly IDataParser _parser;

        public AdapterDataParser(IDataParser parser)
        {
            _parser = parser;
        }

        public double ReadTemperature(string data)
        {
            return _parser.ReadTemperature(data);
        }

        public double ReadHumidity(string data)
        {
            return _parser.ReadHumidity(data);
        }
    }
}
