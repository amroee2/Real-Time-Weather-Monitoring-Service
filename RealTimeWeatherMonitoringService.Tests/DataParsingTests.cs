using Moq;
using RealTimeWeatherMonitoringService.DataParsing;

namespace RealTimeWeatherMonitoringService.Tests
{
    public class DataParsingTests
    {
        [Fact]
        public void ReadTemperature_ShouldReadTempreature()
        {
            //Arrange
            Mock<IDataParser> mockDataParser = new Mock<IDataParser>();
            AdapterDataParser dataParser = new AdapterDataParser(mockDataParser.Object);

            //Act
            dataParser.ReadTemperature("Temperature: 25.0");

            //Assert
            mockDataParser.Verify(x => x.ReadTemperature(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ReadHumidity_ShouldReadHumidity()
        {
            //Arrange
            Mock<IDataParser> mockDataParser = new Mock<IDataParser>();
            AdapterDataParser dataParser = new AdapterDataParser(mockDataParser.Object);

            //Act
            dataParser.ReadHumidity("Humidity: 50.0");

            //Assert
            mockDataParser.Verify(x => x.ReadHumidity(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("<WeatherData><Location>City Name</Location><Temperature>32</Temperature><Humidity>40</Humidity></WeatherData>", 32, "xml")]
        [InlineData("{\"Location\": \"City Name\", \"Temperature\": 32, \"Humidity\": 40}", 32.0, "json")]
        public void ReadTemperature_ShouldReadTemperatureFromMultipleFormats(string data, double expectedTemperature, string parserType)
        {
            //Arrange
            IDataParser parser = parserType == "xml"
                ? new XmlParser()
                : (IDataParser)new JsonParser();

            //Act
            double temperature = parser.ReadTemperature(data);

            //Assert
            Assert.Equal(expectedTemperature, temperature);
        }

        [Theory]
        [InlineData("<WeatherData><Location>City Name</Location><Temperature>32</Temperature><Humidity>40</Humidity></WeatherData>", 40, "xml")]
        [InlineData("{\"Location\": \"City Name\", \"Temperature\": 32, \"Humidity\": 40}", 40.0, "json")]
        public void ReadHumidity_ShouldReadHumidityFromMultipleFormats(string data, double expectedHumidity, string parserType)
        {
            //Arrange
            IDataParser parser = parserType == "xml"
                ? new XmlParser()
                : (IDataParser)new JsonParser();

            //Act
            double humidity = parser.ReadHumidity(data);

            //Assert
            Assert.Equal(expectedHumidity, humidity);
        }

        [Theory]
        [InlineData("xml", typeof(XmlParser))]
        [InlineData("json", typeof(JsonParser))]
        public void GetParser_ShouldReturnCorrectParser(string format, Type expectedParserType)
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