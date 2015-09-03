namespace ContosoUniversity.Core.Logging
{
    using System;
    using Utility.Logging;

    public static class LogManager
    {
        private static ILoggerFactory logFactory = new BlankLoggerFactory();

        public static void SetFactory(ILoggerFactory loggerFactory)
        {
            logFactory = loggerFactory;
        }

        public static ILogger CreateLogger(string name)
        {
            return logFactory.GetLogger(name);
        }

        public static ILogger CreateLogger(Type type)
        {
            return logFactory.GetLogger(type.FullName);
        }

        public static ILogger CreateLogger<T>()
        {
            return logFactory.GetLogger(typeof(T));
        }
    }
}