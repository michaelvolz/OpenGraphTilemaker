using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Common
{
    public class Time
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;

        private readonly string _timeInMs = "### ({0}) took {1}";

        public Time([NotNull] ILogger<Time> logger, [NotNull] IHostingEnvironment environment) {
            _hostingEnvironment = Guard.Against.Null(() => environment);
            _logger = Guard.Against.Null(() => logger);
        }

        public void This(Action action, string name) {
            if (_hostingEnvironment.EnvironmentName != EnvironmentName.Development) {
                action();
                return;
            }

            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();
            _logger.LogInformation(string.Format(_timeInMs, name, Ticks(stopwatch)));
        }

        public async Task ThisAsync(Func<Task> action, string name) {
            if (_hostingEnvironment.EnvironmentName != EnvironmentName.Development) {
                await action();
                return;
            }

            var stopwatch = Stopwatch.StartNew();

            await action();

            stopwatch.Stop();
            _logger.LogInformation(string.Format(_timeInMs, name, Ticks(stopwatch)));
        }

        private static string Ticks(Stopwatch stopwatch) {
            return new DateTime(stopwatch.ElapsedTicks).ToString("s.fff_ffff");
        }
    }
}
