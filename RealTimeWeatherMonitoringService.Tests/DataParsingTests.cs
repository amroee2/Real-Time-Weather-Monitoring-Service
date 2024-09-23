using Moq;
using RealTimeWeatherMonitoringService.DataParsing;

namespace RealTimeWeatherMonitoringService.Tests
{
    public class DataParsingTests
    {
        [Fact]
        public void ShouldReadTempreature()
        {
            Mock<IDataParser> mockDataParser = new Mock<IDataParser>();
            AdapterDataParser dataParser = new AdapterDataParser(mockDataParser.Object);
            dataParser.readTemperature("Temperature: 25.0");

            mockDataParser.Verify(x => x.readTemperature(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public void ShouldReadHumidity()
        {
            Mock<IDataParser> mockDataParser = new Mock<IDataParser>();
            AdapterDataParser dataParser = new AdapterDataParser(mockDataParser.Object);
            dataParser.readHumidity("Humidity: 50.0");

            mockDataParser.Verify(x => x.readHumidity(It.IsAny<string>()), Times.Once);
        }
        [Theory]
        [InlineData("<WeatherData><Location>City Name</Location><Temperature>32</Temperature><Humidity>40</Humidity></WeatherData>", 32, "xml")]
        [InlineData("{\"Location\": \"City Name\", \"Temperature\": 32, \"Humidity\": 40}", 32.0, "json")]
        public void ShouldReadTemperatureFromMultipleFormats(string data, double expectedTemperature, string parserType)
        {
            IDataParser parser = parserType == "xml"
                ? new XmlParser()
                : (IDataParser)new JsonParser();

            double temperature = parser.readTemperature(data);

            Assert.Equal(expectedTemperature, temperature);
        }
        [Theory]
        [InlineData("<WeatherData><Location>City Name</Location><Temperature>32</Temperature><Humidity>40</Humidity></WeatherData>", 40, "xml")]
        [InlineData("{\"Location\": \"City Name\", \"Temperature\": 32, \"Humidity\": 40}", 40.0, "json")]
        public void ShouldReadHumidityFromMultipleFormats(string data, double expectedHumidity, string parserType)
        {
            IDataParser parser = parserType == "xml"
                ? new XmlParser()
                : (IDataParser)new JsonParser();

            double humidity = parser.readHumidity(data);

            Assert.Equal(expectedHumidity, humidity);
        }
        [Theory]
        [InlineData("xml", typeof(XmlParser))]
        [InlineData("json", typeof(JsonParser))]
        public void ShouldReturnCorrectParser(string format, Type expectedParserType)
        {
            DataParserFactory factory = new DataParserFactory();

            IDataParser parser = factory.GetParser(format == "xml" ? Formats.Xml : Formats.Json);

            Assert.IsType(expectedParserType, parser);
        }
    }
}