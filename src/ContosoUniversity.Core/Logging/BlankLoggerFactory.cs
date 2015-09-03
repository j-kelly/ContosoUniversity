namespace ContosoUniversity.Core.Logging
{
    using Utility.Logging;

    public class BlankLoggerFactory : LoggerFactoryBase
    {
        protected override ILogger CreateLogger(string name)
        {
            return new BlankLogger();
        }
    }
}
