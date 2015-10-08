namespace ContosoUniversity.Core.Logging
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Utility.Logging;

    public static class LoggerExtensions
    {
        private const string TimerThresholdKey = "Logging.TimerThreshold";
        private const int DefaultTimerThreshold = 100;
        private const string MessageFormat = "{0} ms|{1}.{2} ({3}){4}|{5}";

        private static readonly ILogger TimerLogger = LogManager.CreateLogger("Timer");
        private static readonly ILogger AuditLogger = LogManager.CreateLogger("Audit");

        [DebuggerStepThrough]
        public static void LogTimings(this ILogger logger, string message = "", [CallerFilePath] string sourceFile = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (logger.IsTraceEnabled)
            {
                var msg = string.Format(
                    "{0}.{1}:{2}|{3}",
                    Path.GetFileNameWithoutExtension(sourceFile),
                    memberName,
                    lineNumber,
                    message);

                logger.Trace(msg);
            }
        }

        [DebuggerStepThrough]
        public static void LogTimings(this ILogger logger, Action action, string message = "", [CallerFilePath] string sourceFile = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, bool alwaysLog = false)
        {
            var timerThreshold = DefaultTimerThreshold;
            if (TimerLogger.IsInfoEnabled)
            {
                var thresholdSetting = ConfigurationManager.AppSettings[TimerThresholdKey];
                int.TryParse(thresholdSetting, out timerThreshold);
            }

            LogTimings(logger, memberName, null, timerThreshold, action, message, sourceFile, lineNumber, alwaysLog);
        }

        [DebuggerStepThrough]
        public static void LogTimings(this ILogger logger, object methodCall, Action action, string message = "", [CallerFilePath] string sourceFile = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, bool alwaysLog = false)
        {
            var timerThreshold = DefaultTimerThreshold;
            WriteObjectDetails(Guid.NewGuid(), methodCall);
            LogTimings(logger, memberName, methodCall, timerThreshold, action, message, sourceFile, lineNumber, alwaysLog);
        }

        [DebuggerStepThrough]
        public static void LogTimings(this ILogger logger, string codeName, Action action, string message = "", [CallerFilePath] string sourceFile = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, bool alwaysLog = false)
        {
            var timerThreshold = DefaultTimerThreshold;
            LogTimings(logger, codeName, null, timerThreshold, action, message, sourceFile, lineNumber, alwaysLog);
        }

        [DebuggerStepThrough]
        public static void LogTimings(this ILogger logger, int timerThreshold, Action action, string message = "", [CallerFilePath] string sourceFile = "", [CallerMemberName] string codeName = "", [CallerLineNumber] int lineNumber = 0, bool alwaysLog = false)
        {
            LogTimings(logger, codeName, null, timerThreshold, action, message, sourceFile, lineNumber, alwaysLog);
        }

        [DebuggerStepThrough]
        public static void LogTimings(this ILogger logger, string codeName, object methodCall, int timerThreshold, Action action, string message = "", [CallerFilePath] string sourceFile = "", [CallerLineNumber] int lineNumber = 0, bool alwaysLog = false)
        {
            LogTimings(logger, message, sourceFile, codeName, lineNumber);

            if (!TimerLogger.IsInfoEnabled)
            {
                action.Invoke();
                return;
            }

            var sw = Stopwatch.StartNew();
            var exceptionMsg = string.Empty;

            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                {
                    innerEx = innerEx.InnerException;
                }

                exceptionMsg = "Exception: " + innerEx.Message;
                throw;
            }
            finally
            {
                if (TimerLogger.IsInfoEnabled)
                {
                    var thresholdSetting = ConfigurationManager.AppSettings[TimerThresholdKey];
                    int.TryParse(thresholdSetting, out timerThreshold);
                }

                if (TimerLogger.IsTraceEnabled || sw.ElapsedMilliseconds >= timerThreshold || alwaysLog)
                {
                    var msg2 = string.Format(
                         MessageFormat,
                         sw.ElapsedMilliseconds,
                         logger.Name,
                         codeName,
                         methodCall != null ? methodCall.GetType().Name : string.Empty,
                         !string.IsNullOrWhiteSpace(message) ? " => " + message : string.Empty,
                         exceptionMsg);

                    TimerLogger.Debug(msg2);
                }
            }
        }

        [DebuggerStepThrough]
        public static T LogTimings<T>(this ILogger logger, Func<T> func, string message = "", [CallerFilePath] string sourceFile = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, bool alwaysLog = false)
        {
            var timerThreshold = DefaultTimerThreshold;
            return LogTimings(logger, memberName, null, timerThreshold, func, message, sourceFile, lineNumber, alwaysLog);
        }

        [DebuggerStepThrough]
        public static T LogTimings<T>(this ILogger logger, object methodCall, Func<T> func, string message = "", [CallerFilePath] string sourceFile = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, bool alwaysLog = false)
        {
            var response = default(T);
            var timerThreshold = DefaultTimerThreshold;
            var id = Guid.NewGuid();
            WriteObjectDetails(id, methodCall);
            try
            {
                response = LogTimings(logger, memberName, methodCall, timerThreshold, func, message, sourceFile, lineNumber, alwaysLog);
                WriteObjectDetails(id, response);
            }
            catch (Exception exception)
            {
                WriteObjectDetails(id, exception);
                throw;
            }

            return response;
        }

        private static void WriteObjectDetails(Guid id, object methodCall)
        {
            if (!AuditLogger.IsInfoEnabled)
                return;

            var reflectedData = methodCall.GetType().GetProperties().Aggregate(
                $"Method call: {methodCall}",
                (current, property) => current + $"{property.Name} : {property.GetValue(methodCall)} ");

            AuditLogger.Info(string.Format("GUID: {0} {1} ", id, reflectedData));
        }

        [DebuggerStepThrough]
        public static T LogTimings<T>(this ILogger logger, string codeName, Func<T> func, string message = "", [CallerFilePath] string sourceFile = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0, bool alwaysLog = false)
        {
            var timerThreshold = DefaultTimerThreshold;
            return LogTimings(logger, codeName, null, timerThreshold, func, message, sourceFile, lineNumber, alwaysLog);
        }

        [DebuggerStepThrough]
        public static T LogTimings<T>(this ILogger logger, int timerThreshold, Func<T> func, string message = "", [CallerFilePath] string sourceFile = "", [CallerMemberName] string codeName = "", [CallerLineNumber] int lineNumber = 0, bool alwaysLog = false)
        {
            return LogTimings(logger, codeName, null, timerThreshold, func, message, sourceFile, lineNumber, alwaysLog);
        }

        [DebuggerStepThrough]
        public static T LogTimings<T>(this ILogger logger, string codeName, object methodCall, int timerThreshold, Func<T> func, string message = "", [CallerFilePath] string sourceFile = "", [CallerLineNumber] int lineNumber = 0, bool alwaysLog = false)
        {
            LogTimings(logger, message, sourceFile, codeName, lineNumber);

            if (!TimerLogger.IsInfoEnabled)
            {
                return func.Invoke();
            }

            var sw = Stopwatch.StartNew();
            var exceptionMsg = string.Empty;
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                {
                    innerEx = innerEx.InnerException;
                }

                exceptionMsg = "Exception: " + innerEx.Message;
                throw;
            }
            finally
            {
                if (TimerLogger.IsInfoEnabled)
                {
                    var thresholdSetting = ConfigurationManager.AppSettings[TimerThresholdKey];
                    int.TryParse(thresholdSetting, out timerThreshold);
                }

                if (TimerLogger.IsTraceEnabled || sw.ElapsedMilliseconds >= timerThreshold || alwaysLog)
                {
                    var msg2 = string.Format(
                         MessageFormat,
                         sw.ElapsedMilliseconds,
                         logger.Name,
                         codeName,
                         methodCall != null ? methodCall.GetType().Name : string.Empty,
                         !string.IsNullOrWhiteSpace(message) ? " => " + message : string.Empty,
                         exceptionMsg);

                    TimerLogger.Debug(msg2);
                }
            }
        }
    }
}