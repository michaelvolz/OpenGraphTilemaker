using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Extensions.Logging;

namespace Common
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Time
    {
        public static readonly string TimeFormat = "### {0} took {1}";

        // ReSharper disable once StringLiteralTypo
        public static string Ticks(Stopwatch stopwatch) => new DateTime(stopwatch.ElapsedTicks).ToString("s.fff_ffff") + " seconds";

        public void This(Action action, string name, ILogger logger)
        {
            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();

            logger.LogInformation(TimeFormat, name, Ticks(stopwatch));
        }

        public async Task ThisAsync(Func<Task> action, string name, ILogger logger)
        {
            var stopwatch = Stopwatch.StartNew();

            await action();

            stopwatch.Stop();

            logger.LogInformation(TimeFormat, name, Ticks(stopwatch));
        }
    }
}