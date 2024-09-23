using RealTimeWeatherMonitoringService.DataParsing;
using RealTimeWeatherMonitoringService.Observer;
using RealTimeWeatherMonitoringService.Settings;
using RealTimeWeatherMonitoringService.WeatherBots;

namespace RealTimeWeatherMonitoringService
{
    class Program
    {
        static void Main(string[] args)
        {
            BotsSettings botsSettings = new BotsSettings();
            UserParser utility = new UserParser(new DataParserFactory());
            UserInterface userInterface = new UserInterface();
            WeatherData weatherData = new WeatherData();
            WeatherStation weatherStation = InitializeWeatherStation(weatherData, botsSettings);

            while (true)
            {
                if (!TryGetInputFormat(userInterface, out Formats format))
                    continue;

                if (format == Formats.Exit)
                    break;

                ProcessWeatherData(utility, userInterface, weatherStation, format);
            }
        }

        private static WeatherStation InitializeWeatherStation(WeatherData weatherData, BotsSettings botsSettings)
        {
            WeatherStation weatherStation = new WeatherStation(weatherData);
            List<WeatherBot<double>> weatherBots = botsSettings.ReadSettings().GetAwaiter().GetResult();

            foreach (var bot in weatherBots)
            {
                weatherStation.RegisterObserver((IObserver)bot);
            }

            return weatherStation;
        }

        private static bool TryGetInputFormat(UserInterface userInterface, out Formats format)
        {
            int inputFormat;
            try
            {
                inputFormat = userInterface.GetUserType();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid input format");
                Console.WriteLine(ex.Message);
                format = Formats.Exit;
                return false;
            }
            if (!Enum.TryParse(inputFormat.ToString(), out format))
            {
                Console.WriteLine("Invalid format");
                return false;
            }

            return true;
        }

        private static void ProcessWeatherData(UserParser utility, UserInterface userInterface, WeatherStation weatherStation, Formats format)
        {
            AdapterDataParser dataParser = utility.InitializeParser((int)format);
            string data = userInterface.GetUserInput();
            double humidity, temperature;
            try
            {
                humidity = dataParser.readHumidity(data);
                temperature = dataParser.readTemperature(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid input format");
                Console.WriteLine(ex.Message);
                return;
            }


            weatherStation.SetWeatherData(temperature, humidity);
        }
    }
}
