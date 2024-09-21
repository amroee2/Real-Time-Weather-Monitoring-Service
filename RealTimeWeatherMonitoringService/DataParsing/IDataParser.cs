namespace RealTimeWeatherMonitoringService.DataParsing
{
    public interface IDataParser
    {
        double readTemperature(string data);
        double readHumidity(string data);
    }
}
