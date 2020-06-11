using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Extensions
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class AsyncExtensions
    {
        /// <summary>
        ///     TimeoutAfter.
        ///     See <a href="link">https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md</a>
        /// </summary>
        public static async Task<T> TimeoutAfter<T>(this Task<T> task, TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource();
            var delayTask = Task.Delay(timeout, cts.Token);
            var resultTask = await Task.WhenAny(task, delayTask);

            if (resultTask == delayTask)
                throw new OperationCanceledException();

            cts.Cancel();

            return await task;
        }

        /// <summary>
        ///     AsyncLazy.
        ///     See <a href="link">https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md</a>
        /// </summary>
        [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
        public class AsyncLazy<T> : Lazy<Task<T>>
        {
            public AsyncLazy(Func<Task<T>> valueFactory) : base(valueFactory) { }
        }
    }
}