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
            //Arrange
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

            //Act
            weatherBot.Update(mockData.Object);

            //Assert
            mockContext.Verify(x => x.SetStrategy(weatherBot), Times.Once);
            mockContext.Verify(x => x.CheckThreshold(value), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetBotTestData))]
        public void ShouldCreateBot(string botType, BotData botData)
        {
            //Arrange
            var factory = new WeatherBotFactory();

            //Act
            var bot = factory.CreateBot(botType, botData);

            //Assert
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

        [Fact]
        public void ShouldRegisterBot()
        {
            //Arrange
            var factory = new WeatherBotFactory();

            //Act
            factory.RegisterBot("RainBot", botData => new RainBot(botData.enabled, botData.message, botData.humidityThreshold));
            factory.RegisterBot("SunBot", botData => new SunBot(botData.enabled, botData.message, botData.temperatureThreshold));
            factory.RegisterBot("SnowBot", botData => new SnowBot(botData.enabled, botData.message, botData.temperatureThreshold));

            //Assert
            Assert.Equal(3, WeatherBotFactory._botRegistry.Count);
        }

        [Fact]
        public async void ShouldCreateAllBots()
        {
            //Arrange
            Mock<IBotFactory> mockFactory = new Mock<IBotFactory>();

            mockFactory.SetupSequence(x => x.CreateBot(It.IsAny<string>(), It.IsAny<BotData>()))
                .Returns(new RainBot(true, "RainBot activated", 80.0))
                .Returns(new SnowBot(true, "SnowBot activated", -5.0))
                .Returns(new SunBot(true, "SunBot activated", 35.0));
            BotsSettings botsSettings = new BotsSettings(mockFactory.Object);

            //Act
            List<WeatherBot<double>> weatherBots  = await botsSettings.ReadSettings("IOTests", "config.json");

            //Assert
            Assert.Equal(3, weatherBots.Count);
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
    }
}
