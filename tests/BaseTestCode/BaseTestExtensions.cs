using Microsoft.Extensions.Logging;

namespace BaseTestCode
{
    public static class BaseTestExtensions
    {
        public static void WriteLine<T>(this ILogger<T> logger, string message, params object[] args) => logger.LogDebug(message, args);
    }
}
