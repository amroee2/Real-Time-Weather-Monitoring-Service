namespace RealTimeWeatherMonitoringService.Observer
{
    public interface IData
    {
        double Temperature { get; set; }
        double Humidity { get; set; }
    }
}
