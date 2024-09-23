using RealTimeWeatherMonitoringService.Observer;

namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public class SunBot : WeatherBot<double>, IObserver
    {
        public SunBot(bool enabled, string message, double temperatureThreshold)
            : base(enabled, message, temperatureThreshold) { }

        public override void CheckThreshold(double Temperature)
        {
            if (Temperature > Threshold && Enabled)
            {
                Console.WriteLine("SunBot activated");
                Console.WriteLine(Message);
            }
        }
        public override string GetWeatherType() => "Temperature";

        public void Update(IData data)
        {
            var context = new WeatherContext();
            context.SetStrategy(this);
            context.CheckThreshold(data.Temperature);
        }
    }
}
