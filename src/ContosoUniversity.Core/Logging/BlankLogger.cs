namespace ContosoUniversity.Core.Logging
{
    using System;
    using Utility.Logging;

    public class BlankLogger : ILogger
    {
        public BlankLogger()
        {
            IsDebugEnabled = false;
            IsErrorEnabled = false;
            IsFatalEnabled = false;
            IsInfoEnabled = false;
            IsTraceEnabled = false;
            IsWarnEnabled = false;
            Name = "Empty logger";
        }

        public void Debug(string message, params object[] parameters)
        {
        }

        public void Debug(Exception exception, string message, params object[] parameters)
        {
        }

        public void Error(string message, params object[] parameters)
        {
        }

        public void Error(Exception exception, string message, params object[] parameters)
        {
        }

        public void Fatal(string message, params object[] parameters)
        {
        }

        public void Fatal(Exception exception, string message, params object[] parameters)
        {
        }

        public void Info(string message, params object[] parameters)
        {
        }

        public void Info(Exception exception, string message, params object[] parameters)
        {
        }

        public void Trace(string message, params object[] parameters)
        {
        }

        public void Trace(Exception exception, string message, params object[] parameters)
        {
        }

        public void Warn(string message, params object[] parameters)
        {
        }

        public void Warn(Exception exception, string message, params object[] parameters)
        {
        }

        public ILogger GetCurrentInstanceLogger()
        {
            return this;
        }

        public ILogger GetLogger(Type type)
        {
            return this;
        }

        public ILogger GetLogger(string name)
        {
            return this;
        }

        public bool IsDebugEnabled { get; }

        public bool IsErrorEnabled { get; }

        public bool IsFatalEnabled { get; }

        public bool IsInfoEnabled { get; }

        public bool IsTraceEnabled { get; }

        public bool IsWarnEnabled { get; }

        public string Name { get; }
    }
}
