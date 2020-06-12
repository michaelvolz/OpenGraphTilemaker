using Microsoft.Extensions.Logging;

namespace Common.Logging
{
    public static class ApplicationLogging
    {
        public static ILogger<T> CreateLogger<T>()
        {
            var factory = ServiceLocator.Current.GetInstance<ILoggerFactory>();

            return factory.CreateLogger<T>();
        }
    }
}
