using AutoFixture;
using Moq;
using RealTimeWeatherMonitoringService.WeatherBots;

namespace RealTimeWeatherMonitoringService.Tests
{
    public class WeatherBotStrategyTests
    {

        [Fact]
        public void SetStrategy_ShouldSetStrategyCorrectly()
        {
            //Arrange
            var weatherBotMock = new Mock<WeatherBot<double>>();
            WeatherContext weatherContext = new WeatherContext();

            //Act
            weatherContext.SetStrategy(weatherBotMock.Object);

            //Assert
            Assert.Equal(weatherBotMock.Object, weatherContext._weatherBot);
        }

        [Fact]
        public void CheckThreshold_ShouldSetWeatherContextCorrectly() {

            //Arrange
            var weatherBotMock = new Mock<WeatherBot<double>>();
            WeatherContext weatherContext = new WeatherContext();
            weatherContext._weatherBot = weatherBotMock.Object;

            Fixture fixture = new Fixture();
            double value = fixture.Create<double>();

            //Act
            weatherContext.CheckThreshold(value);

            //Assert
            weatherBotMock.Verify(w => w.CheckThreshold(It.IsAny<double>()), Times.Once);
        }
    }
}
