namespace RealTimeWeatherMonitoringService.DataParsing
{
    public interface IDataParserFactory
    {
        IDataParser GetParser(Formats format);
    }
}
