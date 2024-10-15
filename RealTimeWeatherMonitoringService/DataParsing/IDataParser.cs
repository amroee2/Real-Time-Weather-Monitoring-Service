namespace RealTimeWeatherMonitoringService.DataParsing
{
    public interface IDataParser
    {
        double ReadTemperature(string data);
        double ReadHumidity(string data);
    }
}
