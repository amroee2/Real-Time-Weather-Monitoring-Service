using Moq;
using RealTimeWeatherMonitoringService.Observer;
using RealTimeWeatherMonitoringService.Settings;
using RealTimeWeatherMonitoringService.WeatherBots;

namespace RealTimeWeatherMonitoringService.Tests
{
    public class WeatherBotTests
    {
        [Theory]
        [InlineData("RainBot", 80.0)]
        [InlineData("SunBot", 50.0)]
        [InlineData("SnowBot", -1.0)]
        public void ShouldUpdate(string BotType, double value)
        {
            WeatherBot<double> weatherBot;
            Mock<IData> mockData = new Mock<IData>();
            mockData.Setup(x => x.Humidity).Returns(value);
            mockData.Setup(x => x.Temperature).Returns(value);
            Mock<IContext> mockContext = new Mock<IContext>();
            switch (BotType)
            {
                case "RainBot":
                    weatherBot = new RainBot(true, "RainBot activated", 80.0);
                    break;
                case "SunBot":
                    weatherBot = new SunBot(true, "SunBot activated", 50.0);
                    break;
                case "SnowBot":
                    weatherBot = new SnowBot(true, "SnowBot activated", 0.0);
                    break;
                default:
                    throw new ArgumentException("Unknown BotType");
            }
            mockContext.Setup(x => x.SetStrategy(weatherBot));
            weatherBot.context = mockContext.Object;
            weatherBot.Update(mockData.Object);
            mockContext.Verify(x => x.SetStrategy(weatherBot), Times.Once);
            mockContext.Verify(x => x.CheckThreshold(value), Times.Once);
        }
        [Theory]
        [MemberData(nameof(GetBotTestData))]
        public void ShouldCreateBot(string botType, BotData botData)
        {
            var factory = new WeatherBotFactory();

            var bot = factory.CreateBot(botType, botData);

            switch (botType)
            {
                case "RainBot":
                    Assert.IsType<RainBot>(bot);
                    break;
                case "SnowBot":
                    Assert.IsType<SnowBot>(bot);
                    break;
                case "SunBot":
                    Assert.IsType<SunBot>(bot);
                    break;
                default:
                    Assert.Fail($"Unknown bot type: {botType}");
                    break;
            }
        }
        public static IEnumerable<object[]> GetBotTestData()
        {
            yield return new object[]
            {
                "RainBot",
                new BotData
                {
                    enabled = true,
                    message = "RainBot activated",
                    humidityThreshold = 80.0
                }
            };
            yield return new object[]
            {
                "SnowBot",
                new BotData
                {
                    enabled = true,
                    message = "SnowBot activated",
                    temperatureThreshold = -5.0
                }
            };
            yield return new object[]
            {
                "SunBot",
                new BotData
                {
                    enabled = true,
                    message = "SunBot activated",
                    temperatureThreshold = 35.0
                }
            };
        }

        [Fact]
        public void ShouldRegisterBot()
        {
            var factory = new WeatherBotFactory();
            factory.RegisterBot("RainBot", botData => new RainBot(botData.enabled, botData.message, botData.humidityThreshold));
            factory.RegisterBot("SunBot", botData => new SunBot(botData.enabled, botData.message, botData.temperatureThreshold));
            factory.RegisterBot("SnowBot", botData => new SnowBot(botData.enabled, botData.message, botData.temperatureThreshold));

            Assert.Equal(3, WeatherBotFactory._botRegistry.Count);
        }
        [Fact]
        public async void ShouldCreateAllBots()
        {
            Mock<IBotFactory> mockFactory = new Mock<IBotFactory>();
            mockFactory.SetupSequence(x => x.CreateBot(It.IsAny<string>(), It.IsAny<BotData>()))
                .Returns(new RainBot(true, "RainBot activated", 80.0))
                .Returns(new SnowBot(true, "SnowBot activated", -5.0))
                .Returns(new SunBot(true, "SunBot activated", 35.0));
            BotsSettings botsSettings = new BotsSettings(mockFactory.Object);
            List<WeatherBot<double>> weatherBots  = await botsSettings.ReadSettings();
            Assert.Equal(3, weatherBots.Count);
        }
    }
}
