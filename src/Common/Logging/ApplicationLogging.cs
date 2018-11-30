using Microsoft.Extensions.Logging;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Common.Logging
{
    /// <summary>
    ///     Shared logger
    /// </summary>
    public static class ApplicationLogging
    {
        public static ILoggerFactory LoggerFactory { get; set; } // = new LoggerFactory();
        public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
        public static ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);
    }
}
