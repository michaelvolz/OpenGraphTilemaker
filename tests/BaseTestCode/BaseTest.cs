using System.Net;
using System.Net.Http;
using Common;
using Common.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Xunit.Abstractions;

// ReSharper disable MemberCanBePrivate.Global

namespace BaseTestCode
{
    public class BaseTest<T>
    {
        protected BaseTest(ITestOutputHelper output)
        {
            ConfigureSerilog(output);
            ConfigureDefaultServices();
            UpdateServiceLocator();

            TestConsole = ApplicationLogging.CreateLogger<T>();
        }

        protected ServiceCollection Services { get; } = new ServiceCollection();

        protected ILogger<T> TestConsole { get; }

        protected HttpClient HttpClient(FakeHttpMessageHandler fakeHttpMessageHandler) =>
            new HttpClient(fakeHttpMessageHandler);

        protected HttpClient HttpClient(string fakeResponse, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            var message = new HttpResponseMessage(httpStatusCode) {Content = new StringContent(fakeResponse)};

            return HttpClient(new FakeHttpMessageHandler(message));
        }

        protected void UpdateServiceLocator() => ServiceLocator.SetServiceProvider(Services.BuildServiceProvider());

        private void ConfigureSerilog(ITestOutputHelper output) =>
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output,
                    outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}\t{Exception}")
                .CreateLogger()
                .ForContext<T>();

        private void ConfigureDefaultServices()
        {
            Services.AddSingleton(provider => (ILoggerFactory)new SerilogLoggerFactory());
            Services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));
        }
    }
}