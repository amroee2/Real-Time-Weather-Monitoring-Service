using Moq;
using RealTimeWeatherMonitoringService.Observer;

namespace RealTimeWeatherMonitoringService.Tests
{
    public class ObserverTests
    {

        [Fact]
        public void RegisterObserver_ShouldRegisterObserver()
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
        public void RemoveObserver_ShouldRemoveObserver()
        {
            //Arrange
            Mock<IObserver> mockObserver = new Mock<IObserver>();
            WeatherStation weatherStation = new WeatherStation(new WeatherData());
            weatherStation.RegisterObserver(mockObserver.Object);

            //Act
            weatherStation.RemoveObserver(mockObserver.Object);

            //Assert
            mockObserver.Verify(x => x.Update(It.IsAny<IData>()), Times.Never);
        }

        [Fact]
        public void SetWeatherData_ShouldNotifyObserversWithCorrectData()
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
        public void SetWeatherData_ShouldNotifyAllRegisteredObservers()
        {
            //Arrange
            Mock<IObserver> mockObserver1 = new Mock<IObserver>();
            Mock<IObserver> mockObserver2 = new Mock<IObserver>();
            WeatherStation weatherStation = new WeatherStation(new WeatherData());
            weatherStation.RegisterObserver(mockObserver1.Object);
            weatherStation.RegisterObserver(mockObserver2.Object);
            //Act
            weatherStation.SetWeatherData(25.0, 50.0);

            //Assert
            mockObserver1.Verify(x => x.Update(It.IsAny<IData>()), Times.Once);
            mockObserver2.Verify(x => x.Update(It.IsAny<IData>()), Times.Once);
        }

    }
}
