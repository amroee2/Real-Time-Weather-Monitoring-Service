using Moq;
using RealTimeWeatherMonitoringService.Observer;

namespace RealTimeWeatherMonitoringService.Tests
{
    public class ObserverTests
    {
        [Fact]
        public void ShouldRegisterObserver()
        {
            Mock<IObserver> mockObserver = new Mock<IObserver>();
            WeatherStation weatherStation = new WeatherStation(new WeatherData());
            weatherStation.RegisterObserver(mockObserver.Object);

            mockObserver.Verify(x => x.Update(It.IsAny<IData>()), Times.Never);
        }
        [Fact]
        public void ShouldRemoveObserver()
        {
            Mock<IObserver> mockObserver = new Mock<IObserver>();
            WeatherStation weatherStation = new WeatherStation(new WeatherData());
            weatherStation.RegisterObserver(mockObserver.Object);
            weatherStation.RemoveObserver(mockObserver.Object);

            mockObserver.Verify(x => x.Update(It.IsAny<IData>()), Times.Never);
        }
        [Fact]
        public void ShouldNotifyObserversWithCorrectData()
        {
            Mock<IObserver> mockObserver = new Mock<IObserver>();
            WeatherStation weatherStation = new WeatherStation(new WeatherData());
            weatherStation.RegisterObserver(mockObserver.Object);

            weatherStation.SetWeatherData(25.0, 50.0);

            mockObserver.Verify(x => x.Update(It.Is<IData>(data =>
                data.Temperature == 25.0 && data.Humidity == 50.0)), Times.Once);
        }
        [Fact]
        public void ShouldNotifyAllRegisteredObservers()
        {
            Mock<IObserver> mockObserver1 = new Mock<IObserver>();
            Mock<IObserver> mockObserver2 = new Mock<IObserver>();
            WeatherStation weatherStation = new WeatherStation(new WeatherData());
            weatherStation.RegisterObserver(mockObserver1.Object);
            weatherStation.RegisterObserver(mockObserver2.Object);

            weatherStation.SetWeatherData(25.0, 50.0);

            mockObserver1.Verify(x => x.Update(It.IsAny<IData>()), Times.Once);
            mockObserver2.Verify(x => x.Update(It.IsAny<IData>()), Times.Once);
        }

    }
}
