namespace RealTimeWeatherMonitoringService
{
    public class SnowBot : IWeatherBot
    {
        bool Enabled { get; set; }
        double TemperatureThreshold { get; set; }
        string Message { get; set; }

        public void CheckThreshold(double Temperature)
        {
            if(Temperature < TemperatureThreshold)
            {
                TriggerMessage();
            }
        }

        public void TriggerMessage()
        {
            if (Enabled)
            {
                Console.WriteLine(Message);
            }
        }
    }
}
