﻿using RealTimeWeatherMonitoringService.Observer;

namespace RealTimeWeatherMonitoringService.WeatherBots
{
    public class SnowBot : WeatherBot<double>, IObserver
    {
        public SnowBot(bool enabled, string message, double temperatureThreshold)
            : base(enabled, message, temperatureThreshold) { }

        public override void CheckThreshold(double Temperature)
        {
            if (Temperature < Threshold && Enabled)
            {
                Console.WriteLine("SnowBot activated");
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
