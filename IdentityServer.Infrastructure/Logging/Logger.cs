using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.Infrastructure.Logging
{
    public class NLogLogger : ILogger
    {
        static private Dictionary<string, NLog.ILogger> _logsType = new Dictionary<string, NLog.ILogger>();
        static private LogFactory _logFactory = null;

        public NLog.ILogger Logger(string name)
        {
            if (_logFactory == null)
            {
                try
                {
                    _logFactory = LogManager.LoadConfiguration("nlog.config");
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex, "error in initializing logger");
                    throw;
                }
            }
            

            if (string.IsNullOrEmpty(name))
            {
                return Logger();
            }
            lock (_logsType)
            {

                if (!_logsType.ContainsKey(name))
                {
                    NLog.ILogger logger = _logFactory.GetLogger(name);
                    _logsType.Add(name, logger);
                    return logger;
                }
                else
                {
                    return _logsType[name];
                }
            }
        }

        public NLog.ILogger Logger()
        {
            return Logger("Core");
        }


        public void Debug(string message)
        {
            Logger().Debug(message);
        }

        public void Error(Exception ex, string message)
        {
            Logger().Error(ex, message);
        }

        public void Info(string message)
        {
            Logger().Info(message);
        }

        public void Trace(string message)
        {
            Logger().Trace(message);
        }

        public void Warn(string message)
        {
            Logger().Warn(message);
        }
    }
}
