using AutoFixture;
using Moq;
using RealTimeWeatherMonitoringService.WeatherBots;

namespace RealTimeWeatherMonitoringService.Tests
{
    public class WeatherBotStrategyTests
    {
        [Fact]
        public void ShouldSetStrategyCorrectly()
        {
            var weatherBotMock = new Mock<WeatherBot<double>>();
            WeatherContext weatherContext = new WeatherContext();

            weatherContext.SetStrategy(weatherBotMock.Object);

            Assert.Equal(weatherBotMock.Object, weatherContext._weatherBot);
        }
        [Fact]
        public void ShouldSetWeatherContextCorrectly() {
            var weatherBotMock = new Mock<WeatherBot<double>>();
            WeatherContext weatherContext = new WeatherContext();
            weatherContext._weatherBot = weatherBotMock.Object;

            Fixture fixture = new Fixture();
            double value = fixture.Create<double>();
            weatherContext.CheckThreshold(value);

            weatherBotMock.Verify(w => w.CheckThreshold(It.IsAny<double>()), Times.Once);
        }
    }
}
