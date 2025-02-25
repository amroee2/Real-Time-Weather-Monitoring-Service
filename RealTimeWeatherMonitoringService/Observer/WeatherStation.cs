﻿namespace RealTimeWeatherMonitoringService.Observer
{
    public class WeatherStation : ISubject
    {
        private List<IObserver> _observers;

        private IData _weatherData;
        public WeatherStation(IData weatherData)
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
            _weatherData.Temperature = temperature;
            _weatherData.Humidity = humidity;
            NotifyObservers();
        }
    }
}
