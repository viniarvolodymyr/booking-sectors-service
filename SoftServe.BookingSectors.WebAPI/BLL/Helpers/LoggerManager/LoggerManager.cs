using NLog;

namespace SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Info messages
        /// </summary>
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Debug messages
        /// </summary>
        public void LogError(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Warning messages
        /// </summary>
        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Error messages
        /// </summary>
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}