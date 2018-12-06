using System.Net;
using System.Net.Http;
using Common;
using Common.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.AspNetCore;
using Xunit.Abstractions;

// ReSharper disable MemberCanBePrivate.Global

namespace BaseTestCode
{
    public class BaseTest<T>
    {
        protected BaseTest(ITestOutputHelper output) {
            ConfigureSerilog(output);
            ConfigureServiceLocator();
            TestConsole = ApplicationLogging.CreateLogger<T>();
        }

        public ServiceCollection Services { get; } = new ServiceCollection();

        protected ILogger<T> TestConsole { get; }

        protected static HttpClient HttpClient(FakeHttpMessageHandler fakeHttpMessageHandler) => new HttpClient(fakeHttpMessageHandler);

        protected static HttpClient HttpClient(string fakeResponse, HttpStatusCode httpStatusCode = HttpStatusCode.OK) {
            var message = new HttpResponseMessage(httpStatusCode) { Content = new StringContent(fakeResponse) };
            return HttpClient(new FakeHttpMessageHandler(message));
        }

        private static void ConfigureSerilog(ITestOutputHelper output) =>
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output, outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}\t{Exception}")
                .CreateLogger()
                .ForContext<T>();

        private void ConfigureServiceLocator() {
            Services.AddSingleton(provider => (ILoggerFactory)new SerilogLoggerFactory(null, false));
            Services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));
            ServiceLocator.SetServiceProvider(Services.BuildServiceProvider());
        }
    }
}
