namespace RealTimeWeatherMonitoringService
{
    public class UserInterface
    {
        public int GetUserType()
        {
            Console.WriteLine("Enter weather data");
            Console.WriteLine("1- JSON\n2- XML");
            int format = Convert.ToInt32(Console.ReadLine());
            return format;
        }
        public string GetUserInput()
        {
            Console.WriteLine("Enter weather data");
            string input = Console.ReadLine();
            return input;
        }
    }
}
