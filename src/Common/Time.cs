using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Common
{
    public class Time
    {
        public static readonly string TimeFormat = "### ({0}) took {1}";

        public static string Ticks(Stopwatch stopwatch) {
            return new DateTime(stopwatch.ElapsedTicks).ToString("s.fff_ffff");
        }

        public void This(Action action, string name) {
            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();
            Console.WriteLine(TimeFormat, name, Ticks(stopwatch));
        }

        public async Task ThisAsync(Func<Task> action, string name) {
            var stopwatch = Stopwatch.StartNew();

            await action();

            stopwatch.Stop();
            Console.WriteLine(TimeFormat, name, Ticks(stopwatch));
        }
    }
}
