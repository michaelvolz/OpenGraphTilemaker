using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace Common
{
    //public class Time
    //{
    //    public static readonly string TimeFormat = "### ({0}) took {1}";

    //    public void This(Action action, string name) {
    //        var stopwatch = Stopwatch.StartNew();

    //        action();

    //        stopwatch.Stop();
    //        Console.WriteLine(string.Format(TimeFormat, name, Ticks(stopwatch)));
            
    //    }

    //    public async Task ThisAsync(Func<Task> action, string name) {
    //        var stopwatch = Stopwatch.StartNew();

    //        await action();

    //        stopwatch.Stop();
    //        Console.WriteLine(string.Format(TimeFormat, name, Ticks(stopwatch)));
    //    }

    //    public static string Ticks(Stopwatch stopwatch) {
    //        return new DateTime(stopwatch.ElapsedTicks).ToString("s.fff_ffff");
    //    }
    //}
}
