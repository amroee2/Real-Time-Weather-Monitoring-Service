namespace RealTimeWeatherMonitoringService.DataParsing
{
    public class DataParser
    {
        private readonly IDataParser _parser;

        public DataParser(IDataParser parser)
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
