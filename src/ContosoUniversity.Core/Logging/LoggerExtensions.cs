namespace ContosoUniversity.Core.Logging
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using Utility.Logging;

    public static class LoggerExtensions
    {
        private const string TimerThresholdKey = "Logging.TimerThreshold";
        private const int DefaultTimerThreshold = 100;

        private static readonly ILogger TimerLogger = LogManager.CreateLogger("Timer");

        [DebuggerStepThrough]
        public static void LogTimings(this ILogger logger, string message, Action action)
        {
            LogTimings(logger, DefaultTimerThreshold, message, action);
        }

        [DebuggerStepThrough]
        public static void LogTimings(this ILogger logger, int timerThreshold, string message, Action action)
        {
            Func<object> actionWrapper = () => { action(); return null; };
            LogTimings<object>(logger, timerThreshold, message, actionWrapper);
        }

        [DebuggerStepThrough]
        public static T LogTimings<T>(this ILogger logger, string message, Func<T> func)
        {
            return LogTimings(logger, DefaultTimerThreshold, message, func);
        }

        [DebuggerStepThrough]
        public static T LogTimings<T>(this ILogger logger, int timerThreshold, string message, Func<T> func)
        {
            if (!TimerLogger.IsInfoEnabled)
                return func.Invoke();

            var sw = Stopwatch.StartNew();
            var exceptionThrown = false;

            try { return func.Invoke(); }
            catch { exceptionThrown = true; throw; }
            finally
            {
                if (sw.ElapsedMilliseconds >= timerThreshold)
                {
                    var thresholdSetting = ConfigurationManager.AppSettings[TimerThresholdKey];
                    int.TryParse(thresholdSetting, out timerThreshold);

                    var exceptionText = exceptionThrown ? "EXCEPTION THROWN" : string.Empty;
                    var msg = $"{sw.ElapsedMilliseconds} ms|{message}|{exceptionText}";
                    TimerLogger.Debug(msg);
                }
            }
        }
    }
}