using System;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable UnusedMember.Global

namespace Common.Extensions
{
    public static class AsyncExtensions
    {
        /// <summary>
        ///     TimeoutAfter.
        ///     See <a href="link">https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md</a>
        /// </summary>
        public static async Task<T> TimeoutAfter<T>(this Task<T> task, TimeSpan timeout) {
            using (var cts = new CancellationTokenSource()) {
                var delayTask = Task.Delay(timeout, cts.Token);
                var resultTask = await Task.WhenAny(task, delayTask);

                if (resultTask == delayTask)
                    throw new OperationCanceledException();

                cts.Cancel();

                return await task;
            }
        }

        /// <summary>
        ///     AsyncLazy.
        ///     See <a href="link">https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md</a>
        /// </summary>
        private class AsyncLazy<T> : Lazy<Task<T>>
        {
            public AsyncLazy(Func<Task<T>> valueFactory) : base(valueFactory) { }
        }
    }
}
