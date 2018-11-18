using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common
{
    public class Stop
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;

        public Stop([NotNull] ILogger<Stop> logger, [NotNull] IHostingEnvironment hostingEnvironment) {
            _hostingEnvironment = Guard.Against.Null(() => hostingEnvironment);
            _logger = Guard.Against.Null(() => logger);
        }

        public void Watch(Action action, string name) {
            if (_hostingEnvironment.EnvironmentName != EnvironmentName.Development) {
                action();
                return;
            }

            Stopwatch stopwatch;
            stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();

            _logger.LogWarning($"### ({name}) Time in ms: {stopwatch.ElapsedMilliseconds}");
        }

        public async Task WatchAsync(Func<Task> action, string name) {
            if (_hostingEnvironment.EnvironmentName != EnvironmentName.Development) {
                await action();
                return;
            }

            var stopwatch = Stopwatch.StartNew();

            await action();

            stopwatch.Stop();

            _logger.LogWarning($"### ({name}) Time in ms: {stopwatch.ElapsedMilliseconds}");
        }
    }
}