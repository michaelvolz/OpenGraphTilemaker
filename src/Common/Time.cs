﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;

namespace Common
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Time
    {
        public static readonly string TimeFormat = "### {0} took {1}";

        // ReSharper disable once StringLiteralTypo
        public static string Ticks(Stopwatch stopwatch) => new DateTime(stopwatch.ElapsedTicks).ToString("s.fff_ffff", CultureInfo.InvariantCulture) + " seconds";

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        public void This(Action action, string name, ILogger logger)
        {
            action = Guard.Against.Null(() => action);
            
            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();

            logger.LogInformation(TimeFormat, name, Ticks(stopwatch));
        }

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        public async Task ThisAsync(Func<Task> action, string name, ILogger logger)
        {
            action = Guard.Against.Null(() => action);

            var stopwatch = Stopwatch.StartNew();

            await action();

            stopwatch.Stop();

            logger.LogInformation(TimeFormat, name, Ticks(stopwatch));
        }
    }
}