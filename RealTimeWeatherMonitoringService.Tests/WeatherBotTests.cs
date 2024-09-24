using Moq;
using RealTimeWeatherMonitoringService.Observer;
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
    }
}
