namespace RealTimeWeatherMonitoringService.Observer
{
    public class WeatherData : IData
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }

        public void SetData(double temperature, double humidity)
        {
            Temperature = temperature;
            Humidity = humidity;
        }
    }
}
