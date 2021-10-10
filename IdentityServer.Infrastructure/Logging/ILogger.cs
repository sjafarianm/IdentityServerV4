using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.Infrastructure.Logging
{
    public interface ILogger
    {
        NLog.ILogger Logger(string loggerName = "");
        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(Exception ex, string message);
    }
}
