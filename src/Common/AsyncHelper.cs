using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    ///     AsyncHelper
    /// </summary>
    /// <example>
    ///     AsyncHelper.RunSync(() => DoAsyncStuff());
    /// </example>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class AsyncHelper
    {
        private static readonly TaskFactory TaskFactory =
            new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func) => TaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();

        public static void RunSync(Func<Task> func) => TaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();

        /// <summary>
        ///     WithCancellation.
        ///     See <a href="link">https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md</a>
        /// </summary>
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            // This disposes the registration as soon as one of the tasks trigger
            await using (cancellationToken.Register(state => { ((TaskCompletionSource<object>)state!)?.TrySetResult(null!); }, tcs))
            {
                var resultTask = await Task.WhenAny(task, tcs.Task);

                if (resultTask == tcs.Task) throw new OperationCanceledException(cancellationToken);

                return await task;
            }
        }
    }
}