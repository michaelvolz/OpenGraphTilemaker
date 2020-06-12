using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;

namespace Common
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Utility class")]
    public class Time
    {
        public static readonly string TimeFormat = "### {0} took {1}";

        // ReSharper disable once StringLiteralTypo
        public static string Ticks(Stopwatch stopwatch) =>
            new DateTime(Guard.Against.Null(stopwatch, nameof(stopwatch)).ElapsedTicks).ToString("s.fff_ffff", CultureInfo.InvariantCulture) + " seconds";

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "TODO")]
        public void This(Action action, string name, ILogger logger)
        {
            Guard.Against.Null(action, nameof(action));

            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();

            logger.LogInformation(TimeFormat, name, Ticks(stopwatch));
        }

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "TODO")]
        public async Task ThisAsync(Func<Task> action, string name, ILogger logger)
        {
            Guard.Against.Null(action, nameof(action));

            var stopwatch = Stopwatch.StartNew();

            await action();

            stopwatch.Stop();

            logger.LogInformation(TimeFormat, name, Ticks(stopwatch));
        }
    }
}
