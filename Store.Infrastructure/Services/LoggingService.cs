﻿using NLog;
using Store.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog.Config;

namespace Store.Infrastructure.Services
{
    public class LoggingService : NLog.Logger, ILoggingService
    {
        private const string _loggerName = "NLogLogger";

        public void Debug(Exception exception, string format, params object[] args)
        {
            if (!base.IsDebugEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Debug, exception, format, args);
            base.Log(typeof(LoggingService), logEvent);
        }

        public void Error(Exception exception, string format, params object[] args)
        {
            if (!base.IsErrorEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Error, exception, format, args);
            base.Log(typeof(LoggingService), logEvent);
        }

        public void Fatal(Exception exception, string format, params object[] args)
        {
            if (!base.IsFatalEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Fatal, exception, format, args);
            base.Log(typeof(LoggingService), logEvent);
        }

        public void Info(Exception exception, string format, params object[] args)
        {
            if (!base.IsInfoEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Info, exception, format, args);
            base.Log(typeof(LoggingService), logEvent);
        }

        public void Trace(Exception exception, string format, params object[] args)
        {
            if (!base.IsTraceEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Trace, exception, format, args);
            base.Log(typeof(LoggingService), logEvent);
        }

        public void Warn(Exception exception, string format, params object[] args)
        {
            if (!base.IsWarnEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Warn, exception, format, args);
            base.Log(typeof(LoggingService), logEvent);
        }

        public void Debug(Exception exception)
        {
            this.Debug(exception, string.Empty);
        }

        public void Error(Exception exception)
        {
            this.Error(exception, string.Empty);
        }

        public void Fatal(Exception exception)
        {
            this.Fatal(exception, string.Empty);
        }

        public void Info(Exception exception)
        {
            this.Info(exception, string.Empty);
        }

        public void Trace(Exception exception)
        {
            this.Trace(exception, string.Empty);
        }

        public void Warn(Exception exception)
        {
            this.Warn(exception, string.Empty);
        }

        private LogEventInfo GetLogEvent(string loggerName, LogLevel level, Exception exception, string format, object[] args)
        {
            string assemblyProp = string.Empty;
            string classProp = string.Empty;
            string methodProp = string.Empty;
            string messageProp = string.Empty;
            string innerMessageProp = string.Empty;

            var logEvent = new LogEventInfo
                (level, loggerName, string.Format(format, args));

            if (exception != null)
            {
                assemblyProp = exception.Source;
                classProp = exception.TargetSite.DeclaringType.FullName;
                methodProp = exception.TargetSite.Name;
                messageProp = exception.Message;
                logEvent.Exception = exception;

                if (exception.InnerException != null)
                {
                    innerMessageProp = exception.InnerException.Message;
                }
            }

            logEvent.Properties["error-source"] = assemblyProp;
            logEvent.Properties["error-class"] = classProp;
            logEvent.Properties["error-method"] = methodProp;
            logEvent.Properties["error-message"] = messageProp;
            logEvent.Properties["inner-error-message"] = innerMessageProp;

            return logEvent;
        }
    }
}
