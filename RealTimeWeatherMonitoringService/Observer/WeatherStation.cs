namespace RealTimeWeatherMonitoringService.Observer
{
    public class WeatherStation : ISubject
    {
        private List<IObserver> _observers;
        private double _temperature;
        private double _humidity;
        private WeatherData _weatherData;
        public WeatherStation(WeatherData weatherData)
        {
            _observers = new List<IObserver>();
            _weatherData = weatherData;
        }

        public void RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(_weatherData);
            }
        }

        public void SetWeatherData(double temperature, double humidity)
        {
            _temperature = temperature;
            _humidity = humidity;
            NotifyObservers();
        }
    }
}
