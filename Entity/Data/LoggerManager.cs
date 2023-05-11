using NLog;
namespace Entity.Data
{
  public static class LoggerManager
  {
    public static Logger GetCurrentClassLogger()
    {
      return LogManager.GetCurrentClassLogger();
    }

    public static void LogInfo(Logger logger, string message)
    {
      logger.Info(message);
    }

    public static void LogDebug(Logger logger, string message)
    {
      logger.Debug(message);
    }
    public static void LogError(Logger logger, string message)
    {
      logger.Error(message);
    }
  }
}
