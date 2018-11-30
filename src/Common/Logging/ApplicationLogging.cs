using Microsoft.Extensions.Logging;

namespace Common.Logging
{
    public static class ApplicationLogging
    {
        public static ILogger<T> CreateLogger<T>() => ServiceLocator.Current.GetInstance<ILogger<T>>();
    }
}
