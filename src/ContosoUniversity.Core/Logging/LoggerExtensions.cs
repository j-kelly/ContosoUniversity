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
        private readonly static int _TimerThreshold;

        private static readonly ILogger TimerLogger = LogManager.CreateLogger("Timer");

        static LoggerExtensions()
        {
            var thresholdSetting = ConfigurationManager.AppSettings[TimerThresholdKey];
            if (!int.TryParse(thresholdSetting, out _TimerThreshold))
                _TimerThreshold = DefaultTimerThreshold;
        }

        [DebuggerStepThrough]
        public static void LogTimings(this ILogger logger, string message, Action action)
        {
            LogTimings(logger, _TimerThreshold, message, action);
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
            return LogTimings(logger, _TimerThreshold, message, func);
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
                    var exceptionText = exceptionThrown ? "EXCEPTION THROWN" : string.Empty;
                    var msg = $"{sw.ElapsedMilliseconds} ms|{message}|{exceptionText}";
                    TimerLogger.Debug(msg);
                }
            }
        }
    }
}