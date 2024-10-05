using Moq;
using RealTimeWeatherMonitoringService.Observer;

namespace RealTimeWeatherMonitoringService.Tests
{
    public class ObserverTests
    {

        [Fact]
        public void ShouldRegisterObserver()
        {
            //Arrange
            Mock<IObserver> mockObserver = new Mock<IObserver>();
            WeatherStation weatherStation = new WeatherStation(new WeatherData());

            //Act
            weatherStation.RegisterObserver(mockObserver.Object);

            //Assert
            mockObserver.Verify(x => x.Update(It.IsAny<IData>()), Times.Never);
        }

        [Fact]
        public void ShouldRemoveObserver()
        {
            //Arrange
            Mock<IObserver> mockObserver = new Mock<IObserver>();
            WeatherStation weatherStation = new WeatherStation(new WeatherData());

            //Act
            weatherStation.RegisterObserver(mockObserver.Object);
            weatherStation.RemoveObserver(mockObserver.Object);

            //Assert
            mockObserver.Verify(x => x.Update(It.IsAny<IData>()), Times.Never);
        }

        [Fact]
        public void ShouldNotifyObserversWithCorrectData()
        {
            //Arrange
            Mock<IObserver> mockObserver = new Mock<IObserver>();
            WeatherStation weatherStation = new WeatherStation(new WeatherData());

            //Act
            weatherStation.RegisterObserver(mockObserver.Object);
            weatherStation.SetWeatherData(25.0, 50.0);

            //Assert
            mockObserver.Verify(x => x.Update(It.Is<IData>(data =>
                data.Temperature == 25.0 && data.Humidity == 50.0)), Times.Once);
        }

        [Fact]
        public void ShouldNotifyAllRegisteredObservers()
        {
            //Arrange
            Mock<IObserver> mockObserver1 = new Mock<IObserver>();
            Mock<IObserver> mockObserver2 = new Mock<IObserver>();
            WeatherStation weatherStation = new WeatherStation(new WeatherData());

            //Act
            weatherStation.RegisterObserver(mockObserver1.Object);
            weatherStation.RegisterObserver(mockObserver2.Object);
            weatherStation.SetWeatherData(25.0, 50.0);

            //Assert
            mockObserver1.Verify(x => x.Update(It.IsAny<IData>()), Times.Once);
            mockObserver2.Verify(x => x.Update(It.IsAny<IData>()), Times.Once);
        }

    }
}
