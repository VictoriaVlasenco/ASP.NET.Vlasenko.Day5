using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookClassLibrary
{
    public class NLoggerAdapter : ILogger
    {
        private static readonly NLog.Logger nLogger = NLog.LogManager.GetCurrentClassLogger();

        public void Debug(string message)
        {
            nLogger.Debug(message);
        }

        public void Warn(string message)
        {
            nLogger.Warn(message);
        }

        public void Error(string message)
        {
            nLogger.Error(message);
        }

        public void Info(string message)
        {
            nLogger.Info(message);
        }
    }
}
