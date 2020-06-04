using System;
using System.Diagnostics.CodeAnalysis;
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

namespace BaseTestCode
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class BaseTest<T> : IDisposable
    {
        private FakeHttpMessageHandler? _fakeHttpMessageHandler;
        private HttpResponseMessage? _httpResponseMessage;

        protected BaseTest(ITestOutputHelper output)
        {
            ConfigureSerilog(output);
            ConfigureDefaultServices();
            UpdateServiceLocator();

            TestConsole = ApplicationLogging.CreateLogger<T>();
        }

        protected ServiceCollection Services { get; } = new ServiceCollection();

        protected ILogger<T> TestConsole { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fakeHttpMessageHandler?.Dispose();
                _httpResponseMessage?.Dispose();
            }
        }

        protected HttpClient HttpClient(FakeHttpMessageHandler fakeHttpMessageHandler) =>
            new HttpClient(fakeHttpMessageHandler);

        protected HttpClient HttpClient(string fakeResponse, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            _httpResponseMessage = new HttpResponseMessage(httpStatusCode) {Content = new StringContent(fakeResponse)};
            _fakeHttpMessageHandler ??= new FakeHttpMessageHandler(_httpResponseMessage);

            return HttpClient(_fakeHttpMessageHandler);
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