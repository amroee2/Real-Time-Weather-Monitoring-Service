using Moq;
using RealTimeWeatherMonitoringService.DataParsing;

namespace RealTimeWeatherMonitoringService.Tests
{
    public class DataParsingTests
    {
        [Fact]
        public void ShouldReadTempreature()
        {
            //Arrange
            Mock<IDataParser> mockDataParser = new Mock<IDataParser>();
            AdapterDataParser dataParser = new AdapterDataParser(mockDataParser.Object);

            //Act
            dataParser.readTemperature("Temperature: 25.0");

            //Assert
            mockDataParser.Verify(x => x.readTemperature(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public void ShouldReadHumidity()
        {
            //Arrange
            Mock<IDataParser> mockDataParser = new Mock<IDataParser>();
            AdapterDataParser dataParser = new AdapterDataParser(mockDataParser.Object);

            //Act
            dataParser.readHumidity("Humidity: 50.0");

            //Assert
            mockDataParser.Verify(x => x.readHumidity(It.IsAny<string>()), Times.Once);
        }
        [Theory]
        [InlineData("<WeatherData><Location>City Name</Location><Temperature>32</Temperature><Humidity>40</Humidity></WeatherData>", 32, "xml")]
        [InlineData("{\"Location\": \"City Name\", \"Temperature\": 32, \"Humidity\": 40}", 32.0, "json")]
        public void ShouldReadTemperatureFromMultipleFormats(string data, double expectedTemperature, string parserType)
        {
            //Arrange
            IDataParser parser = parserType == "xml"
                ? new XmlParser()
                : (IDataParser)new JsonParser();

            //Act
            double temperature = parser.readTemperature(data);

            //Assert
            Assert.Equal(expectedTemperature, temperature);
        }
        [Theory]
        [InlineData("<WeatherData><Location>City Name</Location><Temperature>32</Temperature><Humidity>40</Humidity></WeatherData>", 40, "xml")]
        [InlineData("{\"Location\": \"City Name\", \"Temperature\": 32, \"Humidity\": 40}", 40.0, "json")]
        public void ShouldReadHumidityFromMultipleFormats(string data, double expectedHumidity, string parserType)
        {
            //Arrange
            IDataParser parser = parserType == "xml"
                ? new XmlParser()
                : (IDataParser)new JsonParser();

            //Act
            double humidity = parser.readHumidity(data);

            //Assert
            Assert.Equal(expectedHumidity, humidity);
        }
        [Theory]
        [InlineData("xml", typeof(XmlParser))]
        [InlineData("json", typeof(JsonParser))]
        public void ShouldReturnCorrectParser(string format, Type expectedParserType)
        {
            //Arrange
            DataParserFactory factory = new DataParserFactory();

            //Act
            IDataParser parser = factory.GetParser(format == "xml" ? Formats.Xml : Formats.Json);

            //Assert
            Assert.IsType(expectedParserType, parser);
        }
    }
}