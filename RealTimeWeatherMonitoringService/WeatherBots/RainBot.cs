using RealTimeWeatherMonitoringService.Observer;

namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public class RainBot : WeatherBot<double>, IObserver
    {

        public RainBot(bool enabled, string message, double humidityThreshold)
            : base(enabled, message, humidityThreshold) { }

        public override void CheckThreshold(double Humidity)
        {
            if (Humidity > Threshold && Enabled)
            {
                Console.WriteLine("RainBot activated");
                Console.WriteLine(Message);
            }
        }
        public override void Update(IData data)
        {
            context.SetStrategy(this);
            context.CheckThreshold(data.Humidity);
        }
    }
}
