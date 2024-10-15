namespace RealTimeWeatherMonitoringService.Observer
{
    public interface IObserver
    {
        void Update(IData weatherData);

    }
}
