using NLog;

namespace SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Info messages
        /// </summary>
        public void LogDebug(string message)
        {
            Logger.Debug(message);
        }

        /// <summary>
        /// Debug messages
        /// </summary>
        public void LogError(string message)
        {
            Logger.Error(message);
        }

        /// <summary>
        /// Warning messages
        /// </summary>
        public void LogInfo(string message)
        {
            Logger.Info(message);
        }

        /// <summary>
        /// Error messages
        /// </summary>
        public void LogWarn(string message)
        {
            Logger.Warn(message);
        }
    }
}