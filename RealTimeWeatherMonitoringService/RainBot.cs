namespace RealTimeWeatherMonitoringService
{
    public class RainBot : IWeatherBot
    {
        private bool Enabled { get; set; }
        double HumidityThreshold { get; set; }
        string Message { get; set; }

        public void CheckThreshold(double Humidity)
        {
            if (Humidity > HumidityThreshold)
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
