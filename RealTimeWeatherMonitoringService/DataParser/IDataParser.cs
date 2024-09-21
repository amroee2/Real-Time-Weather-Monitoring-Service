namespace RealTimeWeatherMonitoringService.DataParser
{
    public interface IDataParser
    {
        double? readTemperature(string data);
        double? readHumidity(string data);
    }
}
